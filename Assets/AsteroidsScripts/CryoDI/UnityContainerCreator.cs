using Asteroids;
using CryoDI;
using DefaultNamespace;
using UnityEngine;

namespace AsteroidsScripts.CryoDI
{
    public class UnityContainerCreator : UnityStarter
    {
        protected override void SetupContainer(CryoContainer container)
        {
            container.RegisterSceneObject<Camera>("Main Camera", LifeTime.Scene);

            container.RegisterType<IController, ShipInput, ShipMovement>();
            container.RegisterType<IController, ShipInput, ShipShooter>();
        }
    }
}