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
    class RightFallingState : AbsMovementState
    {
        public RightFallingState(AbsAvatarObject avatar)
            : base(avatar)
        {
            Console.WriteLine("rght falling");
            avatar._velocity.X = 1.25f;
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
            avatar.movementState = new RightIdleFallingState(avatar);
        }
        public override void Right()
        {

        }
        public override void Update()
        {
            if (avatar.Velocity.Y == 0)
            {
                avatar.movementState = new RightWalkingState(avatar);
            }
        }

    }
}
