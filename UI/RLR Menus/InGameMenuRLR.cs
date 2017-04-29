﻿using Assets.Scripts.Launcher;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.UI.RLR_Menus;

namespace Assets.Scripts.UI.RLR_Menu
{
    public class InGameMenuRLR : MonoBehaviour {

        public GameObject InGameMenuObject;
        public GameObject OptionsMenuObject;
        public GameObject HighScoreMenuObject;
        public InGameMenuManagerRLR InGameMenuManagerSla;
        public OptionsMenu.OptionsMenu OptionsMenu;

        public void BackToGame()
        {
            gameObject.SetActive(false);
            InGameMenuManagerSla.MenuOn = false;
            Time.timeScale = 1;
        }

        public void RestartGame()
        {
            GameControl.Dead = true;
            GameControl.TotalScore = 0;
            GameControl.AutoClickerActive = false;
            Time.timeScale = 1;

            SceneManager.LoadScene("RLR");
        }

        public void Options()
        {
            InGameMenuObject.gameObject.SetActive(false);
            OptionsMenuObject.gameObject.SetActive(true);
            OptionsMenu.OptionsMenuActive = true;
        }

        public void BackToMenu()
        {
            GameControl.GameActive = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}