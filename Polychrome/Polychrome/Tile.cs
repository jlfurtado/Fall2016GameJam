using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Polychrome
{
    public class Tile
    {
        public Texture2D Image;
        public Vector2 Position;
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            }
        }

        public Tile(Texture2D image, Vector2 position)
        {
            Image = image;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch, int worldToView)
        {
            spriteBatch.Draw(Image, new Rectangle(Bounds.X - worldToView, Bounds.Y, Bounds.Width, Bounds.Height), Color.White);
        }
    }
}
