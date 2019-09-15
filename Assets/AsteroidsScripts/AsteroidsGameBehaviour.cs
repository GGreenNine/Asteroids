using HappyUnity.Spawners.ObjectPools;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidsGameBehaviour : MonoBehaviour, IPoolableAware, IRecyclable
    {
        private Poolable _poolable;
        
        public void PoolableAwoke(Poolable p)
        {
            _poolable = p;
        }

        public void Recycle()
        {
            RemoveFromGame();
        }
        
        public void RemoveFromGame()
        {
            if (_poolable)
                _poolable.Recycle();
            else
                RequestDestruction();
        }

        protected void InvokeRemoveFromGame(float time) { Invoke(nameof(RemoveFromGame), time); }


        protected static void RemoveFromGame(GameObject victim)
        {
            var handler = victim.GetComponent<AsteroidsGameBehaviour>();
            if (handler)
                handler.RemoveFromGame();
            else
                DefaultDestruction(victim);
        }

        protected virtual void GetScored(int value)
        {
            Score.Earn(value);
        }
        
        protected virtual void RequestDestruction() { DefaultDestruction(gameObject); }


        private static void DefaultDestruction(GameObject gameObject)
        {
            Destroy(gameObject);
        }
    }
}