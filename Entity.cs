using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Entity
    {
        private readonly string textureName;
        protected readonly Sprite sprite;
        public bool Dead;
        public bool allied;

        protected Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }

        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }

        public virtual FloatRect Bounds => sprite.GetGlobalBounds();

        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.Assets.LoadTexture(textureName);
        }

        public virtual void Destroy(Scene scene)
        {
            
        }

        public virtual void Update(Scene scene, float deltaTime)
        {
            foreach (Entity found in scene.FindIntersects(Bounds))
            {
                CollideWith(scene, found);
            }
        }
        
        protected virtual void CollideWith(Scene scene, Entity other){}

        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}