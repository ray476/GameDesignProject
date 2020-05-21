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
    class VertBGObject : AbsObject
    {
        public VertBGObject(ContentManager content)
        {
            _sprite = new SpriteStatic(content.Load<Texture2D>("bouncy_background_3500"), true);
            isVisible = true;
            _opacity = 1.0f;
        }

        public override void Collide(List<AbsObject> collidedObjects)
        {
        }

        public override void Update(GameTime gametime)
        {
            
        }
    }
}
