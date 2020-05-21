using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class NameRightCommand : ICommand
    {
        NameObject recv;
        public NameRightCommand(NameObject player)
        {
            recv = player;
        }

        public void Execute()
        {
            recv.CursorRight();
        }
    }
}
