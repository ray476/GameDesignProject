using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class AbsPlatState
    {
        public PlatformObject platform;
        public PlatformFactory factory;

        public AbsPlatState(PlatformObject platform)
        {
            this.platform = platform;
            // factory = new PlatformFactory(platform.content);
            this.platform.Sprite = factory.build(platform.Position, this);
        }
    }
}
