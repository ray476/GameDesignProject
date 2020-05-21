using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    interface IMarioFactory
    {

        ISprite build(IMovementState state);

    }
}
