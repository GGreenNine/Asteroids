using HappyUnity.TransformUtils;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ship : AsteroidsGameBehaviour
    {
        public delegate void ShipDeathDelagate();

        public static event ShipDeathDelagate OnShipDeath;
        
        public static Ship Spawn(GameObject prefab)
        {
            GameObject clone = Instantiate(prefab);
            var existingShip = clone.GetComponent<Ship>();
            return existingShip ? existingShip : clone.AddComponent<Ship>();
        }  
        public virtual bool IsAlive => gameObject.activeSelf;

        public virtual void Recover()
        {
            if (!IsAlive)
            {
                ResetPosition();
                gameObject.SetActive(true);
                ResetRigidbody();
            }
        }
        public void ResetPosition()
        {
            transform.position = GameField.GetRandomWorldPositionXY(transform);
            transform.rotation = Quaternion.identity;
        }

        public void ResetRigidbody()
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.rotation = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        }
        
        public void EnableControls()
        {
            GetComponent<ShipMovement>().enabled = true;
            GetComponent<ShipShooter>().enabled = true;
        }

        public void DisableControls()
        {
            GetComponent<ShipMovement>().enabled = false;
            GetComponent<ShipShooter>().enabled = false;
        }

        protected override void RequestDestruction()
        {
            DisableControls();
            OnOnShipDeath();
            gameObject.SetActive(false);
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D bulletCollider)
        {
            ExplodeManager.Instance.Explode(ExplodeManager.ExlosionType.Small, transform.position);
            RequestDestruction();
        }


        protected virtual void OnOnShipDeath()
        {
            OnShipDeath?.Invoke();
        }
    }
}