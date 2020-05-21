using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    interface IBlockFactory
    {
        ISprite build(Vector2 position, IBlockState state);
    }
}