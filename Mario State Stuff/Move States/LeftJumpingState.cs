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
    class LeftJumpingState : AbsMovementState
    {
        public LeftJumpingState(AbsAvatarObject avatar)
            : base(avatar)
        {
            // Console.WriteLine("left jumping");
            avatar._velocity.X = -1.25f;
            if (avatar.isGrounded)
                avatar._velocity.Y = -2.6f;
            avatar.isGrounded = false;
        }
        public override void Up()
        {

        }

        public override void Down()
        {
            avatar.movementState = new LeftWalkingState(avatar);
        }
        public override void Left()
        {

        }
        public override void Right()
        {
            avatar.movementState = new LeftJumpingIdleState(avatar);
        }
        public override void Update()
        {
        }

    }
}
