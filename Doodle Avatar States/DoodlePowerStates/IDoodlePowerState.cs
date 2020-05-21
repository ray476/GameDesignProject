using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test.Doodle_Avatar_States.DoodlePowerStates
{
    interface IDoodlePowerState
    {
        void ToPropeller(DoodleObject avatar);
        void ToCannon(DoodleObject avatar);
        void ToFast(DoodleObject avatar);
        void ToDouble(DoodleObject avatar);
    }
}
