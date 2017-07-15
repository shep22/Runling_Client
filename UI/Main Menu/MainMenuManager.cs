﻿using Launcher;
using UI.RLR_Menus;
using UI.SLA_Menus;
using UnityEngine;

namespace UI.Main_Menu
{
    using OptionsMenu;

    public class MainMenuManager : MonoBehaviour
    {
        public MainMenu MainMenu;
        public OptionsMenu OptionsMenu;
        public SLAMenu SLAMenu;
        public HighScoreMenuSLA HighScoreMenuSLA;
        public RLRMenu RLRMenu;
        public HighScoreMenuRLR HighScoreMenuRLR;
        public SoloMenu SoloMenu;
        public MultiplayerMenu MultiplayerMenu;
        public SceneLoader SceneLoader;

        #region CameraVariables
        public Camera Camera;
        public Vector3 CameraPosMainMenu;
        public Vector3 CameraPosRLR;
        public Vector3 CameraPosSLA;
        private Vector3 _targetPos;
        public Quaternion CameraRotMainMenu;
        public Quaternion CameraRotSLA;
        public Quaternion CameraRotRLR;
        private Quaternion _targetRot;
        private float _distance;
        private float _angle;
        private float _initializationTime;
        private float _currentPos;
        private float _currentRot;
        private float _cameraSpeed;
        private Vector3 _oldPos;
        private Quaternion _oldRot;

        private void Awake()
        {
            CameraPosMainMenu = Camera.transform.position;
            CameraPosRLR = new Vector3(0, 35, 70);
            CameraPosSLA = new Vector3(0, 35, 60);
            CameraRotMainMenu = Camera.transform.rotation;
            CameraRotSLA = Quaternion.Euler(40, 0, 0);
            CameraRotRLR = Quaternion.Euler(40, 180, 0);
            _targetPos = Camera.transform.position;
            _targetRot = Camera.transform.rotation;
            _cameraSpeed = 100;
        }
        #endregion


        public void MoveCamera(Vector3 newPos, Quaternion newRot)
        {
            _targetPos = newPos;
            _targetRot = newRot;
            _oldPos = Camera.transform.position;
            _oldRot = Camera.transform.rotation;
            _distance = (newPos - _oldPos).magnitude;
            _angle = Quaternion.Angle(_oldRot, newRot);
            _initializationTime = Time.time;
        }

        private void Update()
        {
            // Move Camera
            if (Camera.transform.position != _targetPos || Camera.transform.rotation != _targetRot)
            {
                var rotationSpeed = _angle / (_distance / _cameraSpeed);
                _currentPos = (Time.time - _initializationTime) * _cameraSpeed / _distance;
                _currentRot = (Time.time - _initializationTime) * rotationSpeed / _angle;
                Camera.transform.position = Vector3.Lerp(_oldPos, _targetPos, _currentPos);
                Camera.transform.rotation = Quaternion.Slerp(_oldRot, _targetRot, _currentRot);
            }

            #region NavigateMenu

            if (GameControl.InputManager.GetButtonDown(HotkeyAction.NavigateMenu))
            {
                if (OptionsMenu.gameObject.activeSelf)
                {
                    OptionsMenu.DiscardChanges();
                }
                else if (SoloMenu.gameObject.activeSelf)
                {
                    SoloMenu.BackToMenu();
                }
                else if (MultiplayerMenu.gameObject.activeSelf)
                {
                    MultiplayerMenu.BackToMenu();
                }
                else if (SLAMenu.gameObject.activeSelf)
                {
                    SLAMenu.BackToMenu();
                }
                else if (RLRMenu.gameObject.activeSelf)
                {
                    RLRMenu.BackToMenu();
                }
                else if (HighScoreMenuSLA.gameObject.activeSelf)
                {
                    HighScoreMenuSLA.Back();
                }
                else if (HighScoreMenuRLR.gameObject.activeSelf)
                {
                    HighScoreMenuRLR.Back();
                }
            }
            #endregion
        }
    }
}