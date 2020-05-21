using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace template_test
{
    class PlatformObject : AbsObject
    {
        private float[] _xRange;
        private float[] _yRange;
        private int _updatesUntilExpire;
        private int _updatesStoodOn;
        private bool _stoodOn;
        private ContentManager _content;
        public bool keystone = false;

        public bool IsMoving { get; }

        public PlatformObject(Vector2 position, int updatesUntilExpire, ContentManager content)
        {
            IsMoving = false;
            _content = content;
            isVisible = true;
            deleteThis = false;
            _hitbox = new BoundingBox(new Vector3(position.X , position.Y, 0), new Vector3(position.X + 192, position.Y + 32, 0));
            _updatesUntilExpire = updatesUntilExpire;
            _updatesStoodOn = 0;
            _opacity = 1.0f;
            _stoodOn = false;
            _position = position;
            _velocity = Vector2.Zero;
            _acceleration = Vector2.Zero;
            _objectsToNotCollide = new List<AbsObject>();
            _objectsToAdd = new List<AbsObject>();
            _sprite = new SpriteStatic(content.Load<Texture2D>("testPlatformSprite"), true);
            hitbox_rep = content.Load<Texture2D>("hitbox_temp");
        }

        public PlatformObject(Vector2 startPos, Vector2 endPos, int updatesUntilExpire, ContentManager content)
        {
            IsMoving = true;
            _xRange = new float[] { startPos.X, endPos.X };
            _yRange = new float[] { startPos.Y, endPos.Y };
            foreach (float[] range in new float[][]{ _xRange, _yRange }){
                if (range[0] > range[1])
                {
                    float temp = range[1];
                    range[1] = range[0];
                    range[0] = temp;
                }
            }
            _content = content;
            isVisible = true;
            deleteThis = false;
            _sprite = new SpriteStatic(content.Load<Texture2D>("testPlatformSprite"), true);
            _position = startPos;
            _hitbox = new BoundingBox(new Vector3(_position.X, _position.Y, 0), new Vector3(_position.X + 192, _position.Y + 32, 0));
            _updatesUntilExpire = updatesUntilExpire;
            _updatesStoodOn = 0;
            _opacity = 1.0f;
            _stoodOn = false;
            _velocity = Vector2.Zero;
            _acceleration = Vector2.Zero;
            _objectsToNotCollide = new List<AbsObject>();
            _objectsToAdd = new List<AbsObject>();
            hitbox_rep = content.Load<Texture2D>("hitbox_temp");
        }

        public void Change_Range(Vector2 start, Vector2 end)
        {
            _xRange = new float[] { start.X, end.X };
            _yRange = new float[] { start.Y, end.Y };
            foreach (float[] range in new float[][] { _xRange, _yRange })
            {
                if (range[0] > range[1])
                {
                    float temp = range[1];
                    range[1] = range[0];
                    range[0] = temp;
                }
            }
            _velocity = Vector2.Normalize(end - start) / 2;

        }

        //public ISprite AssignSprite(ContentManager content)
        //{
        //    ISprite sprite;
        //    Random rand = new Random();
        //    int num = rand.Next(1, 4);
        //    if (num == 1)
        //    {
        //        sprite = new SpriteStatic(content.Load<Texture2D>("testPlatformSprite"), true);
        //    }
        //    else if (num == 2)
        //    {
        //        sprite = new SpriteStatic(content.Load<Texture2D>("testPlatformSprite"), true);
        //    }
        //    else
        //    {
        //        sprite = new SpriteStatic(content.Load<Texture2D>("testPlatformSprite"), true);
        //    }
        //    return sprite;
        //}
        public override void Update(GameTime gametime)
        {
            if (IsMoving && (_position.X < _xRange[0] || _position.X > _xRange[1] || _position.Y < _yRange[0] || _position.Y > _yRange[1]))
            {
                _velocity = _velocity * -1;
            }
            if (_updatesUntilExpire > 0 && _stoodOn)
            {
                _updatesStoodOn++;
                if (_updatesStoodOn > _updatesUntilExpire)
                {
                    isVisible = false;
                } else
                {
                    _opacity -= 0.9f / _updatesUntilExpire;
                }
            }
            _stoodOn = false;
            _objectsToNotCollide.Clear();
        }
        public override void Collide(List<AbsObject> collidedObjects)
        {
            Dictionary<AbsObject, Collision.CollisionType> cornerCollidedObjects = new Dictionary<AbsObject, Collision.CollisionType>();
            foreach (AbsObject obj in collidedObjects)
            {
                Collision.CollisionType type = Collision.GetCollisionType(this, obj);
                if (type == Collision.CollisionType.TSide || type == Collision.CollisionType.TRCorner || type == Collision.CollisionType.TLCorner)
                {
                    TopCollide(obj);
                    //} else if(type == Collision.CollisionType.RSide || type == Collision.CollisionType.LSide)
                    //{
                    //    SideCollide(obj);
                    //}
                }
                else
                {
                    _objectsToNotCollide.Add(obj);
                }
            }
        }

        private void TopCollide(AbsObject obj)
        {
            if (obj is DoodleObject)
            {
                _stoodOn = true;
            } else if (obj is ItemObject || obj is EnemyObject)
            {
                // No collision response
            } else
            {
                _objectsToNotCollide.Add(obj);
            }
        }

        private void SideCollide(AbsObject obj)
        {
            if(obj is PlatformObject)
            {
                Console.WriteLine("platform on platform detected");
            }
            else
            {
                _objectsToNotCollide.Add(obj);
            }
        }
    }
}
