using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace template_test
{

    class MakeSuperCommand : ICommand
    {
        MarioObject mario;
        public MakeSuperCommand(MarioObject mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {

        }
    }
}