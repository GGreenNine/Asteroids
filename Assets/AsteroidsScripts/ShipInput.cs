using System.Runtime.CompilerServices;
using UnityEngine;

namespace Asteroids
{
    using DefaultNamespace;
    public class ShipInput : IController
    {
        bool IController.IsShooting => Input.GetButtonDown("Fire1");

        bool IController.IsHyperspacing => Input.GetButtonDown("Fire2");

        public float TurnAxis => Input.GetAxis("Horizontal");

        public float FrwdAxis
        {
            get
            {
                float axis = Input.GetAxis("Vertical");
                return Mathf.Clamp01(axis);
            }
        }

        bool IController.ChangeWeapon => Input.GetKeyDown(KeyCode.R);
    }

}