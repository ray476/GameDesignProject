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
    class DoodleObject : AbsAvatarObject
    {
        private bool isMoving;
        public Vector2 groundVelocity;
        public int score;
        public AbsPowerStateDoodle powerState;
        public AbsDoodleMoveState moveState;
        private int propellerUpdates;
        private int powerUpdates;
        public bool hasDoubleJump = false;
        public DoodleObject(Vector2 startingPosition, ContentManager cont, AudioManager audio)
        {
            propellerUpdates = 0;
            powerUpdates = 0;
            score = 0;
            isMoving = false;
            _opacity = 1.0f;
            _objectsToNotCollide = new List<AbsObject>();
            _objectsToAdd = new List<AbsObject>();
            isVisible = true;
            deleteThis = false;
            _position = startingPosition;
            content = cont;
            _velocity = new Vector2(0);
            _acceleration = new Vector2(0, 0.03f);
            powerState = new StandardDoodleState(this);
            moveState = new DoodleIdleRightState(this);
            isGrounded = false;
            groundVelocity = Vector2.Zero;
            tookDamage = false;
            this.audio = audio;
        }

        override public void Up()
        {
            
            if (isGrounded)
            {
                _velocity.Y += -5.0f;
                audio.PlaySound("bouncyJump");
            }
            else if(powerState is DoubleState)
            {
                if (hasDoubleJump)
                {
                    _velocity.Y += -5f;
                    audio.PlaySound("bouncyJump");
                    hasDoubleJump = false;
                }
            }
        }

        override public void Down()
        {
            moveState.Down();
        }
        override public void Left()
        {
            if (!(powerUpState is SpeedState))
            {
                if (isGrounded)
                {
                    if (_velocity.X - groundVelocity.X > -3f)
                    {
                        _velocity.X += -0.1f;
                    }
                }
                else
                {
                    if (_velocity.X > -3f)
                    {
                        _velocity.X += -0.1f;
                    }
                }
            }
            moveState.Left();
            isMoving = true;
        }
        override public void Right()
        {
            if (!(powerUpState is SpeedState))
            {

                if (isGrounded)
                {
                    if (_velocity.X - groundVelocity.X < 3f)
                    {
                        _velocity.X += 0.1f;
                    }
                }
                else
                {
                    if (_velocity.X < 3f)
                    {
                        _velocity.X += 0.1f;
                    }
                }
            }
            moveState.Right();
            isMoving = true;
        }

        override public void FindCheckpoint()
        {
        }

        override public void TakeDamage()
        {
            
        }
        override public void Super()
        {
            
        }
        override public void Fire()
        {
            
        }
        override public void Small()
        {
           
        }


        public override void Update(GameTime gameTime)
        {
            if (moveState is DoodleFlyingState)
            {
                _velocity.Y = -5f;
                if (propellerUpdates < 400)
                {
                    propellerUpdates++;
                } else
                {
                    propellerUpdates = 0;
                    moveState = new DoodleFallingState(this);
                }
            }
            if(!(powerState is StandardDoodleState))
            {
                if (powerUpdates < 500)
                {
                    powerUpdates++;
                }
                else
                {
                    powerUpdates = 0;
                    powerState = new StandardDoodleState(this);
                }
            }

            _velocity = _velocity + _acceleration;
            if (!isMoving)
            {
                if (isGrounded && _velocity.X != groundVelocity.X)
                {
                    if (_velocity.X - groundVelocity.X < -0.5)
                    {
                        _velocity.X += 0.1f;
                    } else if (_velocity.X - groundVelocity.X > 0.5)
                    {
                        _velocity.X -= 0.1f;
                    } else
                    {
                        _velocity.X = groundVelocity.X;
                    }
                } else if (!isGrounded && _velocity.X != 0)
                {
                    if (_velocity.X < -0.5f)
                    {
                        _velocity.X += 0.05f;
                    } else if (_velocity.X > 0.5f)
                    {
                        _velocity.X -= 0.05f;
                    } else
                    {
                        _velocity.X = 0;
                    }
                }
            }
            if (!isGrounded)
            {
                if (_velocity.Y > 0)
                    moveState = new DoodleFallingState(this);
            } else
            {
                moveState = new DoodleIdleRightState(this);
            }
            if (tookDamage)
            {
                damageCounter++;
                if (damageCounter >= 10)
                    tookDamage = false;
            }
           
            Sprite.Update(gameTime);
            isGrounded = false;
            isMoving = false;
            _objectsToNotCollide.Clear();
        }

        public override void Collide(List<AbsObject> collidedObjects)
        {

            bool[] directionsBlocked = new bool[] { false, false, false, false };
            Dictionary<AbsObject, Collision.CollisionType> cornerCollidedObjects = new Dictionary<AbsObject, Collision.CollisionType>();
            foreach (AbsObject obj in collidedObjects)
            {
                if (obj is ItemObject)
                {
                    if (((ItemObject)obj).State == "speed")
                    {
                        this.powerState.ToFast();
                        this.audio.PlaySound("potion");
                    }
                    else if (((ItemObject)obj).State == "jump")
                    {
                        this.powerState.ToDouble();
                        this.audio.PlaySound("potion");
                    }
                    else if (((ItemObject)obj).State == "propeller")
                    {
                        this.moveState = new DoodleFlyingState(this);
                        this.audio.PlaySound("copter");
                    }
                }
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

        private static void TopCollide(DoodleObject avatar, AbsObject obj, bool[] directionsBlocked)
        {
            if (obj is ItemObject)
            {
                switch (((ItemObject)obj).State)
                {
                    case "jump":
                        avatar.powerState = new DoubleState(avatar);
                        break;
                    case "speed":
                        avatar.powerState = new SpeedState(avatar);
                        break;
                    case "propeller":
                        
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

        private static void SideCollide(DoodleObject avatar, AbsObject obj, bool[] directionsBlocked)
        {
            
            if (obj is ItemObject)
            {
                switch (((ItemObject)obj).State)
                {
                    case "speed":
                        avatar.powerState = new SpeedState(avatar);
                        break;
                    case "jump":
                        avatar.powerState = new DoubleState(avatar);
                        break;
                    case "propeller":
                        
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

        private static void BottomCollide(DoodleObject avatar, AbsObject obj, bool[] directionsBlocked)
        {
            avatar.hasDoubleJump = true;
            if ((obj is BlockObject || obj is PlatformObject) && avatar.Velocity.Y >= 0)
            {
                avatar._velocity.Y = 0;
                directionsBlocked[2] = true;
                avatar.isGrounded = true;
                avatar.groundVelocity = obj.Velocity;

                if (avatar.movementState is LeftJumpingIdleState || avatar.movementState is LeftJumpingState || avatar.movementState is LeftFallingState || avatar.movementState is LeftIdleFallingState)
                {
                    avatar.movementState = new LeftIdleState(avatar);
                }
                else if (avatar.movementState is RightJumpingIdleState || avatar.movementState is RightJumpingState || avatar.movementState is RightFallingState || avatar.movementState is RightIdleFallingState)
                {
                    avatar.movementState = new RightIdleState(avatar);
                }
            }
            else if (obj is ItemObject)
            {
                switch (((ItemObject)obj).State)
                {
                    case "speed":
                        avatar.powerState = new SpeedState(avatar);
                        break;
                    case "jump":
                        avatar.powerState = new DoubleState(avatar);
                        break;
                    case "propeller":
                        
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
    }
}
