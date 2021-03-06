﻿using Drones.DroneTypes;
using Drones.Movement;
using Drones.Pattern;

namespace RLR.Levels.Normal
{
    public class Level6RLR : ALevelRLR
    {
        public Level6RLR(LevelManagerRLR manager) : base(manager)
        {
        }

        public override void SetChasers()
        {
            Manager.RunlingChaser.SetChaserPlatforms(new DefaultDrone(6, 1f, DroneColor.BrightGreen), new[] { 1, 8}, new[] { 4, 12});
        }

        public override void CreateDrones()
        {
            // Spawn blue drones
            DroneFactory.SetPattern(new PatContinuousSpawn(0.15f, 1),
                new RandomDrone(9, 2, DroneColor.Golden, restrictedZone: 0, droneType: DroneType.FlyingOneWayDrone, movementType: new SinusoidalMovement(5,3)));
        }
    }
}
