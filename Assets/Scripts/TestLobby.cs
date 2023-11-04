using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Core;

public class TestLobby : MonoBehaviour
{
    private Lobby hostLobby;
    private float heartBeatTimer;

    // Start is called before the first frame update
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>{
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update(){
        HandleLobbyHeartbeat();
    }

    private async void HandleLobbyHeartbeat(){
        if(hostLobby != null){
            heartBeatTimer -= Time.deltaTime;
            if(heartBeatTimer < 0){
                float heartBeatTimerMax = 15;
                heartBeatTimer = heartBeatTimerMax;

               await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }
    
    private async void CreateLobby(){
        try {
            string lobbyName = "Test Lobby";
            int maxPlayers = 4;

            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions {
                IsPrivate = true,
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createLobbyOptions);

            hostLobby = lobby;
            Debug.Log("Lobby created: " + lobby.Id);
        } catch(LobbyServiceException e) {
            Debug.Log("Error creating lobby: " + e.Message);
        }
    }

    private async void ListLobbies(){
        try {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found: " + queryResponse.Results.Count);
            foreach(Lobby lobby in queryResponse.Results){
                Debug.Log("Lobby: " + lobby.Id);
            }
        } catch (LobbyServiceException e) {
            Debug.Log("Error listing lobbies: " + e.Message);
        }
    }

    private async void JoinLobbyByCode(string lobbyCode){
        try {
            Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode);

            Debug.Log("Lobby joined: " + lobby.Id);
        } catch (LobbyServiceException e) {
            Debug.Log("Error joining lobby: " + e.Message);
        }
    }

    private async void QuickJoinLobby(){
        try {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        } catch (LobbyServiceException e) {

            Debug.Log("Error quick joining lobby: " + e.Message);
        }
    }

    private void PrintPlayers(Lobby lobby){
        Debug.Log("Players in lobby: " + lobby.Id);
        try {
            foreach (Player player in lobby.Players){
                Debug.Log("Player: " + player.Id);
            }
        } catch (LobbyServiceException e) {
            Debug.Log("Error listing players: " + e.Message);
        }
    }
}
