using System;
using UnityEngine;

namespace Asteroids
{
    public class EnemyUnit : GameUnit
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            GameObject otherObject = other.gameObject;
            
            switch (otherObject.tag)
            {
                case "Ship":
                    HitByShip(otherObject);
                    break;
                case "UFO":
                    HitByUFO(otherObject);
                    break;
            }
        }
        
        protected void HitByShip(GameObject ship)
        {
            KillWithExplosion(ship, ExplodeManager.ExlosionType.Ship);
        }

        protected void HitByUFO(GameObject ufo)
        {
            KillWithExplosion(ufo, ExplodeManager.ExlosionType.UFO);
        }
        
        public void KillWithExplosion(GameObject victim, ExplodeManager.ExlosionType explosion_Type )
        {
            ExplodeManager.Instance.Explode(explosion_Type, victim.transform.position);
            RemoveFromGame(victim);
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D bulletCollider)
        {
            HitByBullet(bulletCollider.gameObject);
        }

        protected virtual void HitByBullet(GameObject bullet)
        {
            RemoveFromGame(bullet);
            ExplodeManager.Instance.Explode(ExplodeManager.ExlosionType.Small, bullet.transform.position);
            KillWithExplosion(this.gameObject, ExplodeManager.ExlosionType.Big);
            GetScored(destructionScore);
        }

        public virtual void HitByLaser()
        {
            KillWithExplosion(this.gameObject, ExplodeManager.ExlosionType.Big);
            GetScored(destructionScore);
        }

    }
}