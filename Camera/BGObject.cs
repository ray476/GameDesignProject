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
    class BGObject : AbsObject
    {
        public BGObject(ContentManager content)
        {
            _sprite = new SpriteStatic(content.Load<Texture2D>("background"), true);
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
