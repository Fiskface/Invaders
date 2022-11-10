using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Background
    {
        private List<Vector2f> listOfPositions = new List<Vector2f>();
        private float speed = 100;
        private Sprite sprite = new Sprite();
        private bool firstTime = true;
        public Background()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    listOfPositions.Add(new Vector2f(x*256,y*255));
                }
            }
            
        }

        public void ManageBackground(Scene scene, float deltaTime, RenderTarget target)
        {
            if (firstTime)
            {
                sprite.Texture = scene.Assets.LoadTexture("purple");
                firstTime = false;
            }
            for (int i = 0; i < listOfPositions.Count; i++)
            {
                listOfPositions[i] = new Vector2f(listOfPositions[i].X, listOfPositions[i].Y + speed * deltaTime);
                if (listOfPositions[i].Y > 800)
                {
                    listOfPositions[i] = new Vector2f(listOfPositions[i].X, listOfPositions[i].Y - (800 + 256));
                }
                sprite.Position = listOfPositions[i];
                target.Draw(sprite);
            }
        }
    }
}