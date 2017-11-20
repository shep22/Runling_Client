﻿using Launcher;
using RLR;
using TMPro;
using UnityEngine;

namespace UI.RLR_Menus
{
    public class HighScoreMenuRLR : MonoBehaviour {

        public GameObject Menu;
        public GameObject ScorePrefab;
        public GameObject Background;
        public ControlRLR ControlRLR;
        public ScoreRLR Score;

        private GameObject _descriptionText;
        private readonly GameObject[] _levelScore = new GameObject[LevelManagerRLR.NumLevels];
        private GameObject _timeModeScore;

        private void Awake()
        {
            CreateTextObjects(Background);
        }

        private void OnEnable()
        {
            SetNumbers();
        }

        public void CreateTextObjects(GameObject background)
        {
            // Descriptions
            _descriptionText = Instantiate(ScorePrefab, background.transform);
            _descriptionText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level";
            if (Score != null && GameControl.GameState.SetGameMode != GameMode.Practice)
            {
                _descriptionText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Cur:";
                _descriptionText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "PB:";
            }
            else
            {
                _descriptionText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Normal:";
                _descriptionText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Hard";
            }

            _descriptionText.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 20);
            for (var i = 0; i < 3; i++)
            {
                _descriptionText.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 20);
            }

            // Level Highscores
            for (var i = 0; i < LevelManagerRLR.NumLevels; i++)
            {
                _levelScore[i] = Instantiate(ScorePrefab, background.transform);
                _levelScore[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            }

            // Time Mode Score
            _timeModeScore = Instantiate(ScorePrefab, background.transform);
            _timeModeScore.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Time Mode";
            _timeModeScore.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 20);
            _timeModeScore.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 20);


            if (Score == null)
            {
                _descriptionText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Normal";
                _descriptionText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Hard";
            }
        }

        public void SetNumbers()
        {
            if (Score != null && GameControl.GameState.SetGameMode != GameMode.Practice)
            {
                SetHighScoresIngame();
                SetCurrentScores();
            }
            else
            {
                SetMainMenuScores();
            }
        }

        private void SetHighScoresIngame()
        {
            switch (GameControl.GameState.SetDifficulty)
            {
                case Difficulty.Normal:
                    for (var i = 0; i < LevelManagerRLR.NumLevels; i++)
                    {
                        if (GameControl.HighScores.HighScoreRLRNormal[i + 1] > 0.1f)
                        {
                            _levelScore[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameControl.HighScores.HighScoreRLRNormal[i + 1] > 60
                                    ? (int)GameControl.HighScores.HighScoreRLRNormal[i + 1] / 60 + ":" + (GameControl.HighScores.HighScoreRLRNormal[i + 1] % 60).ToString("00.00")
                                    : GameControl.HighScores.HighScoreRLRNormal[i + 1].ToString("f2");
                        }
                        else
                        {
                            _levelScore[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "-";
                        }
                    }
                    _timeModeScore.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameControl.HighScores.HighScoreRLRNormal[0].ToString("f0");
                    break;

                case Difficulty.Hard:
                    for (var i = 0; i < LevelManagerRLR.NumLevels; i++)
                    {
                        if (GameControl.HighScores.HighScoreRLRHard[i + 1] > 0.1f)
                        {
                            _levelScore[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameControl.HighScores.HighScoreRLRHard[i + 1] > 60
                                    ? (int)GameControl.HighScores.HighScoreRLRHard[i + 1] / 60 + ":" + (GameControl.HighScores.HighScoreRLRHard[i + 1] % 60).ToString("00.00") 
                                    : GameControl.HighScores.HighScoreRLRHard[i + 1].ToString("f2");
                        }
                        else
                        {
                            _levelScore[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "-";
                        }
                    }
                    _timeModeScore.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameControl.HighScores.HighScoreRLRHard[0].ToString("f0");
                    break;
            }
        }

        private void SetCurrentScores()
        {
            switch (GameControl.GameState.SetDifficulty)
            {
                case Difficulty.Normal:
                    for (var i = 0; i < LevelManagerRLR.NumLevels; i++)
                    {
                        if (Score.FinishTimeCurGame[i] > 0.1f)
                        {
                            _levelScore[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Score.FinishTimeCurGame[i] > 60
                                ? (int)Score.FinishTimeCurGame[i] / 60 + ":" + (Score.FinishTimeCurGame[i] % 60).ToString("00.00")
                                : Score.FinishTimeCurGame[i].ToString("f2");
                        }
                        else
                        {
                            _levelScore[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "-";
                        }

                        // New records are shown green
                        if ((Score.FinishTimeCurGame[i] <= GameControl.HighScores.HighScoreRLRNormal[i + 1] || GameControl.HighScores.HighScoreRLRNormal[i + 1] < 0.1f) && Score.FinishTimeCurGame[i] > 0.1f)
                        {
                            _levelScore[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.green;
                        }
                    }
                    if (GameControl.GameState.SetGameMode == GameMode.TimeMode)
                    {
                        _timeModeScore.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ControlRLR.PlayerManager.TotalScore.ToString();
                        if (ControlRLR.PlayerManager.TotalScore >= GameControl.HighScores.HighScoreRLRNormal[0] && ControlRLR.PlayerManager.TotalScore != 0)
                        {
                            _timeModeScore.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.green;
                        }
                    }
                    else
                    {
                        _timeModeScore.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                    }
                    break;

                case Difficulty.Hard:
                    for (var i = 0; i < LevelManagerRLR.NumLevels; i++)
                    {
                        if (Score.FinishTimeCurGame[i] > 0.1f)
                        {
                            _levelScore[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Score.FinishTimeCurGame[i] > 60
                                ? (int)Score.FinishTimeCurGame[i] / 60 + ":" + (Score.FinishTimeCurGame[i] % 60).ToString("00.00")
                                : Score.FinishTimeCurGame[i].ToString("f2");
                        }
                        else
                        {
                            _levelScore[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "-";
                        }

                        // New records are shown green
                        if ((Score.FinishTimeCurGame[i] <= GameControl.HighScores.HighScoreRLRHard[i + 1] || GameControl.HighScores.HighScoreRLRHard[i + 1] < 0.1f) && Score.FinishTimeCurGame[i] > 0.1f)
                        {
                            _levelScore[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.green;
                        }
                    }
                    if (GameControl.GameState.SetGameMode == GameMode.TimeMode)
                    {
                        _timeModeScore.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ControlRLR.PlayerManager.TotalScore.ToString();
                        if (ControlRLR.PlayerManager.TotalScore >= GameControl.HighScores.HighScoreRLRHard[0] && ControlRLR.PlayerManager.TotalScore != 0)
                        {
                            _timeModeScore.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.green;
                        }
                    }
                    else
                    {
                        _timeModeScore.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                    }
                    break;
            }

        }

        private void SetMainMenuScores()
        {
            // Normal
            for (var i = 0; i < LevelManagerRLR.NumLevels; i++)
            {
                if (GameControl.HighScores.HighScoreRLRNormal[i + 1] > 0.1f)
                {
                    _levelScore[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameControl.HighScores.HighScoreRLRNormal[i + 1] > 60
                        ? (int)GameControl.HighScores.HighScoreRLRNormal[i + 1] / 60 + ":" + (GameControl.HighScores.HighScoreRLRNormal[i + 1] % 60).ToString("00.00")
                        : GameControl.HighScores.HighScoreRLRNormal[i + 1].ToString("f2");
                }
                else
                {
                    _levelScore[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "-";
                }
            }
            _timeModeScore.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameControl.HighScores.HighScoreRLRNormal[0].ToString("f0");

            // Hard
            for (var i = 0; i < LevelManagerRLR.NumLevels; i++)
            {
                if (GameControl.HighScores.HighScoreRLRHard[i + 1] > 0.1f)
                {
                    _levelScore[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameControl.HighScores.HighScoreRLRHard[i + 1] > 60
                        ? (int)GameControl.HighScores.HighScoreRLRHard[i + 1] / 60 + ":" + (GameControl.HighScores.HighScoreRLRHard[i + 1] % 60).ToString("00.00")
                        : GameControl.HighScores.HighScoreRLRHard[i + 1].ToString("f2");
                }
                else
                {
                    _levelScore[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "-";
                }
            }
            _timeModeScore.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameControl.HighScores.HighScoreRLRHard[0].ToString("f0");
        }
        
        public void Back()
        {
            gameObject.SetActive(false);
            Menu.gameObject.SetActive(true);
        }
    }
}
