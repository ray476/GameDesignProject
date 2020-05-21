using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    abstract class AbsBlockState : IBlockState
    {
        public BlockObject block;
        public IBlockFactory factory;
        public AbsBlockState(BlockObject block)
        {
            this.block = block;
            factory = new BlockSpriteFactory(block.content);
            block.Sprite = factory.build(block.Position, this);
        }

        public abstract void Use();

        public abstract void Bump();

        public abstract void Reveal();

    }
}