﻿using Assets.Scripts.SLA.Levels;
using UnityEngine;

namespace Assets.Scripts.Drones
{
    public class RandomBouncingDrone : ADrone
    {
        public RandomBouncingDrone(float speed, float size, Color color) : base(speed, size, color)
        {
        }

        public override GameObject CreateDroneInstance(DroneFactory factory, bool isAdded, Area area)
        {
            var pos = isAdded
                ? DroneStartPosition.RandomCornerGround(Size, area)
                : DroneStartPosition.RandomPositionGround(Size, area);

            var newDrone = Object.Instantiate(factory.BouncingDrone, pos, Quaternion.Euler(0, DroneDirection.RandomDirection(1f), 0));
            return newDrone;
        }
    }
}
