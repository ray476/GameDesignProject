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
    class BlockSpriteFactory : IBlockFactory
    {
        private Texture2D _floorBlockTex;
        private Texture2D _brickBlockTex;
        private Texture2D _brickBlockBumpedTex;
        private Texture2D _brickBlockBrokenTex;
        private Texture2D _stairBlockTex;
        private Texture2D _usedBlockTex;
        private Texture2D _usedBlockBumpedTex;
        private Texture2D _questionBlockTex;
        private Texture2D _invisibleBlockTex;
        private Texture2D _brickBlockParticleTex;
        private ContentManager _content;

        public BlockSpriteFactory(ContentManager manager)
        {
            _floorBlockTex = manager.Load<Texture2D>("block_floor");
            _brickBlockTex = manager.Load<Texture2D>("block_brick");
            _brickBlockBumpedTex = manager.Load<Texture2D>("block_brick_bumped");
            _brickBlockBrokenTex = manager.Load<Texture2D>("block_brick_broken");
            _stairBlockTex = manager.Load<Texture2D>("block_stair");
            _usedBlockTex = manager.Load<Texture2D>("block_used");
            _usedBlockBumpedTex = manager.Load<Texture2D>("block_used_bumped");
            _questionBlockTex = manager.Load<Texture2D>("block_question");
            _invisibleBlockTex = manager.Load<Texture2D>("block_invisible");
            _brickBlockParticleTex = manager.Load<Texture2D>("block_brick_particle");
            _content = manager;
        }

        public ISprite build(Vector2 position, IBlockState state)
        {

            // ended up added a new factory to make the objects.  debating between trying to merge them together.  The problem i could
            // see happening there would be the inability to change the sprite in the object when the state changes without making a whole
            // new object. 

            ISprite sprite;
            if (state is BrickState || state is null)
                sprite = new SpriteStatic(_brickBlockTex, true);
            else if (state is BrickBumpedState)
                sprite = new SpriteAnimated(_brickBlockBumpedTex, 1, 7, 7, true);
            else if (state is BrickBrokenState)
                sprite = new SpriteAnimated(_brickBlockBrokenTex,  1, 3, 4, true);
            else if (state is StairState)
                sprite = new SpriteStatic(_stairBlockTex,  true);
            else if (state is UsedState)
                sprite = new SpriteStatic(_usedBlockTex,  true);
            else if (state is UsedBumpedState)
                sprite = new SpriteAnimated(_usedBlockBumpedTex,  1, 7, 14, true);
            else if (state is QuestionState)
                sprite = new SpriteAnimated(_questionBlockTex,  1, 3, 4, true);
            else if (state is HiddenState)
                sprite = new SpriteStatic(_invisibleBlockTex,  true);
            else if (state is FloorState)
                sprite = new SpriteStatic(_floorBlockTex,  true);
            else
                sprite = new SpriteStatic(_brickBlockTex,  true);

            //ISprite sprite;
            //Console.WriteLine(state);
            //char[] separators = new char[] { '.' };
            //string test = state.Split(separators, StringSplitOptions.RemoveEmptyEntries)[1];
            //Console.WriteLine(test);
            //switch (test)
            //{
            //    case "BrickState":
            //        sprite = new SpriteStatic(_brickBlockTex, true);
            //        break;
            //    case "BrickBumpedState":
            //        sprite = new SpriteStatic(_brickBlockTex, true);
            //        break;
            //    case "BrickBrokenState":
            //        sprite = new SpriteAnimated(_brickBlockBrokenTex, 1, 3, 4, true);
            //        break;
            //    case "StairState":
            //        sprite = new SpriteStatic(_stairBlockTex, true);
            //        break;
            //    case "UsedState":
            //        sprite = new SpriteStatic(_usedBlockTex, true);
            //        break;
            //    case "UsedBumpedState":
            //        sprite = new SpriteAnimated(_usedBlockBumpedTex, 1, 7, 14, true);
            //        break;
            //    case "QuestionState":
            //        sprite = new SpriteAnimated(_questionBlockTex, 1, 3, 4, true);
            //        break;
            //    case "HiddenState":
            //        sprite = new SpriteStatic(_invisibleBlockTex, true);
            //        break;
            //    case "FloorState":
            //        sprite = new SpriteStatic(_floorBlockTex, true);
            //        break;
            //    default:
            //        sprite = new SpriteStatic(_brickBlockTex, true);
            //        break;
            //}

            return sprite;
        }
    }
}
