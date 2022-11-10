using System;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    class Program
    {
        static void Main(string[] args)
        {
            using var window = new RenderWindow(
                new VideoMode(600, 800), "Space Invaders");
            window.Closed += (o, e) => window.Close();
            
            Scene scene = new Scene();

            Clock clock = new Clock();
            while (window.IsOpen)
            {
                window.DispatchEvents();

                float deltaTime = clock.Restart().AsSeconds();
                deltaTime = MathF.Min(deltaTime, 0.1f);
                
                scene.UpdateAll(deltaTime);

                window.Clear(new Color(5, 20, 30));

                scene.RenderAll(window, deltaTime);

                window.Display();
            }
        }
    }
}