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
    class GoombaSpriteFactory : ISpriteFactory
    {
        private Texture2D _goombaWalkTex;
        private Texture2D _goombaFlatTex;

        public GoombaSpriteFactory(ContentManager manager)
        {
            _goombaWalkTex = manager.Load<Texture2D>("goomba_walk");
            _goombaFlatTex = manager.Load<Texture2D>("goomba_flat");
        }

        public ISprite build(string state)
        {
            ISprite sprite;
            switch (state)
            {
                case "flat":
                    sprite = new SpriteStatic(_goombaFlatTex, true);
                    break;
                default:
                    sprite = new SpriteAnimated(_goombaWalkTex, 1, 2, 4, true);
                    break;
            }
            return sprite;
        }
    }
}
