using System;
using HappyUnity.Singletons;
using HappyUnity.Spawners.ObjectPools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class ExplodeManager : Singleton<ExplodeManager>
    {
        public enum ExlosionType
        {
            Big,
            Small,
            UFO,
            Ship
        }

        protected void Awake()
        {
            GenerateExplosionPools();
        }

        public GameObject explosion_Big_Prefab;
        public GameObject explosion_Small_Prefab;
        public GameObject explosion_Ship_Prefab;
        public GameObject explosion_UFO_Prefab;

        ObjectPool m_BigExplosionPool;
        ObjectPool m_SmallExplosionPool;
        GameObject shipExplosion;
        GameObject ufoExplosion;


        public void GenerateExplosionPools()
        {
            m_BigExplosionPool = ObjectPool.Build(explosion_Big_Prefab, 5, 5);
            m_SmallExplosionPool = ObjectPool.Build(explosion_Small_Prefab, 5, 5);
            
            m_BigExplosionPool.hideFlags = HideFlags.DontSave;
            m_SmallExplosionPool.hideFlags = HideFlags.DontSave;
            
            shipExplosion = Instantiate(explosion_Ship_Prefab);
            ufoExplosion = Instantiate(explosion_UFO_Prefab);
            
            shipExplosion.SetActive(false);
            ufoExplosion.SetActive(false);
        }

        public void Explode(ExlosionType type, Vector2 pos)
        {
            switch (type)
            {
                case ExlosionType.Big:
                    Spawn_AsteroidExplosion(true, pos);
                    break;
                case ExlosionType.Small:
                    Spawn_AsteroidExplosion(false, pos);
                    break;
                case ExlosionType.UFO:
                    Spawn_UFOExplosion(pos);
                    break;
                case ExlosionType.Ship:
                    Spawn_ShipExplosion(pos);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Взорвалась твоя жопа");
            }
        }
        
        void Spawn_AsteroidExplosion(bool spawnBig, Vector2 position)
        {
            Poolable explosion = spawnBig ? m_BigExplosionPool.GetRecyclable() : m_SmallExplosionPool.GetRecyclable();
            explosion.transform.position = position;
            explosion.transform.rotation = Random.rotation;
        }
        
        void Spawn_ShipExplosion(Vector2 position)
        {
            shipExplosion.transform.position = position;
            shipExplosion.transform.rotation = Random.rotation;
            shipExplosion.SetActive(true);
        }
        
        void Spawn_UFOExplosion(Vector3 position)
        {
            ufoExplosion.transform.position = position;
            ufoExplosion.transform.rotation = Random.rotation;
            ufoExplosion.SetActive(true);
        }
    }
}