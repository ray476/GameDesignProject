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
    class UsedState : AbsBlockState
    {
        public UsedState(BlockObject block)
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
