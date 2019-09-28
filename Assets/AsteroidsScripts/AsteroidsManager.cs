using System.Collections.Generic;
using HappyUnity.Singletons;
using HappyUnity.Spawners.ObjectPools;
using UnityEngine;

namespace Asteroids
{
    public sealed class AsteroidsManager : Singleton<AsteroidsManager>
    {
        public ObjectPool big, small;

        List<Asteroid> asteroids = new List<Asteroid>();

        public static AsteroidsManager New(ObjectPool big, ObjectPool small)
        {
            Instance.big = big;
            Instance.small = small;
            return Instance;
        }

        public  void ShowAsteroids()
        {
            SpawnAllAsteroids(big);
            SpawnAllAsteroids(small);
        }

        public  void HideAsteroids()
        {
            foreach (var asteroid in asteroids)
                asteroid.RemoveFromGame();
            asteroids.Clear();
        }

        private void SpawnAllAsteroids(ObjectPool pool)
        {
            while (!pool.IsEmpty)
                asteroids.Add(SpawnAsteroidFromPool(pool));
        }

        private Asteroid SpawnAsteroidFromPool(ObjectPool pool)
        {
            Asteroid asteroid = pool.GetRecyclable<Asteroid>();
            asteroid.Spawn();
            return asteroid;
        }
    }
}