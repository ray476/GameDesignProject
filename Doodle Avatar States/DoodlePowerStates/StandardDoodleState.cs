﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test

{
    class StandardDoodleState : AbsPowerStateDoodle
    {
        public StandardDoodleState(DoodleObject avatar)
            : base(avatar)
        {
            avatar.Hitbox = new BoundingBox(new Vector3(avatar.Position.X, avatar.Position.Y, 0),
            new Vector3(avatar.Position.X + 55, avatar.Position.Y + 55, 0));

        }
        

        public override void ToCannon()
        {
            
        }

        

        public override void ToDouble()
        {
            avatar.powerState = new DoubleState(avatar);
        }

      

        public override void ToFast()
        {
            avatar.powerState = new SpeedState(avatar);
        }

       

        
    }
}
