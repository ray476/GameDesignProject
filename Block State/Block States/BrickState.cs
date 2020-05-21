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
    class BrickState : AbsBlockState
    {
        public BrickState(BlockObject block)
            : base(block)
        {
            block.Velocity = new Vector2(0);

        }

        public override void Use()
        {
        }

        public override void Bump()
        {
            block.blockMoveState = new BrickBumpedState(block);
            if (block.items.Count == 0 && block.hasItems)
            {
                block.blockState = new UsedState(block);
            }
        }

        public override void Reveal()
        {
        }
    }
}
