using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace template_test
{
    abstract class AbsMovementState : IMovementState
    {
        public AbsAvatarObject avatar;
        public IMarioFactory factory;

        public AbsMovementState(AbsAvatarObject avatar)
        {
            this.avatar = avatar;
            factory = new MetaFactory(avatar.powerUpState, avatar.content);
            avatar.Sprite = factory.build(this);
        }


        public abstract void Up();
        public abstract void Down();
        public abstract void Left();
        public abstract void Right();
        public abstract void Update();
    }
}
