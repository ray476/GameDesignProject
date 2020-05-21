using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class NameLeftCommand : ICommand
    {
        NameObject recv;
        public NameLeftCommand(NameObject player)
        {
            recv = player;
        }

        public void Execute()
        {
            recv.CursorLeft();
        }
    }
}
