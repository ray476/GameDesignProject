using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class UsedBumpedState : AbsBlockState
    {
        public UsedBumpedState(BlockObject block)
            : base(block)
        {

        }
        public override void Use()
        {
        }

        public override void Bump()
        {
        }

        public override void Reveal()
        {
        }
    }
}
