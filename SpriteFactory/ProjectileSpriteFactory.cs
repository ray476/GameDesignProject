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
    class ProjectileSpriteFactory : ISpriteFactory
    {
        private Texture2D _brickPieceTex;

        public ProjectileSpriteFactory(ContentManager manager)
        {
            _brickPieceTex = manager.Load<Texture2D>("brick_piece");
        }

        public ISprite build(string state)
        {
            ISprite sprite;
            switch (state)
            {
                case "brickPiece":
                    sprite = new SpriteAnimated(_brickPieceTex, 1, 4, 8, true);
                    break;
                default:
                    sprite = null;
                    break;
            }
            return sprite;
        }
    }
}
