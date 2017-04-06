﻿using Assets.Scripts.Drones;
using UnityEngine;

namespace Assets.Scripts.SLA.Levels
{
    public class Level11SLA : ALevelSLA
    {
        public Level11SLA(LevelManagerSLA manager) : base(manager)
        {
        }

        public override float GetMovementSpeed()
        {
            return 11;
        }

        MineVariations MineVariations = new MineVariations();

        public override void CreateDrones()
        {
            // Spawn Bouncing Drones
            DroneFactory.SpawnAndAddDrones(new RandomBouncingDrone(7f, 1.5f, Color.red), 10, 7f, BoundariesSLA.BouncingSla);

            // Spawn Mine Drones
            var mine = DroneFactory.SpawnDrones(new MineDrone(5f, 3f, Color.black), area: BoundariesSLA.BouncingSla);
            var mine2 = DroneFactory.SpawnDrones(new MineDrone(5f, 3f, Color.black), area: BoundariesSLA.BouncingSla);
            var mine3 = DroneFactory.SpawnDrones(new MineDrone(5f, 3f, Color.black), area: BoundariesSLA.BouncingSla);
            GameObject[] mines = { mine, mine2, mine3 };

            MineVariations.StraightFlying360Drones(32, 10f, 8f, 1f, Color.cyan, mines, DroneFactory, 0.1f);
        }
    }
}