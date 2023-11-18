using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class TestingLobbyUI : MonoBehaviour
{
    [SerializeField] private Button createGameButton;
    [SerializeField] private Button quickJoinGameButton;
    [SerializeField] private Button showLobbiesButton;
    [SerializeField] private LobbyListUI lobbyListUI;

    private void Awake(){
        createGameButton.onClick.AddListener(() =>{
            SpaceGameLobby.Instance.CreateLobby(Random.Range(0, 100).ToString(), false);
            Lobby lobby = SpaceGameLobby.Instance.GetLobby();
        });
        quickJoinGameButton.onClick.AddListener(() =>{
            SpaceGameLobby.Instance.QuickJoin();
            Lobby lobby = SpaceGameLobby.Instance.GetLobby();
        });
        showLobbiesButton.onClick.AddListener(() =>{
            lobbyListUI.Show();
        });
    }

}
