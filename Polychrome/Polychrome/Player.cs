using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Polychrome
{
    class Player : Entity
    {
        public int[] RGBLevels { get; set; }

        public Player(Texture2D image, Vector2 startPos) : base(image, startPos)
        {
            RGBLevels = new int[3];
        }
    }
}
