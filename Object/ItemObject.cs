using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class ItemObject : AbsObject
    {
        public Vector2 sp;
        public bool inBlock = false;
        private ISpriteFactory factory;
        public ContentManager content;
        public string State;
        public Vector2 afterBump = new Vector2(0);
        public AudioManager audio;

        public ItemObject(Vector2 startingPosition, ContentManager content, AudioManager audio, string state)
        {
            _opacity = 1.0f;
            _objectsToAdd = new List<AbsObject>();
            deleteThis = false;
            isVisible = true;
            sp = startingPosition;
            _position = startingPosition;
            factory = new ItemSpriteFactory(content);
            _hitbox = new BoundingBox(new Vector3(startingPosition.X, startingPosition.Y, 0), new Vector3(startingPosition.X + 16, startingPosition.Y + 16, 0));
            this.content = content;
            this.audio = audio;
            _velocity = new Vector2(0);
            if (state != "coin" && state != "speed" && state != "jump" && state != "propeller")
                _acceleration = new Vector2(0, 0.05f);
            _sprite = factory.build(state);
            State = state;
            _objectsToNotCollide = new List<AbsObject>();
        }


        public override void Update(GameTime gameTime)
        {
            _objectsToNotCollide.Clear();
            if (inBlock)
            {
                //inBlock = false;
                if (State != "coin")
                {
                    if (_position.Y < sp.Y - 17)
                    {
                        _velocity.Y = 0;
                        _velocity.X = afterBump.X;
                        inBlock = false;
                    }

                }
                else
                {
                    if (_position.Y < sp.Y - 32)
                    {
                        _velocity.Y = 0;
                        isVisible = false;
                        _hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                        inBlock = false;

                    }
                }
            }
            else
            {
                if (!State.Equals("castle") && !State.Equals("pipe") && !State.Equals("pipe"))
                {
                    _velocity = _velocity + _acceleration;
                }
            }
            _sprite.Update(gameTime);
        }

        public void Bump()
        {
            _velocity.Y = -3.3f;
            _hitbox = new BoundingBox(new Vector3(sp.X, sp.Y, 0), new Vector3(sp.X + 16, sp.Y + 16, 0));
        }








        public void speedD(Vector2 pos)
        {
            switch (this.State)
            {
                case "super":
                    audio.PlaySound("powerUpAppear");
                    if (pos.X < this.Position.X)
                        afterBump.X = -1;
                    else
                        afterBump.X = 1;
                    break;
                case "1up":
                    audio.PlaySound("powerUpAppear");
                    if (pos.X < this.Position.X)
                        afterBump.X = 1;
                    else
                        afterBump.X = -1;
                    break;
                case "coin":
                    audio.PlaySound("coin");
                    break;
                case "star":
                    audio.PlaySound("powerUpAppear");
                    if (pos.X < this.Position.X)
                        afterBump.X = 1;
                    else
                        afterBump.X = -1;

                    break;
                default:
                    audio.PlaySound("powerUpAppear");
                    break;




            }
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
                    TopCollide(this, obj, directionsBlocked, audio);
                }
                else if (type == Collision.CollisionType.RSide || type == Collision.CollisionType.LSide)
                {
                    SideCollide(this, obj, directionsBlocked, audio);
                }
                else if (type == Collision.CollisionType.BSide)
                {
                    BottomCollide(this, obj, directionsBlocked, audio);
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
                    SideCollide(this, obj, directionsBlocked, audio);
                }
                else if (cornerCollidedObjects[obj] == Collision.CollisionType.TLCorner && !directionsBlocked[0] && !directionsBlocked[3])
                {
                    SideCollide(this, obj, directionsBlocked, audio);
                }
                else if (cornerCollidedObjects[obj] == Collision.CollisionType.BRCorner && !directionsBlocked[2] && !directionsBlocked[1])
                {
                    BottomCollide(this, obj, directionsBlocked, audio);
                }
                else if (cornerCollidedObjects[obj] == Collision.CollisionType.BLCorner && !directionsBlocked[2] && !directionsBlocked[3])
                {
                    BottomCollide(this, obj, directionsBlocked, audio);
                }
            }
        }

        private static void TopCollide(ItemObject item, AbsObject obj, bool[] directionsBlocked, AudioManager audio)
        {
            if (obj is BlockObject || obj is PlatformObject)
            {
                item._velocity.Y = 0;
                item.Displace(0, 0.5f);
                directionsBlocked[0] = true;
                if (item.State == "star")
                {
                    item._velocity.Y *= -1;
                }
            }
            else if (obj is MarioObject)
            {
                if (item.State == "coin")
                    audio.PlaySound("coin");
                else if (item.State == "1up")
                    audio.PlaySound("oneUp");
                item.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                item.deleteThis = true;
                item.isVisible = false;
            }
            else if (obj is DoodleObject)
            {
                if (item.State == "speed" || item.State == "jump")
                    audio.PlaySound("potion");
                item.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                item.deleteThis = true;
                item.isVisible = false;
            }
            else
            {
                item.ObjectsToNotCollide.Add(obj);
            }
        }

        private static void SideCollide(ItemObject item, AbsObject obj, bool[] directionsBlocked, AudioManager audio)
        {
            if (obj is BlockObject || obj is PlatformObject)
            {
                if (Collision.GetCollisionType(item, obj) == Collision.CollisionType.RSide)
                {
                    if (!directionsBlocked[1])
                    {
                        directionsBlocked[1] = true;
                        item._velocity.X *= -1;
                    }
                }
                else
                {
                    if (!directionsBlocked[3])
                    {
                        directionsBlocked[3] = true;
                        item._velocity.X *= -1;
                    }
                }
                Console.WriteLine("Post side collision x velocity: " + item.Velocity.X);
            }
            else if (obj is MiscObject)
            {
                if (((MiscObject)obj).Type == "pipe_head" || ((MiscObject)obj).Type == "pipe_section")
                {
                    item._velocity.X *= -1;
                    if (Collision.GetCollisionType(item, obj) == Collision.CollisionType.RSide)
                    {
                        directionsBlocked[1] = true;
                    }
                    else
                    {
                        directionsBlocked[3] = true;
                    }
                }
                else
                {
                    item.ObjectsToNotCollide.Add(obj);
                }
            }
            else if (obj is MarioObject)
            {
                if (item.State == "coin")
                    audio.PlaySound("coin");
                else if (item.State == "1up")
                    audio.PlaySound("oneUp");
                item.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                item.deleteThis = true;
                item.isVisible = false;
            }
            else if (obj is DoodleObject)
            {
                if (item.State == "speed" || item.State == "jump")
                    audio.PlaySound("potion");
                item.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                item.deleteThis = true;
                item.isVisible = false;
            }
            else
            {
                item.ObjectsToNotCollide.Add(obj);
            }
        }

        private static void BottomCollide(ItemObject item, AbsObject obj, bool[] directionsBlocked, AudioManager audio)
        {
            if (obj is BlockObject || obj is PlatformObject)
            {
                item._velocity.Y = 0;
                directionsBlocked[2] = true;
                if (item.State == "star")
                {
                    item._velocity.Y = -1;
                }
            }
            else if (obj is MiscObject)
            {
                if (((MiscObject)obj).Type == "pipe_head")
                {
                    item._velocity.Y = 0;
                    directionsBlocked[2] = true;
                    if (item.State == "star")
                    {
                        item._velocity.Y = -1;
                    }
                }
                else
                {
                    item.ObjectsToNotCollide.Add(obj);
                }
            }
            else if (obj is MarioObject)
            {
                if (item.State == "coin")
                    audio.PlaySound("coin");
                else if (item.State == "1up")
                    audio.PlaySound("oneUp");
                item.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                item.deleteThis = true;
                item.isVisible = false;
            }
            else if (obj is DoodleObject)
            {
                if (item.State == "speed" || item.State == "jump")
                    audio.PlaySound("potion");
                item.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                item.deleteThis = true;
                item.isVisible = false;
            }
            else
            {
                item.ObjectsToNotCollide.Add(obj);
            }
        }
    }
}