using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polychrome
{
    class Bullet
    {
        public RGBEnum BulletColor { get; set; }

        // TODO: Update to design decision
        public Bullet(RGBEnum color = RGBEnum.Composite)
        {
            BulletColor = color;
        }
    }
}
