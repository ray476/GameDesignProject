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
    class SuperCommand : ICommand
    {
        AbsAvatarObject recievier;

        public SuperCommand(AbsAvatarObject recieve)
        {
            this.recievier = recieve;
        }

        public void Execute()
        {
            recievier.Super();
        }
    }
}
