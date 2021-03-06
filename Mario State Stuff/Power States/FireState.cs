﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace template_test
{
    class FireState : AbsPowerState
    {
        public FireState(AbsAvatarObject avatar)
            : base(avatar)
        {
            avatar.Hitbox = new BoundingBox(new Vector3(avatar.Position.X, avatar.Position.Y, 0),
                new Vector3(avatar.Position.X + 16, avatar.Position.Y + 32, 0));
        }
        public override void TakeDamage()
        {
            avatar.powerUpState = new SuperState(avatar);
        }

        public override void ToSuper()
        {
            avatar.powerUpState = new SuperState(avatar);
        }

        public override void ToFire()
        {
            
        }

        public override void ToSmall()
        {
            if (avatar.movementState is LeftCrouchingState)
            {
                avatar.movementState = new LeftIdleState(avatar);
            }
            else if (avatar.movementState is RightCrouchingState)
            {
                avatar.movementState = new RightIdleState(avatar);
            }
            avatar.Displace(0, 16);
            avatar.powerUpState = new SmallState(avatar);
        }
    }
}
