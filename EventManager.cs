namespace Invaders
{
    public delegate void ValueChangedEvent(Scene scene, int value);
    public class EventManager
    {
        public event ValueChangedEvent LoseHealth;
        public event ValueChangedEvent GainScore;
        
        private int healthLost { get; set; }
        private int scoreGained { get; set; }
        
        public void PublishLostHealth(int amount) => healthLost += amount;
        public void PublishScoreGained(int amount) => scoreGained += amount;

        public void CallEvents(Scene scene)
        {
            if (healthLost != 0)
            {
                LoseHealth?.Invoke(scene, healthLost);
                healthLost = 0;
            }

            if (scoreGained != 0)
            {
                GainScore?.Invoke(scene, scoreGained);
                scoreGained = 0;
            }
        }
    }
}