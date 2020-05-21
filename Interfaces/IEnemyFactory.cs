using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    interface IEnemyFactory
    {
        ISprite build(Vector2 position, IEnemyState state);
    }
}
