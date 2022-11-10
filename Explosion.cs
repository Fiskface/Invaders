using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Explosion : Entity
    {
        private float timeUntilNextFrame = 0.1f;
        private int frame = 0;
        private static IntRect firstFrame = new IntRect(0, 0, 50, 50);
        private static IntRect secondFrame = new IntRect(50, 0, 50, 50);
        private static IntRect thirdFrame = new IntRect(100, 0, 50, 50);
        private IntRect[] explosionSprites = new IntRect[] { firstFrame, secondFrame, thirdFrame };
        private Explosion() : base("explosions"){}

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = explosionSprites[frame];
            sprite.Origin = new Vector2f(25, 25);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            timeUntilNextFrame -= deltaTime;
            if (timeUntilNextFrame <= 0)
            {
                timeUntilNextFrame = 0.1f;
                if (frame < explosionSprites.Length-1)
                {
                    frame++;
                    sprite.TextureRect = explosionSprites[frame];
                }
                else
                {
                    Dead = true;
                }
            }
        }

        public static void SpawnExplosion(float x, float y, Scene scene)
        {
            Explosion explosion = new Explosion();
            explosion.Position = new Vector2f(x, y);
            scene.Spawn(explosion);
        }
    }
}