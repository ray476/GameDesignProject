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
    class SmallState : AbsPowerState
    {
        public SmallState(AbsAvatarObject avatar)
            : base(avatar)
        {
            avatar.Hitbox = new BoundingBox(new Vector3(avatar.Position.X + 2, avatar.Position.Y, 0),
                new Vector3(avatar.Position.X + 14, avatar.Position.Y + 16, 0)); 
        }
        public override void TakeDamage()
        {
            avatar.movementState = new DeadState(avatar);
        }

        public override void ToSuper()
        {
            // is it necessary to make a new vector2 each time?
            avatar.Displace(0, -16);
            avatar.powerUpState = new SuperState(avatar);
        }

        public override void ToFire()
        {
            avatar.Displace(0, -16);
            avatar.powerUpState = new FireState(avatar);
        }

        public override void ToSmall()
        {

        }
    }
}
