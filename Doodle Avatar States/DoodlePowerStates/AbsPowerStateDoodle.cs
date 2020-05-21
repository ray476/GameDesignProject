using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    abstract class AbsPowerStateDoodle : IPowerDoodle
    {
        public DoodleObject avatar;
        public IDoodleSpriteFactory factory;

        public AbsPowerStateDoodle(DoodleObject avatar)
        {
            this.avatar = avatar;
            factory = new DoodleMetaFactory(this, avatar.content);
            avatar.Sprite = factory.build(avatar.moveState);
        }
       

        public abstract void ToCannon();

        public abstract void ToFast();

        public abstract void ToDouble();

        public void ToPropeller()
        {
        }
    }
}
