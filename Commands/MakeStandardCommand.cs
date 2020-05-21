using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace template_test
{
    
    class MakeStandardCommand : ICommand
    {
        MarioObject mario;
        public MakeStandardCommand(MarioObject mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {

        }
    }
}