using HappyUnity.TransformUtils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    public class GameUnit : AsteroidsGameBehaviour
    {
        [SerializeField]
        [Range(0, 3000)]
        protected int destructionScore = 100;
        
        [SerializeField]
        [Range(0, 2000)]
        protected float max_Force = 100;
        
        [SerializeField]
        [Range(0, 3000)]
        protected float max_Torque = 100;
        
        [Range(0, 5)]
        [SerializeField]
        float minScale = 1.0f;

        [Range(0, 5)]
        [SerializeField]
        float maxScale = 1.5f;

        public Vector3 Randomize()
        {
            float r = Random.Range(minScale, maxScale);
            return new Vector3(r, r, r);
        }
        
        public virtual void Spawn()
        {
            transform.localScale = Randomize();

            Give_RandomForce();
            Give_RandomTorque();
            
            transform.position = GameField.GetRandomWorldPositionXY(this.transform);
        }
        

        
        public virtual void SpawnAt(Vector3 position)
        {
            transform.localScale = Randomize();
            
            Give_RandomForce();
            Give_RandomTorque();
            
            transform.position = position;
        }
        
        

        protected void Give_RandomForce()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(max_Force * 10 * Random.insideUnitCircle);
        }

        protected void Give_RandomTorque()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.angularVelocity = 0;
            rb.AddTorque(Random.Range(0, max_Torque)* 10);
        }
    }
}