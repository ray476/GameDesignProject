using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class NameDownCommand : ICommand
    {
        NameObject recv;
        public NameDownCommand(NameObject player)
        {
            recv = player;
        }

        public void Execute()
        {
            recv.CharDown();
        }
    }
}
