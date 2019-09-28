using System;
using HappyUnity.Spawners.ObjectPools;
using HappyUnity.TransformUtils;
using UnityEngine;

namespace Asteroids
{
    public class UFO : EnemyUnit
    {
        public Transform target;
        public float maxSpeed;
        public float smoothTime;

        private FollowTarget _smoothMover;

        private void Awake()
        {
            _smoothMover = GetComponent<FollowTarget>();
        }

        private void FixedUpdate()
        {
        }

        public override void Spawn()
        {
            transform.position = GameField.GetRandomWorldPositionXY(this.transform);
        }

        protected override void RequestDestruction()
        {
            gameObject.SetActive(false);
        }

        public void ConfigureSmoothMover()
        {
            _smoothMover.Target = target;
            _smoothMover.StartFollowing();
            _smoothMover.FollowPositionSpeed = maxSpeed;
        }
    }
}