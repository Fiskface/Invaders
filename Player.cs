using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Invaders
{
    public class Player : Entity
    {
        private float speed;
        private float diagonalSpeed;
        private int width = 112, height = 75;
        private float readyToShoot, invulnerable;
        public Player() : base("spritesheet"){}
        
        public override FloatRect Bounds
        {
            get
            {
                FloatRect bounds = base.Bounds;
                bounds.Left += 5;
                bounds.Width -= 10;
                bounds.Top += 20;
                bounds.Height -= 25;
                return bounds;
            }
        }
        public override void Create(Scene scene)
        {
            allied = true;
            speed = 350.0f;
            diagonalSpeed = MathF.Sqrt(speed*speed/2);
            base.Create(scene);
            sprite.TextureRect = new IntRect(112, 791, width, height);
            sprite.Origin = new Vector2f(width / 2, height / 2);
            Position = new Vector2f(300, 600);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);

            if (readyToShoot > 0) readyToShoot -= deltaTime;
            
            HandleKeyPresses(deltaTime, scene);
            OutOfBounds();
            if (invulnerable > 0) invulnerable -= deltaTime;
            else sprite.Color = Color.White;
        }

        public void HandleKeyPresses(float deltaTime, Scene scene)
        {
            Vector2f direction = new Vector2f(0, 0);
            if (Keyboard.IsKeyPressed(Right))
            {
                direction.X += 1;
            }
            if (Keyboard.IsKeyPressed(Left))
            {
                direction.X -= 1;
            }
            if (Keyboard.IsKeyPressed(Up))
            {
                direction.Y -= 1;
            }

            if (Keyboard.IsKeyPressed(Down))
            {
                direction.Y += 1;
            }
            if (Keyboard.IsKeyPressed(Space))
            {
                if (readyToShoot <= 0)
                {
                    Bullet.SpawnABullet(true, 0, Position.X - width/3, Position.Y, scene);
                    Bullet.SpawnABullet(true, 0, Position.X + width/3, Position.Y, scene);
                    readyToShoot = 1f;
                }
            }

            //If it's going diagonal, move less in both X and Y
            if (direction.X != 0 && direction.Y != 0)
            {
                Position += direction * diagonalSpeed * deltaTime;
            }
            //Else move full speed in one of the axes
            else
            {
                Position += direction * speed * deltaTime;
            }
        }

        private void OutOfBounds()
        {
            if (Position.X > 600 - sprite.Origin.X) Position = new Vector2f(600 - sprite.Origin.X, Position.Y);
            if (Position.X < sprite.Origin.X) Position = new Vector2f(sprite.Origin.X, Position.Y);
            if (Position.Y > 800 - sprite.Origin.Y) Position = new Vector2f(Position.X, 800 - sprite.Origin.Y);
            if (Position.Y < sprite.Origin.Y) Position = new Vector2f(Position.X, sprite.Origin.Y);
        }
        protected override void CollideWith(Scene scene, Entity other)
        {
            base.CollideWith(scene, other);
            if (!other.allied && invulnerable <= 0 && other is not (GUI or Explosion))
            {
                other.Dead = true;
                scene.Events.PublishLostHealth(1);
                invulnerable = 2.0f;
                sprite.Color = new Color(150, 150, 150, 150);
            }
        }
    }
}