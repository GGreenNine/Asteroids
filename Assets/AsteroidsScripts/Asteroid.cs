using System;
using UnityEngine;

namespace Asteroids
{
    public class Asteroid : EnemyUnit
    {
        public enum Asteroid_Type
        {
            Big,
            Small
        }

        [SerializeField] private Asteroid_Type AsteroidType;
        [SerializeField] private Asteroid_Type AsteroidSpawnType;

        [Range(0, 5)] [SerializeField] protected int fragments = 2;

        static int activeCount;

        public static bool Any => activeCount > 0;

        protected virtual void OnEnable()
        {
            ++activeCount;
        }
        
        protected virtual void OnDisable()
        {
            --activeCount;
        }
        
        protected virtual void Reset()
        {
            destructionScore = 100;
        }
        
        protected override void HitByBullet(GameObject bullet)
        {
            Division();
            base.HitByBullet(bullet);
        }

        public void Division()
        {
            for (int i = 0; i < fragments; ++i)
            {
                GameManager.SpawnAsteroid(transform.position, AsteroidSpawnType);
            }
        }
    }
}