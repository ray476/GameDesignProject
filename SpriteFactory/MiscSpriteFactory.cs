using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class MiscSpriteFactory : ISpriteFactory
    {
        private Texture2D _pipeHeadTex;
        private Texture2D _pipeSectionTex;
        private Texture2D _flagTex;
        private Texture2D _castleTex;
        private Texture2D _brickPieceTex;
        private Texture2D _fireballTex;

        public MiscSpriteFactory(ContentManager manager)
        {
            _pipeHeadTex = manager.Load<Texture2D>("pipe_head");
            _pipeSectionTex = manager.Load<Texture2D>("pipe_section");
            _flagTex = manager.Load<Texture2D>("flag");
            _castleTex = manager.Load<Texture2D>("castle");
            _brickPieceTex = manager.Load<Texture2D>("brick_piece");
        }

        public ISprite build(string type)
        {
            ISprite sprite;
            switch (type)
            {
                case "pipe_head":
                    sprite = new SpriteStatic(_pipeHeadTex, true);
                    break;
                case "pipe_section":
                    sprite = new SpriteStatic(_pipeSectionTex, true);
                    break;
                case "flag":
                    sprite = new SpriteStatic(_flagTex, true);
                    break;
                case "castle":
                    sprite = new SpriteStatic(_castleTex, true);
                    break;
                case "brick_piece":
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
