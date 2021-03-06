﻿using Characters;
using Network.Synchronization;
using Network.Synchronization.Data;
using Players;
using Server.Scripts.Synchronization;
using SLA;
using UnityEngine;

namespace Server.Scripts.SLA
{
    public class InitializeGameSLAServer : MonoBehaviour
    {
        public PlayerFactory PlayerFactory;
        private ControlSLAServer _controlSLA;
        private LevelManagerSLAServer _levelManager;
        private ScoreSLAServer _score;

        private void Awake()
        {
            _controlSLA = GetComponent<ControlSLAServer>();
            _levelManager = GetComponent<LevelManagerSLAServer>();
            _score = GetComponent<ScoreSLAServer>();
        }

        public PlayerManager InitializePlayer(Player player)
        {
            var playerManager = PlayerFactory.Create("Manticore");
            playerManager.Model.SetActive(false);
            playerManager.Player = player;
            playerManager.PlayerMovement = playerManager.gameObject.AddComponent<PlayerMovement>();
            var data = playerManager.gameObject.AddComponent<PlayerStateManager>();
            data.Id = playerManager.Player.Id;
            playerManager.PlayerStateManager = data;
            _score.Scores[playerManager] = new ScoreDataSLA(playerManager, LevelManagerSLAServer.NumLevels);
            return playerManager;
        }

        public PlayerState SpawnPlayer(PlayerManager playerManager)
        {
            playerManager.IsDead = false;
            playerManager.IsImmobile = false;
            playerManager.IsInvulnerable = true;
            playerManager.Model.SetActive(true);
            playerManager.Shield.SetActive(true);

            playerManager.CharacterController.Speed.SetBaseSpeed(_levelManager.GetMovementSpeed(_controlSLA.CurrentLevel));

            playerManager.transform.position = _controlSLA.PlayerManagers.Values.Count != 1
                ? Vector3.zero + Quaternion.Euler(0, 360f * playerManager.Player.Id / _controlSLA.PlayerManagers.Count, 0) * Vector3.right * 2
                : Vector3.zero;

            playerManager.transform.rotation = _controlSLA.PlayerManagers.Values.Count != 1 
                ? Quaternion.LookRotation(Vector3.zero - playerManager.transform.position, Vector3.up)
                : Quaternion.identity;

            return new PlayerState(playerManager.Player.Id, playerManager.transform.position.x, 
                playerManager.transform.position.z, playerManager.transform.eulerAngles.y);
        }

        public void StartLevel()
        {
            SyncGameServer.StartLevel();

            foreach (var playerManager in _controlSLA.PlayerManagers.Values)
            {
                ServerManager.Instance.Server.Dispatcher.InvokeAsync(() =>
                {
                    playerManager.Shield.SetActive(false);
                });
                playerManager.IsInvulnerable = false;
            }

            _score.StartScore();
        }
    }
}
