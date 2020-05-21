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
    class LeftIdleFallingState : AbsMovementState
    {
        public LeftIdleFallingState(AbsAvatarObject avatar)
            : base(avatar)
        {
            Console.WriteLine("left idle falling");
            avatar._velocity.X = 0;
            avatar.isGrounded = false;
        }
        public override void Up()
        {
        }

        public override void Down()
        {

        }
        public override void Left()
        {
            avatar.movementState = new LeftFallingState(avatar);
        }
        public override void Right()
        {
            avatar.movementState = new RightIdleFallingState(avatar);
        }
        public override void Update()
        {
            if (Math.Abs(avatar.Velocity.Y) == 0)
            {
                avatar.movementState = new LeftIdleState(avatar);
            }
        }

    }
}
