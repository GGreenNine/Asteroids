using System;
using System.Collections;
using Asteroids;
using HappyUnity.Spawners.ObjectPools;
using UnityEngine;

namespace DefaultNamespace
{
    public class LaserBehaviour : Weapon
    {
        public Vector2 LaserWidth;
        public float LaserMaxDistance = 50;
        
        private LineRenderer _line;
        private AudioSource source;

        protected Vector2 _destination;

        private Transform muzzle;

        private void Awake()
        {
            UIStorage.Instance.AmmoUi.InitializeLazerInfo(this);
            source = GetComponent<AudioSource>();
            muzzle = owner.transform.Find("BulletSpawnPoint");
            
            _line = gameObject.GetComponent<LineRenderer>();
            _line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            _line.receiveShadows = true;
            _line.startWidth = LaserWidth.x;
            _line.endWidth = LaserWidth.y;
        }

        /// </summary>
        /// Initialize this weapon
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            UIStorage.Instance.AmmoUi.InitializeLazerInfo(this);
            muzzle = owner.transform.Find("BulletSpawnPoint");
        }


        /// <summary>
        /// Called everytime the weapon is used
        /// </summary>
        public override void WeaponUse()
        {
            base.WeaponUse();
            ShootLaser();
            source.Play();

        }

        /// <summary>
        /// Draws the actual laser
        /// </summary>
        protected virtual void ShootLaser()
        {
            // our laser will be shot from the weapon's laser origin
            RaycastHit2D hit = Physics2D.Raycast(muzzle.position, muzzle.transform.up);
            

                if (hit.collider != null)
            {
                var enemy = hit.collider.GetComponent<EnemyUnit>();
                if(!enemy) return;
                enemy.HitByLaser();
            }

            // if we've hit something, our destination is the raycast hit
            if (hit.transform != null)
            {
                   _destination = (hit.point);
            }
            // otherwise we just draw our laser in front of our weapon 
            else
            {
                _destination = (muzzle.transform.up * LaserMaxDistance);
                //_destination = MMMaths.RotatePointAroundPivot (_destination, _weapon.transform.position, _weapon.transform.rotation);
            }

            // we set our laser's line's start and end coordinates
            _line.SetPosition(0, muzzle.transform.position);
            _line.SetPosition(1, _destination);
        }

        public override void CaseWeaponUse()
        {
            base.CaseWeaponUse();
            _line.enabled = true;
            OnAmmoChanged();
            StartCoroutine(LaserDisableTimer());
        }


        IEnumerator LaserDisableTimer()
        {
            yield return new WaitForSeconds(0.3f);
            _line.enabled = false;
        }


        
    }
}