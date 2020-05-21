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
    class PlatformFactory 
    {
        private Texture2D _4Platform;


        public PlatformFactory(ContentManager manager)
        {
            _4Platform = manager.Load<Texture2D>("used_platform");
 
        }

        public ISprite build(Vector2 position, AbsPlatState state)
        {


            ISprite sprite;
            if (state is NonMovingPlatformState || state is null)
            {
                sprite = new SpriteStatic(_4Platform, true);
            } else
            {
                //some default behavior needed
                sprite = new SpriteStatic(_4Platform, true);

            }

            return sprite;
        }
    }
}
