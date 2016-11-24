using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Polychrome
{
    class World
    {
        public bool IsGameOver { get; private set; } = false;
        public bool IsGameWon { get; private set; } = false;

        private Level[] levels;
        private int currentLevel = 0;

        public World(Texture2D[] levelBackgrounds, string[] levelFiles)
        {
            levels = new Level[levelBackgrounds.Length];

            for (int i = 0; i < levelBackgrounds.Length; ++i)
            {
                levels[i] = new Level(levelBackgrounds[i], levelFiles[i]);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (levels[currentLevel].IsBeaten) { currentLevel++; }
            else if(levels[currentLevel].IsLost) { IsGameOver = true; IsGameWon = false; }

            if (currentLevel < levels.Length) { levels[currentLevel].Update(gameTime); }
            else { IsGameOver = true; IsGameWon = true; }
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            if (currentLevel < levels.Length) { levels[currentLevel].Draw(spriteBatch, screenWidth, screenHeight); }
        }


    }
}
