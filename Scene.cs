using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Scene
    {
        private List<Entity> _entities = new List<Entity>();
        public readonly Spawner Spawner = new Spawner();
        public readonly AssetManager Assets = new AssetManager();
        public readonly EventManager Events = new EventManager();
        public readonly Background Background = new Background();

        public void Spawn(Entity entity)
        {
            _entities.Add(entity);
            entity.Create(this);
        }

        public void Clear()
        {
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                Entity entity = _entities[i];
                _entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }

        public void UpdateAll(float deltaTime)
        {
            Spawner.SpawnerFunction(this, deltaTime);

            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                Entity entity = _entities[i];
                entity.Update(this, deltaTime);
            }

            Events.CallEvents(this);

            for (int i = 0; i < _entities.Count;)
            {
                Entity entity = _entities[i];
                if (entity.Dead)
                {
                    if (entity is Enemy)
                    {
                        Explosion.SpawnExplosion(entity.Position.X, entity.Position.Y, this);
                    }
                    _entities.RemoveAt(i);
                }
                else i++;
            }
        }

        public void RenderAll(RenderTarget target, float deltaTime)
        {
            Background.ManageBackground(this, deltaTime, target);
            
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                _entities[i].Render(target);
            }
            //foreach (var entity in _entities) entity.Render(target);
        }
        
        public IEnumerable<Entity> FindIntersects(FloatRect bounds)
        {
            int lastEntity = _entities.Count - 1;

            for (int i = lastEntity; i >= 0; i--)
            {
                Entity entity = _entities[i];
                if (entity.Dead) continue;
                if (entity.Bounds.Intersects(bounds))
                    yield return entity;
            }
        }

        public void Restart()
        {
            Clear();
            Spawner.Restart();
            
        }
    }
}