using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polychrome
{
    public class Level
    {
        public Texture2D Background { get; private set;}
        public Tile[] Tiles { get; private set; }
        public Entity[] Entities { get; private set; }
        public Player Roy { get; private set; }
        public bool IsBeaten { get; private set; } = false;
        public bool IsLost { get; private set; } = false;

        public int LevelWidth { get; private set; }

        public Level(Texture2D background, string filePath, int screenWidth)
        {
            Background = background;

            XDocument doc = XDocument.Load(filePath);
            XElement root = doc.Element("XnaContent").Element("Asset").Element("Level");

            var tiles = root.Element("Tiles").Elements("Tile").Select(
                 t =>
                 new Tile(Game1.TILES[int.Parse(t.Element("ImageIndex").Value)], new Vector2(float.Parse(t.Element("X").Value), float.Parse(t.Element("Y").Value)))      
             );

            Tiles = tiles.ToArray();

            var ents = root.Element("Entities").Elements("Entity").Select(
                e => new Entity(Game1.ENEMIES[int.Parse(e.Element("ImageIndex").Value)], new Vector2(float.Parse(e.Element("X").Value), float.Parse(e.Element("Y").Value)))
                );

            Entities = ents.ToArray();

            int maxW = 0;
            foreach(Tile tile in Tiles)
            {
                if (tile.Bounds.Right > maxW) { maxW = tile.Bounds.Right; }
            }

            LevelWidth = MathHelper.Max(maxW, screenWidth);

            Roy = new Player(Game1.playerSprite, new Vector2(float.Parse(root.Element("PlayerStartX").Value),
                                                             float.Parse(root.Element("PlayerStartY").Value)));
        }

        public void Update(GameTime gameTime)
        {
            Roy.Update(gameTime);

            foreach (Entity entity in Entities)
            {
                entity.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            spriteBatch.Draw(Background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

            int d = (int)Roy.Position.X + Roy.Bounds.Width / 2 - screenWidth / 2;
            d = MathHelper.Clamp(d, 0, LevelWidth - screenWidth);
            Rectangle screenWorldRect = new Rectangle(d, 0, screenWidth, screenHeight);

            foreach (Tile tile in Tiles)
            {
                if (tile.Bounds.Intersects(screenWorldRect))
                {
                    tile.Draw(spriteBatch, d);
                }
            }

            foreach (Entity entity in Entities)
            {
                if (entity.Bounds.Intersects(screenWorldRect))
                {
                    entity.Draw(spriteBatch, d);
                }
            }

            Roy.Draw(spriteBatch, d);
        }
    }
}
