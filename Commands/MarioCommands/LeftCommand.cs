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
    class LeftCommand : ICommand
    {
        AbsAvatarObject reciever;

        public LeftCommand(AbsAvatarObject recieve)
        {
            this.reciever = recieve;
        }

        public void Execute()
        {
            reciever.Left();
        }
    }
}
