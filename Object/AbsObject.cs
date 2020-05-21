using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    public abstract class AbsObject
    {
        
        public bool isVisible;
        public bool deleteThis;
        protected float _opacity;
        protected ISprite _sprite;
        protected BoundingBox? _hitbox;
        protected Vector2 _position;
        public Vector2 _velocity;
        protected Vector2 _acceleration;
        protected List<AbsObject> _objectsToNotCollide;
        public List<AbsObject> _objectsToAdd;
        public Texture2D hitbox_rep;

        public ISprite Sprite
        {
            get
            {
                return _sprite;
            }
            set
            {
                _sprite = value;
            }
        }
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }
        public BoundingBox? Hitbox
        {
            get
            {
                return _hitbox;
            }
            set
            {
                _hitbox = value;
            }
        }
        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }
        public Vector2 Acceleration
        {
            get
            {
                return _acceleration;
            }
        }
        public List<AbsObject> ObjectsToNotCollide
        {
            get
            {
                return _objectsToNotCollide;
            }
        }
        public List<AbsObject> ObjectsToAdd
        {
            get
            {
                return _objectsToAdd;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                _sprite.Draw(_position, _opacity, spriteBatch);
            if(hitbox_rep != null && _hitbox != null)
            {
                BoundingBox temp_box = (BoundingBox)Hitbox;
                Rectangle newRect = new Rectangle((int)temp_box.Min.X, (int)temp_box.Min.Y, (int)(temp_box.Max.X - temp_box.Min.X), (int)(temp_box.Max.Y - temp_box.Min.Y));
                spriteBatch.Draw(hitbox_rep, newRect, Color.White*50);
            }
            
        }
        public void Displace(float xDisp, float yDisp)
        {
            _position.X += xDisp;
            _position.Y += yDisp;
            if (_hitbox != null)
            {
                BoundingBox newHitbox = _hitbox.GetValueOrDefault();
                newHitbox.Min.X += xDisp;
                newHitbox.Max.X += xDisp;
                newHitbox.Min.Y += yDisp;
                newHitbox.Max.Y += yDisp;
                _hitbox = newHitbox;
            }
        }
        public void Teleport(float xCoord, float yCoord)
        {
            if (_hitbox != null)
            {
                float minXOffset = _hitbox.Value.Min.X;
                float minYOffset = _hitbox.Value.Min.Y;
                float maxXOffset = _hitbox.Value.Max.X;
                float maxYOffset = _hitbox.Value.Max.Y;
                _position.X = xCoord;
                _position.Y = yCoord;
                _hitbox = new BoundingBox(new Vector3(_position.X + minXOffset, _position.Y + minYOffset, 0), new Vector3(_position.X + maxXOffset, _position.Y + maxYOffset, 0));
            } else
            {
                _position.X = xCoord;
                _position.Y = yCoord;
            }
        }

        abstract public void Update(GameTime gametime);
        abstract public void Collide(List<AbsObject> collidedObjects);
    }
}
