using System;

namespace Invaders
{
    public class Spawner
    {
        private bool firstTime = true, dontHarder = false;
        private float maxSpawnTimer = 5f, currentSpawnTimer = 5f, timeUntilHarder = 8f;
        private int amountOfEnemiesSpawned = 1, amountOfHarder = 0;


        public void SpawnerFunction(Scene scene, float deltaTime)
        {
            if (firstTime)
            {
                GUI gui = new GUI();
                scene.Spawn(gui);
                Player player = new Player();
                scene.Spawn(player);
                firstTime = false;
            }
            else
            {
                if (currentSpawnTimer <= 0)
                {
                    SpawnEnemies(scene);
                    currentSpawnTimer = maxSpawnTimer;
                }
                else
                {
                    currentSpawnTimer -= deltaTime;
                }
            }
            if(!dontHarder) MakeItHarder(deltaTime);
        }

        private void SpawnEnemies(Scene scene)
        {
            for (int i = 0; i < amountOfEnemiesSpawned; i++)
            {
                Enemy enemy = new Enemy();
                scene.Spawn(enemy);
            }
        }

        private void MakeItHarder(float deltaTime)
        {
            timeUntilHarder -= deltaTime;
            if (timeUntilHarder <= 0)
            {
                timeUntilHarder = 8f;
                amountOfHarder++;
                if (amountOfHarder % 4 == 0)
                {
                    if (amountOfEnemiesSpawned < 5)
                    {
                        amountOfEnemiesSpawned++;
                    }
                    else dontHarder = true;
                }
                else
                {
                    maxSpawnTimer -= 0.1f;
                }
            }
        }

        public void Restart()
        {
            dontHarder = false;
            firstTime = true;
            maxSpawnTimer = currentSpawnTimer = 5f;
            timeUntilHarder = 8f;
            amountOfHarder = 0;
            amountOfEnemiesSpawned = 1;
        }
    }
}