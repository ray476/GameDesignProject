using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    interface IDoodleSpriteFactory
    {
        ISprite build(AbsDoodleMoveState moveState);
    }
}
