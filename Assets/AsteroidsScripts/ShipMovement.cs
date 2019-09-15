using HappyUnity.Math;
using UnityEngine;

namespace Asteroids
{
    public class ShipMovement : MonoBehaviour
    {
        public float maxSpeed = 300f;
        public float thrust = 1000f;
        public float torque = 500f;
        float thrustInput;
        Rigidbody2D rb;
        
        void Move()
        {
            Vector3 thrustForce = thrustInput * Time.deltaTime * 100 * transform.up;
            rb.AddForce(thrustForce);
        }
        
        void Reset()
        {
            thrustInput = 0f;
        }
        
        void Awake() { rb = GetComponent<Rigidbody2D>(); }
        void OnEnable() { Reset(); }

        void Update()
        {
            thrustInput = ShipInput.GetForwardThrust();
        }
        
        void ClampSpeed()
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        void FixedUpdate() { Move(); Turn(); ClampSpeed(); }
        
        void Turn()
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
 
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }
    
    
}