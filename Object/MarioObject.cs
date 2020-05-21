using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace template_test
{
    class MarioObject : AbsAvatarObject
    {

        public bool warped;
        public MiscObject Pipe;
        public int warpingUpdates;
        private List<int> checkpoints = new List<int> { 1000, 2000, 2890 };



        public MarioObject(Vector2 startingPosition, ContentManager cont, AudioManager audio)
        {
            _opacity = 1.0f;
            warpingUpdates = 0;
            _objectsToNotCollide = new List<AbsObject>();
            _objectsToAdd = new List<AbsObject>();
            isVisible = true;
            deleteThis = false;
            _position = startingPosition;
            content = cont;
            _velocity = new Vector2(0);
            _acceleration = new Vector2(0, 0.05f);
            powerUpState = new SmallState(this);
            movementState = new RightIdleState(this);
            isGrounded = true;
            warped = true;
            Pipe = null;
            PipeContacted = null;
            origMoveState = null;
            origSprite = null;
            tookDamage = false;
            this.audio = audio;
        }
        

        override public void Up()
        {
            // Console.WriteLine("up");
            if (isGrounded)
            {
                if (!(movementState is RightCrouchingState) && !(movementState is LeftCrouchingState))
                {
                    if (powerUpState is SmallState)
                        audio.PlaySound("smallJump");
                    else
                        audio.PlaySound("superJump");
                }
                movementState.Up();
            }
        }

        override public void Down()
        {
            // Console.WriteLine("down");
            if (isGrounded)
            {
                if (PipeContacted != null && PipeContacted.WarpDestination != null)
                {
                    _velocity = new Vector2(0);
                    warped = false;
                    Pipe = PipeContacted;
                    origMoveState = movementState;
                    origSprite = _sprite;
                    movementState = new WarpDownState(this);
                    _sprite = origSprite;
                    this.Teleport(PipeContacted.Position.X + 9, PipeContacted.Position.Y - 16);
                    audio.PlaySound("pipe");
                }
                else
                {
                    movementState.Down();
                }
            }
        }
        override public void Left()
        {
            // Console.WriteLine("left");
            movementState.Left();
        }
        override public void Right()
        {
            // Console.WriteLine("right");
            movementState.Right();
        }

        override public void FindCheckpoint()
        {
            int max = 0;
            foreach(int i in checkpoints)
            {
                if((this.Position.X > i) && (i > max))
                {
                    max = i;
                }
            }
            this.Teleport(max, 420);
        }

        override public void TakeDamage()
        {
            if (!tookDamage)
            {
                powerUpState.TakeDamage();
                tookDamage = true;
                if (movementState is DeadState)
                {
                    MediaPlayer.Stop();
                    audio.PlaySound("marioDie");
                }
                else
                    audio.PlaySound("powerDown");
            }
        }
        override public void Super()
        {
            audio.PlaySound("powerUp");
            powerUpState.ToSuper(); 
        }
        override public void Fire()
        {
            audio.PlaySound("powerUp");
            powerUpState.ToFire();
        }
        override public void Small()
        {
            powerUpState.ToSmall();
            if (movementState is DeadState)
            {
                movementState = new RightIdleState(this);
            }
        }
        

        public override void Update(GameTime gameTime)
        {
            if (!warped && (movementState is WarpDownState || movementState is WarpUpState))
            {
                if (movementState is WarpDownState)
                {
                    this.Displace(0, 1);
                }
                else
                {
                    this.Displace(0, -1);
                }
                warpingUpdates++;
                if (warpingUpdates >= (_hitbox.Value.Max.Y - _hitbox.Value.Min.Y))
                {
                    if (movementState is WarpDownState)
                    {
                        this.Teleport(Pipe.WarpDestination.Value.X + 9, Pipe.WarpDestination.Value.Y);
                        warpingUpdates = 0;
                        origSprite = _sprite;
                        movementState = new WarpUpState(this);
                        _sprite = origSprite;
                    }
                    else
                    {
                        if (origMoveState is RightIdleState || origMoveState is RightWalkingState)
                        {
                            movementState = new RightIdleState(this);
                        }
                        else
                        {
                            movementState = new LeftIdleState(this);
                        }
                        //_velocity = new Vector2(0);
                        warped = true;
                    }
                    warpingUpdates = 0;
                }
            }
            else
            {
                _velocity = _velocity + _acceleration;
            }

            if (_velocity.Y > 0.5)
            {
                this.isGrounded = false;
                //if (!(movementState is LeftJumpingIdleState || movementState is LeftJumpingState || movementState is RightJumpingState || movementState is RightJumpingState))
                //    movementState = new RightJumpingState(this);
            }

            if (tookDamage)
            {
                damageCounter++;
                if (damageCounter >= 10)
                    tookDamage = false;
            }

            if (!(movementState is DeadState))
            {
                if (this.Position.Y > 500)
                {
                    powerUpState = new SmallState(this);
                    movementState = new DeadState(this);
                }
            }

            Sprite.Update(gameTime);
            PipeContacted = null;
            _objectsToNotCollide.Clear();
        }

        public override void Collide(List<AbsObject> collidedObjects)
        {

                bool[] directionsBlocked = new bool[] { false, false, false, false };
                Dictionary<AbsObject, Collision.CollisionType> cornerCollidedObjects = new Dictionary<AbsObject, Collision.CollisionType>();
                foreach (AbsObject obj in collidedObjects)
                {
                    Collision.CollisionType type = Collision.GetCollisionType(this, obj);
                    if (type == Collision.CollisionType.TSide)
                    {
                        TopCollide(this, obj, directionsBlocked);
                    }
                    else if (type == Collision.CollisionType.RSide || type == Collision.CollisionType.LSide)
                    {
                        SideCollide(this, obj, directionsBlocked);
                    }
                    else if (type == Collision.CollisionType.BSide)
                    {
                        BottomCollide(this, obj, directionsBlocked);
                    }
                    else
                    {
                        cornerCollidedObjects.Add(obj, type);
                    }
                }
                foreach (AbsObject obj in cornerCollidedObjects.Keys)
                {
                    if (cornerCollidedObjects[obj] == Collision.CollisionType.TRCorner && !directionsBlocked[0] && !directionsBlocked[1])
                    {
                        SideCollide(this, obj, directionsBlocked);
                    }
                    else if (cornerCollidedObjects[obj] == Collision.CollisionType.TLCorner && !directionsBlocked[0] && !directionsBlocked[3])
                    {
                        SideCollide(this, obj, directionsBlocked);
                    }
                    else if (cornerCollidedObjects[obj] == Collision.CollisionType.BRCorner && !directionsBlocked[2] && !directionsBlocked[1])
                    {
                        BottomCollide(this, obj, directionsBlocked);
                    }
                    else if (cornerCollidedObjects[obj] == Collision.CollisionType.BLCorner && !directionsBlocked[2] && !directionsBlocked[3])
                    {
                        BottomCollide(this, obj, directionsBlocked);
                    }
                }
        }

        private static void TopCollide(AbsAvatarObject avatar, AbsObject obj, bool[] directionsBlocked)
        {
            if (obj is BlockObject || obj is PlatformObject)
            {
                avatar._velocity.Y = 0;
                avatar.Displace(0, 0.5f);
                directionsBlocked[0] = true;
            }
            else if (obj is EnemyObject)
            {
                avatar.TakeDamage();
                avatar.ObjectsToNotCollide.Add(obj);
            }
            else if (obj is ItemObject)
            {
                switch (((ItemObject)obj).State)
                {
                    case "super":
                        avatar.Super();
                        avatar.hud.ChangeScore(1000);
                        break;
                    case "fire":
                        avatar.Fire();
                        avatar.hud.ChangeScore(1000);
                        break;
                    case "coin":
                        avatar.hud.GetCoin();
                        break;
                    case "star":
                        avatar.hud.ChangeScore(1000);
                        break;
                    case "1up":
                        avatar.hud.ChangeLife(1);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                avatar.ObjectsToNotCollide.Add(obj);
            }
        }

        private static void SideCollide(AbsAvatarObject avatar, AbsObject obj, bool[] directionsBlocked)
        {
            if (obj is BlockObject || obj is PlatformObject)
            {
                avatar._velocity.X = 0;
                if (Collision.GetCollisionType(avatar, obj) == Collision.CollisionType.RSide)
                {
                    directionsBlocked[1] = true;
                } else
                {
                    directionsBlocked[3] = true;
                }
                if (avatar.movementState is LeftWalkingState)
                {
                    avatar.movementState = new LeftIdleState(avatar);
                }
                else if (avatar.movementState is RightWalkingState)
                {
                    avatar.movementState = new RightIdleState(avatar);
                }
                else if (avatar.movementState is LeftJumpingState)
                {
                    avatar.movementState = new LeftJumpingIdleState(avatar);
                }
                else if (avatar.movementState is RightJumpingState)
                {
                    avatar.movementState = new RightJumpingIdleState(avatar);
                }
            }
            else if (obj is EnemyObject)
            {
                avatar.TakeDamage();
                avatar.ObjectsToNotCollide.Add(obj);
            }
            else if (obj is ItemObject)
            {
                switch (((ItemObject)obj).State)
                {
                    case "super":
                        avatar.Super();
                        avatar.hud.ChangeScore(1000);
                        break;
                    case "fire":
                        avatar.Fire();
                        avatar.hud.ChangeScore(1000);
                        break;
                    case "coin":
                        avatar.hud.GetCoin();
                        break;
                    case "star":
                        avatar.hud.ChangeScore(1000);
                        break;
                    case "1up":
                        avatar.hud.ChangeLife(1);
                        break;
                    default:
                        break;
                }
            }
            else if (obj is MiscObject)
            {
                if (((MiscObject)obj).Type == "pipe_head" || ((MiscObject)obj).Type == "pipe_section")
                {
                    avatar._velocity.X = 0;
                    if (Collision.GetCollisionType(avatar, obj) == Collision.CollisionType.RSide)
                    {
                        directionsBlocked[1] = true;
                    }
                    else
                    {
                        directionsBlocked[3] = true;
                    }
                    if (avatar.movementState is LeftWalkingState)
                    {
                        avatar.movementState = new LeftIdleState(avatar);
                    }
                    else if (avatar.movementState is RightWalkingState)
                    {
                        avatar.movementState = new RightIdleState(avatar);
                    }
                    else if (avatar.movementState is LeftJumpingState)
                    {
                        avatar.movementState = new LeftJumpingIdleState(avatar);
                    }
                    else if (avatar.movementState is RightJumpingState)
                    {
                        avatar.movementState = new RightJumpingIdleState(avatar);
                    }
                }
                else
                {
                    avatar.ObjectsToNotCollide.Add(obj);
                }
            }
            else
            {
                avatar.ObjectsToNotCollide.Add(obj);
            }
        }

        private static void BottomCollide(AbsAvatarObject avatar, AbsObject obj, bool[] directionsBlocked)
        {
            if (obj is BlockObject || obj is PlatformObject)
            {
                avatar._velocity.Y = 0;
                directionsBlocked[2] = true;
                avatar.isGrounded = true;

                if (avatar.movementState is LeftJumpingIdleState || avatar.movementState is LeftJumpingState)
                {
                    avatar.movementState = new LeftIdleState(avatar);
                }
                else if (avatar.movementState is RightJumpingIdleState || avatar.movementState is RightJumpingState)
                {
                    avatar.movementState = new RightIdleState(avatar);
                }
            }
            else if (obj is EnemyObject)
            {
                avatar._velocity.Y = -2;
                avatar.hud.ChangeScore(100);
            }
            else if (obj is ItemObject)
            {
                switch (((ItemObject)obj).State)
                {
                    case "super":
                        avatar.Super();
                        avatar.hud.ChangeScore(1000);
                        break;
                    case "fire":
                        avatar.Fire();
                        avatar.hud.ChangeScore(1000);
                        break;
                    case "coin":
                        avatar.hud.GetCoin();
                        break;
                    case "star":
                        avatar.hud.ChangeScore(1000);
                        break;
                    case "1up":
                        avatar.hud.ChangeLife(1);
                        break;
                    default:
                        break;
                }
            }
            else if (obj is MiscObject)
            {
                if (((MiscObject)obj).Type == "pipe_head")
                {
                    avatar._velocity.Y = 0;
                    avatar.PipeContacted = (MiscObject)obj;
                    directionsBlocked[2] = true;
                    avatar.isGrounded = true;

                    if (avatar.movementState is LeftJumpingIdleState || avatar.movementState is LeftJumpingState)
                    {
                        avatar.movementState = new LeftIdleState(avatar);
                    }
                    else if (avatar.movementState is RightJumpingIdleState || avatar.movementState is RightJumpingState)
                    {
                        avatar.movementState = new RightIdleState(avatar);
                    }
                }
                else
                {
                    avatar.ObjectsToNotCollide.Add(obj);
                }
            }
            else
            {
                avatar.ObjectsToNotCollide.Add(obj);
            }
        }
    }
}
