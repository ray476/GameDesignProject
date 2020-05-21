using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class DoodleMetaFactory : IDoodleSpriteFactory
    {
        ISprite product;
        IDoodleSpriteFactory localFactory;

        public DoodleMetaFactory(IPowerDoodle powerState, ContentManager content)
        {
            if (powerState is StandardDoodleState || powerState == null)
            {
                localFactory = new RedDoodleFactory(content);
            } else if (powerState is DoubleState)
            {
                localFactory = new PurpleDoodleFactory(content);
            } else if (powerState is SpeedState)
            {
                localFactory = new BlueDoodleFactory(content);
            }
        }

        public ISprite build(AbsDoodleMoveState moveState)
        {
            product = localFactory.build(moveState);
            return product;
        }
    }
}
