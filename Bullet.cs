using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Bullet : Entity
    {
        private float speed;
        private int direction;
        private Vector2f movement;

        private Bullet() : base("spritesheet"){}
        
        public override void Create(Scene scene)
        {
            base.Create(scene);
            speed = 500;
            sprite.Rotation = direction;
            if (allied)
            {
                movement = new Vector2f(0, -speed);
                sprite.TextureRect = new IntRect(856, 57, 9, 37);
                sprite.Origin = new Vector2f(9/2, 37/2);
                Position += new Vector2f(0, -16);
            }
            else
            {
                movement = new Vector2f(-MathF.Sin(ConvertToRadians(direction)) * speed,
                    MathF.Cos(ConvertToRadians(direction)) * speed);
                sprite.TextureRect = new IntRect(856, 602, 9, 37);
                sprite.Origin = new Vector2f(9/2, 37/2);
                Position += movement * 0.08f;
            }
            
        }

        public override void Update(Scene scene, float deltaTime)
        {
            Position += movement * deltaTime;
            
            KillBullet();
        }

        private float ConvertToRadians(int degrees)
        {
            return (MathF.PI / 180) * direction;
        }
        
        public static void SpawnABullet(bool allied, int direction, float x, float y, Scene scene)
        {
            Bullet bullet = new Bullet();
            bullet.Position = new Vector2f(x, y);
            bullet.direction = direction;
            bullet.allied = allied;
            scene.Spawn(bullet);
        }

        private void KillBullet()
        {
            if (Position.X is > 650 or < -50 || Position.Y is > 850 or < -50)
            {
                Dead = true;
            }
        }
    }
}