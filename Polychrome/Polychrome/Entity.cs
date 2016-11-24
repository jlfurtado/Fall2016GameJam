using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Polychrome
{
    public class Entity
    {
        public Texture2D Image;
        public Vector2 Position { get; protected set; }
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            }
        }

        public Entity(Texture2D image, Vector2 position)
        {
            Position = position;
            Image = image;
        }

        public virtual void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch, int worldToView)
        {
            spriteBatch.Draw(Image, new Rectangle(Bounds.X - worldToView, Bounds.Y, Bounds.Width, Bounds.Height), Color.White);
        }


    }
}
