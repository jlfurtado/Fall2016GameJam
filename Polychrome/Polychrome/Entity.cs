﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Polychrome
{
    class Entity
    {
        public Texture2D Image;
        public Vector2 Position { get; set; }
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Bounds, Color.White);
        }


    }
}
