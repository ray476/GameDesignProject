using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace template_test
{
    class QuestionState : AbsBlockState
    {
        public QuestionState(BlockObject block)
            : base(block)
        {

        }
        public override void Use()
        {
            block.blockState = new UsedState(block);
        }

        public override void Bump()
        {
            block.blockMoveState = new BrickBumpedState(block);
            block.blockState = new UsedState(block);
        }

        public override void Reveal()
        {
        }
    }
}