using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class NameUpCommand : ICommand
    {
        NameObject recv;
        public NameUpCommand(NameObject player)
        {
            recv = player;
        }

        public void Execute()
        {
            recv.CharUp();
        }
    }
}
