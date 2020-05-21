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
    class LeftJumpingIdleState : AbsMovementState
    {
        public LeftJumpingIdleState(AbsAvatarObject avatar)
            : base(avatar)
        {
            // Console.WriteLine("left idle jumping");
            avatar._velocity.X = 0;
            if (avatar.isGrounded)
                avatar._velocity.Y = -2.6f;
            avatar.isGrounded = false;
        }
        public override void Up()
        {

        }

        public override void Down()
        {
            avatar.movementState = new LeftIdleState(avatar);
        }
        public override void Left()
        {
            avatar.movementState = new LeftJumpingState(avatar);
        }
        public override void Right()
        {
            avatar.movementState = new RightJumpingIdleState(avatar);
        }
        public override void Update()
        {
        }

    }
}
