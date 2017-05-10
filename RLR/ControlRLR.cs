﻿using Assets.Scripts.Launcher;
using UnityEngine;

namespace Assets.Scripts.RLR
{
    public class ControlRLR : MonoBehaviour
    {
        public LevelManagerRLR LevelManager;
        public InitializeGameRLR InitializeGameRLR;
        public DeathRLR DeathRLR;
        public GameObject PracticeMode;

        public bool StopUpdate;

        void Start()
        {
            // Set current Level and movespeed, load drones and spawn immunity
            StopUpdate = true;
            GameControl.GameActive = true;
            GameControl.MoveSpeed = 13;
            if (GameControl.SetGameMode == GameControl.Gamemode.Practice)
            {
                PracticeMode.SetActive(true);
            }

            InitializeGameRLR.InitializeGame();
        }

        //update when dead
        private void Update()
        {
            if (GameControl.IsDead && !StopUpdate)
            {
                StopUpdate = true;
                DeathRLR.Death(LevelManager, InitializeGameRLR, this);
            }

            if (GameControl.FinishedLevel && !StopUpdate)
            {
                LevelManager.EndLevel(0f);
            }


            // Press Ctrl to start autoclicking
            if (InputManager.Instance.GetButtonDown(HotkeyAction.ActivateClicker))
            {
                if (!GameControl.AutoClickerActive)
                    GameControl.AutoClickerActive = true;
            }

            // Press Alt to stop autoclicking
            if (InputManager.Instance.GetButtonDown(HotkeyAction.DeactivateClicker))
            {
                if (GameControl.AutoClickerActive)
                    GameControl.AutoClickerActive = false;
            }

            // Press 1 to be invulnerable
            if (InputManager.Instance.GetButtonDown(HotkeyAction.ActivateGodmode) && !GameControl.GodModeActive)
            {
                GameControl.GodModeActive = true;
            }

            // Press 2 to be vulnerable
            if (InputManager.Instance.GetButtonDown(HotkeyAction.DeactiveGodmode) && GameControl.GodModeActive)
            {
                GameControl.GodModeActive = false;
            }
        }
    }
}

