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

        private SmoothMover _smoothMover;

        private void Awake()
        {
            _smoothMover = GetComponent<SmoothMover>();
        }

        private void FixedUpdate()
        {
            if (target)
            {
                _smoothMover.Move();
            }
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
            _smoothMover.BeginMoving();
            _smoothMover.useFixedDeltaTime = true;
            _smoothMover.smoothTime = smoothTime;
            _smoothMover.maxSpeed = maxSpeed;
            _smoothMover.TargetPosition = target;
            _smoothMover.stopWhenReach = false;
        }
    }
}