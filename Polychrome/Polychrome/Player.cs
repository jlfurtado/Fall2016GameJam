using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Polychrome
{
    public class Player : Entity
    {
        public int[] RGBLevels { get; set; }
        private const float playerSpeed = 500.0f;
        private const float maximumVerticalSpeed = 500.0f;
        private const float jumpSpeed = 750.0f;
        private const float gravity = 1000.0f;
        private const int maxJumps = 2;
        private int currentJumps = maxJumps;
        private Vector2 velocity;
        

        public Player(Texture2D image, Vector2 startPos) : base(image, startPos)
        {
            RGBLevels = new int[3];
        }

        public override void Update(GameTime gameTime)
        {
            float horizDir = GetHorizontalDirection();
            velocity.X = horizDir * playerSpeed;
            velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (velocity.Y > maximumVerticalSpeed) { velocity.Y = maximumVerticalSpeed; }
            Vector2 movementAmount = velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (World.IntersectsTile((new Rectangle(Bounds.X, Bounds.Y + 1, Bounds.Width, Bounds.Height))))
            {
                currentJumps = maxJumps;
            }
            else
            {
                if (currentJumps == maxJumps) { currentJumps--; }
            }

            if (InputManager.KeyPressed(Keys.Space) && currentJumps > 0)
            {
                currentJumps--;
                velocity.Y = -jumpSpeed;
            }

            if (velocity.Y < 0 && !InputManager.CurrentState.IsKeyDown(Keys.Space)) { velocity.Y = MathHelper.Max(velocity.Y, -jumpSpeed * 0.5f); }

            if (World.IntersectsTile(new Rectangle(Bounds.X + (int)movementAmount.X, Bounds.Y, Bounds.Width, Bounds.Height)))
            {
                while (!World.IntersectsTile(new Rectangle(Bounds.X + Math.Sign((int)movementAmount.X), Bounds.Y, Bounds.Width, Bounds.Height)))
                {
                    Position = new Vector2(Position.X + Math.Sign((int)movementAmount.X), Position.Y);
                    velocity.X = 0.0f;
                }
            }
            else
            {
                Position = new Vector2(Position.X + (int)movementAmount.X, Position.Y);
            }

            if (World.IntersectsTile(new Rectangle(Bounds.X, Bounds.Y + (int)movementAmount.Y, Bounds.Width, Bounds.Height)))
            {
                while (!World.IntersectsTile(new Rectangle(Bounds.X, Bounds.Y + Math.Sign((int)movementAmount.Y), Bounds.Width, Bounds.Height)))
                {
                    Position = new Vector2(Position.X, Position.Y + Math.Sign((int)movementAmount.Y));
                    velocity.Y = 0.0f;
                }
            }
            else
            {
                Position = new Vector2(Position.X, Position.Y + (int)movementAmount.Y);
            }

            base.Update(gameTime);
        }

        private float GetHorizontalDirection()
        {
            float right = InputManager.CurrentState.IsKeyDown(Keys.D) ? 1.0f : 0.0f;
            float left = InputManager.CurrentState.IsKeyDown(Keys.A) ? -1.0f : 0.0f;
            return right + left;
        }
    }
}
