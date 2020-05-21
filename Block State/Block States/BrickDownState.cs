using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class BrickDownState : IBlockState
    {
        public BrickDownState(BlockObject block)
        {
            block.Velocity = new Microsoft.Xna.Framework.Vector2(0, 0);
        }

        public void Bump()
        {
            throw new NotImplementedException();
        }

        public void Reveal()
        {
            throw new NotImplementedException();
        }

        public void Use()
        {
            throw new NotImplementedException();
        }
    }
}