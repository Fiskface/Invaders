using System;
using System.Net.NetworkInformation;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Enemy : Entity
    {
        private float speed, shootTimer;
        private int direction;
        private Vector2f movement;
        Random rnd = new Random();
        
        public Enemy() : base("spritesheet"){}

        public override FloatRect Bounds
        {
            get
            {
                FloatRect bounds = base.Bounds;
                bounds.Left += 5;
                bounds.Width -= 10;
                bounds.Top += 5;
                bounds.Height -= 15;
                return bounds;
            }
        }
        public override void Create(Scene scene)
        {
            base.Create(scene);
            allied = false;
            speed = rnd.Next(100, 300);
            direction = rnd.Next(-50, 51);
            sprite.Rotation = direction;
            movement = new Vector2f(-MathF.Sin(ConvertToRadians(direction))*speed, MathF.Cos(ConvertToRadians(direction))*speed);
            sprite.TextureRect = new IntRect(120, 604, 104, 84);
            sprite.Origin = new Vector2f(104/2, 84/2);
            Position = new Vector2f(rnd.Next(60, 740), -sprite.Origin.Y-20);
            //Fix position to be random in x and over the screen. 
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            Position += movement * deltaTime;
            OutOfBounds();
            if(ShouldIShoot(deltaTime)) Bullet.SpawnABullet(false, direction, Position.X, Position.Y, scene);
        }

        private void OutOfBounds()
        {
            if(Position.X < sprite.Origin.X)
            {
                Position = new Vector2f(sprite.Origin.X, Position.Y);
                OutsideX();
            }
            else if(Position.X > 600 - sprite.Origin.X)
            {
                Position = new Vector2f(600 - sprite.Origin.X, Position.Y);
                OutsideX();
            }
            if (Position.Y > 800 + sprite.Origin.Y)
            {
                Position = new Vector2f(Position.X, -sprite.Origin.Y-20);
            }
        }

        private bool ShouldIShoot(float deltaTime)
        {
            if (shootTimer <= 0)
            {
                shootTimer = 0.50f;
                shootTimer += rnd.Next(200) / 100;
                return true;
            }

            shootTimer -= deltaTime;
            return false;
        }

        private void OutsideX()
        {
            direction *= -1;
            sprite.Rotation = direction;
            movement = new Vector2f(-movement.X, movement.Y);
        }
        private float ConvertToRadians(int degrees)
        {
            return (MathF.PI / 180) * direction;
        }
        
        protected override void CollideWith(Scene scene, Entity other)
        {
            base.CollideWith(scene, other);
            if (!Dead && other is Bullet && other.allied)
            {
                other.Dead = true;
                Dead = true;
                scene.Events.PublishScoreGained(10);
            }
        }
    }
}