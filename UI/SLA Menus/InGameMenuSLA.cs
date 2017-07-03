﻿using Launcher;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.SLA_Menus
{
    public class InGameMenuSLA : MonoBehaviour {

        public GameObject InGameMenuObject;
        public GameObject OptionsMenuObject;
        public GameObject HighScoreMenuObject;
        public GameObject ChooseLevelObject;

        public InGameMenuManagerSLA InGameMenuManagerSla;
        public RLRMenus.Characters.OptionsMenu OptionsMenu;
        public HighScoreMenuSLA HighScoreMenuSLA;
        public ChooseLevelMenuSLA ChooseLevelMenu;

        public void BackToGame()
        {
            gameObject.SetActive(false);
            InGameMenuManagerSla.MenuOn = false;
            Time.timeScale = 1;
        }

        public void HighScores()
        {
            gameObject.SetActive(false);
            HighScoreMenuObject.gameObject.SetActive(true);
            HighScoreMenuSLA.HighScoreMenuActive = true;
        }

        public void RestartGame()
        {
            GameControl.State.IsDead = true;
            GameControl.State.TotalScore = 0;
            GameControl.State.CurrentLevel = 1;
            GameControl.State.AutoClickerActive = false;
            Time.timeScale = 1;

            SceneManager.LoadScene("SLA");
        }

        public void Options()
        {
            InGameMenuObject.gameObject.SetActive(false);
            OptionsMenuObject.gameObject.SetActive(true);
            OptionsMenu.OptionsMenuActive = true;
        }

        public void BackToMenu()
        {
            GameControl.State.GameActive = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }

        public void ChooseLevel()
        {
            InGameMenuObject.gameObject.SetActive(false);
            ChooseLevelObject.gameObject.SetActive(true);
            ChooseLevelMenu.ChooseLevelMenuActive = true;
        }
    }
}
