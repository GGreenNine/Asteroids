using System.Collections.Generic;
using Asteroids;
using CryoDI;
using DefaultNamespace;
using HappyUnity.Spawners.ObjectPools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    public class ShipShooter : CryoBehaviour
    {
        public Weapon[] WeaponInventory;

        public Weapon _currentWeapon;
        private int _currentWeaponId;

        [HardDependency(typeof(ShipShooter))] private IController ShipController { get; set; }

        protected override void Awake()
        {
            base.Awake();
            InitializeWeaponInventory();
        }

        public void InitializeWeaponInventory()
        {
            foreach (var weapon in WeaponInventory)
            {
                weapon.DefineOwner(gameObject.GetComponent<AsteroidsGameBehaviour>());
                weapon.Initialization();
            }

            _currentWeapon = WeaponInventory[0];
            _currentWeaponId = 0;
        }

        private void SwapWeapon()
        {
            _currentWeaponId = _currentWeaponId == 0 ? 1 : 0;
            _currentWeapon = WeaponInventory[_currentWeaponId];
        }


        void Update()
        {
            if (ShipController.IsShooting) _currentWeapon.WeaponInputStart();
            if (ShipController.ChangeWeapon) SwapWeapon();
        }

    }
}