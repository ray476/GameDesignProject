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
    class QuitCommand : ICommand
    {
        

        public QuitCommand()
        {
            
        }

        public void Execute()
        {
            GameStateManager.GetInstance().Clear();
        }
    }
}
