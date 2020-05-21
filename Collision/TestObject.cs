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
    class TestObject : AbsObject
    {
        public TestObject (float xPos, float yPos, float xVel, float yVel, float xAccel, float yAccel, ContentManager content)
        {
            _sprite = new SpriteStatic(content.Load<Texture2D>("test"), true);
            _hitbox = new BoundingBox(new Vector3(xPos, yPos, 0), new Vector3(xPos + 16, yPos + 16, 0));
            _position = new Vector2(xPos, yPos);
            _velocity = new Vector2(xVel, yVel);
            _acceleration = new Vector2(xAccel, yAccel);
        }
        public void BottomCollision(List<AbsObject> collidedObjects)
        {
            Console.WriteLine("Bottom collision detected!");
            _velocity.Y *= -1;
        }

        public override void Collide(List<AbsObject> collidedObjects)
        {
            throw new NotImplementedException();
        }

        public void SideCollision(List<AbsObject> collidedObjects)
        {
            Console.WriteLine("Side collision detected!");
            _velocity.X *= -1;
        }

        public void TopCollision(List<AbsObject> collidedObjects)
        {
            Console.WriteLine("Top collision detected!");
            _velocity.Y *= -1;
        }

        public override void Update(GameTime gametime)
        {
            
        }
    }
}
