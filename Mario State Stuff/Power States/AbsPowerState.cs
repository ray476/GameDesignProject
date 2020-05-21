using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
     abstract class AbsPowerState : IPowerState
    {
        public AbsAvatarObject avatar;
        public IMarioFactory factory;

        public AbsPowerState(AbsAvatarObject avatar)
        {
            this.avatar = avatar;
            factory = new MetaFactory(this, avatar.content);
            avatar.Sprite = factory.build(avatar.movementState);
        }
        public abstract void TakeDamage();

        public abstract void ToSuper();

        public abstract void ToFire();

        public abstract void ToSmall();
    }
}
