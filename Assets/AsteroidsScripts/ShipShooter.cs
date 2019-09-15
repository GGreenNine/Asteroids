using System.Collections.Generic;
using Asteroids;
using DefaultNamespace;
using HappyUnity.Spawners.ObjectPools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    public class ShipShooter : MonoBehaviour
    {
        public Weapon[] WeaponInventory;

        public Weapon _currentWeapon;
        private int _currentWeaponId;
        
        void Awake()
        {
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
            if (ShipInput.IsShooting()) _currentWeapon.WeaponInputStart();
            if(ShipInput.ChangeWeapon()) SwapWeapon();
        }

        
    }
}