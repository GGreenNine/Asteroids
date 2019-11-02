using CryoDI;
using DefaultNamespace;
using UnityEngine;

namespace Asteroids
{
    public class ShipMovement : CryoBehaviour
    {
        public float maxSpeed = 300f;
        public float thrust = 1000f;
        public float torque = 500f;
        
        private float thrustInput;
        private Rigidbody2D rb;
        
        [TypeDependency(typeof(ShipMovement))] private IController ShipController { get; set; }
        
        private void Move()
        {
            var thrustForce = thrustInput * Time.deltaTime * 100 * transform.up;
            rb.AddForce(thrustForce);
        }
        
        private void Reset()
        {
            thrustInput = 0f;
        }

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Reset();
        }

        private void Update()
        {
            thrustInput = ShipController.FrwdAxis;
        }
        
        private void ClampSpeed()
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        private void FixedUpdate()
        {
            Move(); 
            Turn(); 
            ClampSpeed();
        }
        
        private void Turn()
        {
            if (Camera.main != null)
            {
                var diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                diff.Normalize();
 
                var rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
            }
        }
    }
    
    
}