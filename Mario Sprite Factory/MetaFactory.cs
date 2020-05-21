using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;




namespace template_test
{ 
    class MetaFactory : IMarioFactory
    {
        ISprite product;
        IMarioFactory localFactory;

        public MetaFactory(IPowerState powerState, ContentManager manager)
        {
            //this might not work fyi.  consider using pState.GetType() == typeof(SmallState)
            // as a possible fix.  powerUpstate should only be null during avatar initialization
            if (powerState is SmallState || powerState is null)
            {
                localFactory = new SmallMarioFactory(manager);
            } else if (powerState is SuperState)
            {
                localFactory = new SuperMarioFactory(manager);
            } else if (powerState is FireState)
            {
                localFactory = new FireMarioFactory(manager);
            }

        }
        public ISprite build(IMovementState mState)
        {
            product = localFactory.build(mState);
            if(product == null)
            {
                Console.WriteLine("help");
            }
            return product;
        }

    }
}
