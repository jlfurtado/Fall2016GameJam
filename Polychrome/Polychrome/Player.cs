using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polychrome
{
    class Player : Entity
    {
        public int[] RGBLevels { get; set; }

        public Player()
        {
            RGBLevels = new int[3];
        }
    }
}
