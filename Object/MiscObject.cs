using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace template_test
{
    public class MiscObject : AbsObject
    {
        private string _type;
        private AbsAvatarObject _mario;
        private AbsObject _hiddenObj;
        private ISpriteFactory _spriteFactory;
        public bool inRange;
        public bool isTouching;

        public string Type
        {
            get { return _type; }
        }
        public AbsObject HiddenObject
        {
            set { _hiddenObj = value; }
        }
        public AbsAvatarObject Mario
        {
            get { return _mario; }
            set { _mario = value; }
        }
        public Vector2? WarpDestination { get; set; }

        public MiscObject(Vector2 startPos, ContentManager content, string type)
        {
            _opacity = 1.0f;
            isVisible = true;
            deleteThis = false;
            HiddenObject = null;
            WarpDestination = null;
            inRange = false;
            isTouching = false;
            _type = type;
            _mario = null;
            _hiddenObj = null;
            _spriteFactory = new MiscSpriteFactory(content);
            _sprite = _spriteFactory.build(type);
            _hitbox = GetHitbox(type, startPos);
            _position = startPos;
            _velocity = new Vector2(0);
            _acceleration = new Vector2(0);
            _objectsToNotCollide = new List<AbsObject>();
            _objectsToAdd = new List<AbsObject>();
        }

        public override void Update(GameTime gametime)
        {
            if (_mario != null)
            {
                if (_mario.Hitbox.Value.Intersects(_hitbox.Value))
                {
                    isTouching = true;
                }
                else
                {
                    isTouching = false;
                }
                if (_mario.Position.X > _position.X - 160 && _mario.Position.X < _position.X + 160)
                {
                    inRange = true;
                }
                else
                {
                    inRange = false;
                }
            }

        }

        public override void Collide(List<AbsObject> collidedObjects)
        {
            // No response needed...right?
        }

        private static BoundingBox? GetHitbox(string type, Vector2 pos)
        {
            BoundingBox? hitbox;
            switch (type)
            {
                case "pipe_head":
                    hitbox = new BoundingBox(new Vector3(pos.X, pos.Y, 0), new Vector3(pos.X + 32, pos.Y + 16, 0));
                    break;
                case "pipe_section":
                    hitbox = new BoundingBox(new Vector3(pos.X, pos.Y, 0), new Vector3(pos.X + 32, pos.Y + 16, 0));
                    break;
                default:
                    hitbox = null;
                    break;
            }
            return hitbox;
        }
    }
}
