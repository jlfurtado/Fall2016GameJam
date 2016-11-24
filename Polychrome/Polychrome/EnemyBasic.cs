using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Polychrome
{
    class EnemyBasic : Entity
    {
        public RGBEnum EnemyColor { get; private set; }

        public void AddColor(RGBEnum addColor)
        {
            // TODO: Update to match design decision
            if ((addColor | EnemyColor) == RGBEnum.Composite)
            {
                return;
            }
            else
            {
                EnemyColor = addColor | EnemyColor;
            }
        }

        public EnemyBasic(Texture2D image, Vector2 startPos) : base(image, startPos)
        {

        }
    }
}
