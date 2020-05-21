using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class BrickBumpedState : IBlockState
    {
        public BrickBumpedState(BlockObject block)
        {
            block.Velocity = new Microsoft.Xna.Framework.Vector2(0, -3);
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
