using System;
using Asteroids;
using HappyUnity.Spawners.ObjectPools;
using Unity.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class MachineGunBehaviour : Weapon
    {
        public float maxSpeed;
        /// the number of projectiles to spawn per shot
        public int ProjectilesPerShot = 1;

        private AudioSource source;
        /// the object pooler used to spawn projectiles
        private ObjectPool ObjectPooler { get; set; }

        public GameObject bulletPrefab;

        private Transform muzzle;
        

        /// <summary>
        /// Initialize this weapon
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            muzzle = owner.transform.Find("BulletSpawnPoint");
            source = GetComponent<AudioSource>();
            ObjectPooler = ObjectPool.Build(bulletPrefab, initialClones: 40, initialCapacity: 40);
        }


        /// <summary>
        /// Called everytime the weapon is used
        /// </summary>
        public override void WeaponUse()
        {
            base.WeaponUse();

            for (int i = 0; i < ProjectilesPerShot; i++)
            {
                SpawnProjectile(muzzle.position, owner.transform.up * maxSpeed, owner.transform.rotation);
                source.Play();
            }
        }

        /// <summary>
        /// Spawns a new object and positions/resizes it
        /// </summary>
        public virtual GameObject SpawnProjectile(Vector2 spawnPosition, Vector2 velocity, Quaternion rotation)
        {
            var bullet = ObjectPooler.GetRecyclable<BulletBehaviour>();
            bullet.Fire(spawnPosition, rotation, velocity);
            return bullet.gameObject;
        }
        
    }
}