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
    class EnemyObject : AbsObject
    {
        
        public IEnemyState enemyState;
        public IEnemyState pirhanaState;
        public ContentManager content;
        public MarioObject Mario { get; set; }
        public MiscObject Pipe;
        public bool start = false;
        public AudioManager audio;
        private int updateCounter;

        public EnemyObject(Vector2 startingPosition, ContentManager content, AudioManager audio, string state)
        {
            _opacity = 1.0f;
            updateCounter = 0;
            pirhanaState = null;
            _objectsToAdd = new List<AbsObject>();
            _objectsToNotCollide = new List<AbsObject>();
            deleteThis = false;
            isVisible = true;
           _position = startingPosition;
            this.content = content;
            this.audio = audio;
            _velocity = new Vector2(0);
            _acceleration = new Vector2(0, 0);
            switch (state) {
                case "goomba":
                    enemyState = new WalkingGoomba(this);
                    break;

                case "redKoopa":
                    enemyState = new WalkingRedKoopa(this);
                    break;
                case "pirhana":
                    enemyState = new PirhanaPlant(this);
                    break;

                case "greenKoopa":
                    enemyState = new WalkingGreenKoopa(this);
                    break;
            }
        }

        public void TakeDamage()
        {
            enemyState.TakeDamage();
        }

        public void StartMoving()
        {
            //first check is the enemy started on the (default) screen with mario
            if (Position.X < 800 && !start)
            {
                _velocity = new Vector2(-1, 0);
                _acceleration = new Vector2(0, 0.05f);
                start = true;
            } //if not starting with mario, then the 'leading edge' of the screen will be with mario in the middle 
            else if ((Math.Abs(Position.X - Mario.Position.X) < 400) && !start)
            {
                _velocity = new Vector2(-1, 0);
                _acceleration = new Vector2(0, 0.05f);
                start = true;
            }
            //changing start to true prevents setting the velocity after the movement has started
        }


        public override void Update(GameTime gameTime)
        {
            StartMoving();
            _sprite.Update(gameTime);
            _objectsToNotCollide.Clear();
            _velocity = _velocity + _acceleration;
            if (this._position.Y > 550)
            {
                this.deleteThis = true;
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

        private static void TopCollide(EnemyObject enemy, AbsObject obj, bool[] directionsBlocked, AudioManager audio)
        {
            if (obj is BlockObject)
            {
                enemy._velocity.Y = 0;
                enemy.Displace(0, 0.5f);
                directionsBlocked[0] = true;
            }
            else if (obj is MarioObject)
            {
                audio.PlaySound("kick");
                enemy.TakeDamage();
                enemy.ObjectsToNotCollide.Add(obj);
            }
            else
            {
                enemy.ObjectsToNotCollide.Add(obj);
            }
        }

        private static void SideCollide(EnemyObject enemy, AbsObject obj, bool[] directionsBlocked)
        {
            if (obj is BlockObject)
            {
                if (Collision.GetCollisionType(enemy, obj) == Collision.CollisionType.RSide)
                {
                    if (!directionsBlocked[1])
                    {
                        directionsBlocked[1] = true;
                        enemy._velocity.X *= -1;
                    }
                }
                else
                {
                    if (!directionsBlocked[3])
                    {
                        directionsBlocked[3] = true;
                        enemy._velocity.X *= -1;
                    }
                }
                Console.WriteLine("Post side collision x velocity: " + enemy.Velocity.X);
            }
            else if (obj is MiscObject)
            {
                if (((MiscObject)obj).Type == "pipe_head" || ((MiscObject)obj).Type == "pipe_section")
                {
                    enemy._velocity.X *= -1;
                    if (Collision.GetCollisionType(enemy, obj) == Collision.CollisionType.RSide)
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
                    enemy.ObjectsToNotCollide.Add(obj);
                }
            }
            else
            {
                enemy.ObjectsToNotCollide.Add(obj);
            }
        }

        private static void BottomCollide(EnemyObject enemy, AbsObject obj, bool[] directionsBlocked)
        {
            if (obj is BlockObject)
            {
                enemy._velocity.Y = 0;
                directionsBlocked[2] = true;
            }
            else if (obj is MiscObject)
            {
                if (((MiscObject)obj).Type == "pipe_head")
                {
                    enemy._velocity.Y = 0;
                    directionsBlocked[2] = true;
                }
                else
                {
                    enemy.ObjectsToNotCollide.Add(obj);
                }
            }
            else
            {
                enemy.ObjectsToNotCollide.Add(obj);
            }
        }

        /*
        public override void Collide(List<AbsObject> collidedObjects)
        {
            bool[] directionsBlocked = new bool[] { false, false, false, false };
            Dictionary<AbsObject, int> cornerCollidedBlocks = new Dictionary<AbsObject, int>();
            foreach (AbsObject obj in collidedObjects)
            {
                int collisionType = (int)Collision.GetCollisionType(this, obj);
                if (obj is MarioObject)
                {
                    if (collisionType == 1 || collisionType == 5 || collisionType == 8)
                    {
                        audio.PlaySound("kick");
                        enemyState.TakeDamage();
                    }
                    _objectsToNotCollide.Add(obj);
                } else if (obj is BlockObject)
                {
                    if (collisionType == 1)
                    {
                        _velocity.Y = 0;
                        directionsBlocked[0] = true;
                    }
                    else if (collisionType == 2 || collisionType == 4)
                    {
                        if (collisionType == 2)
                        {
                            directionsBlocked[1] = true;
                        } else
                        {
                            directionsBlocked[3] = true;
                        }
                        _velocity.X *= -1;
                    }
                    else if (collisionType == 3)
                    {
                        _velocity.Y = 0;
                        directionsBlocked[2] = true;
                    }
                    else
                    {
                        cornerCollidedBlocks.Add(obj, collisionType);
                    }
                } else if (obj is ItemObject)
                {
                    if (((ItemObject)obj).State.Equals("pipe"))
                    {
                        if (collisionType == 1 || collisionType == 3)
                        {
                            _velocity.Y = 0;
                        } else if (collisionType == 2 || collisionType == 4)
                        {
                            _velocity.X *= -1;
                        }
                    } else
                    {
                        _objectsToNotCollide.Add(obj);
                    }
                } else if (obj is EnemyObject)
                {
                    if (collisionType == 1 || collisionType == 3)
                    {
                        _velocity.Y = 0;
                    } else if (collisionType == 2 || collisionType == 4)
                    {
                        _velocity.X *= -1;
                    }
                } else
                {
                    _objectsToNotCollide.Add(obj);
                }
            }
            foreach (AbsObject obj in cornerCollidedBlocks.Keys)
            {
                if (cornerCollidedBlocks[obj] == 5 && !directionsBlocked[0] && !directionsBlocked[1])
                {
                    _velocity.X = 0;
                }
                else if (cornerCollidedBlocks[obj] == 6 && !directionsBlocked[1] && !directionsBlocked[2])
                {
                    _velocity.Y = 0;
                }
                else if (cornerCollidedBlocks[obj] == 7 && !directionsBlocked[2] && !directionsBlocked[3])
                {
                    _velocity.Y = 0;
                }
                else if (cornerCollidedBlocks[obj] == 8 && !directionsBlocked[3] && !directionsBlocked[0])
                {
                    _velocity.X = 0;
                }
            }
        }
        */
    }

}
