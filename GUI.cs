using System;
using System.Buffers.Text;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class GUI : Entity
    {
        private Text scoreText = new Text();
        private Font font;
        private int currentHealth, currentScore = 0, maxHealth = 3, killScore = 0;
        private float timeScore = 0f;

        public GUI() : base ("spritesheet") {}
        
        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(465, 991, 37, 26);
            font = scene.Assets.LoadFont("kenvector_future");
            
            sprite.Origin = new Vector2f(37 / 2, 26 / 2);
            sprite.Rotation = 0;
            scoreText.DisplayedString = "Score";
            currentHealth = maxHealth;

            scene.Events.LoseHealth += OnLoseHealth;
            scene.Events.GainScore += OnGainScore;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            timeScore += deltaTime * 10;
            currentScore = killScore + Convert.ToInt32(timeScore);
        }

        public override void Destroy(Scene scene)
        {
            scene.Events.LoseHealth -= OnLoseHealth;
            scene.Events.GainScore -= OnGainScore;
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                scene.Restart();
            }
        }

        private void OnGainScore(Scene scene, int amount)
        {
            killScore += amount;
        }

        public override void Render(RenderTarget target)
        {
            sprite.Position = new Vector2f(30, 20);
            for (int i = 0; i < maxHealth; i++)
            {
                if (i >= currentHealth)
                    continue;
                base.Render(target);
                sprite.Position += new Vector2f(40, 0);
            }

            scoreText.DisplayedString = $"Score: {currentScore}";
            scoreText.Position = new Vector2f(600 - scoreText.GetGlobalBounds().Width - 10, -5);
            scoreText.FillColor = Color.White;
            scoreText.Font = font;
            scoreText.CharacterSize = 32;
            target.Draw(scoreText);
        }
    }
}