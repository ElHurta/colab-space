using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Netcode;
using UnityEngine;
using System;

public class SpaceGameLobby : MonoBehaviour
{
    public static SpaceGameLobby Instance { get; private set;}
    private Lobby joinedLobby;
    private float heartbeatTimer;
    private float listLobbiesTimer;
    public event EventHandler<OnLobbyListChangedEventArgs> OnLobbyListChanged;
    public class OnLobbyListChangedEventArgs : EventArgs {
        public List<Lobby> lobbyList;
    };

    private void Awake(){
        Instance = this;

        DontDestroyOnLoad(gameObject);

        InitializeUnityAuthentication();
    }

    private async void InitializeUnityAuthentication(){
        if (UnityServices.State != ServicesInitializationState.Initialized){

            InitializationOptions initializationOptions = new InitializationOptions();

            initializationOptions.SetProfile(UnityEngine.Random.Range(0, 1000).ToString());

            await UnityServices.InitializeAsync(initializationOptions);

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    private void Update(){
        HandleHeartbeat();
        HandlePeriodicListLobbies();
    }

    private void HandlePeriodicListLobbies(){
        if(joinedLobby == null && AuthenticationService.Instance.IsSignedIn) return;
        listLobbiesTimer -= Time.deltaTime;

        if (listLobbiesTimer <= 0){
            listLobbiesTimer = 3f;
            ListLobbies();
        }
    }

    private void HandleHeartbeat(){
        if (IsLobbyHost()){
            heartbeatTimer -= Time.deltaTime;

            if (heartbeatTimer <= 0){
                heartbeatTimer = 15f;
                LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);
            }
        }
    }

    private bool IsLobbyHost(){
        return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
    }

    private async void ListLobbies(){
        try {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions {
                Filters = new List<QueryFilter> {
                    new QueryFilter(
                            QueryFilter.FieldOptions.AvailableSlots,
                            "0",
                            QueryFilter.OpOptions.GT
                        )
                }
            };
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync(queryLobbiesOptions);
            OnLobbyListChanged?.Invoke(this, new OnLobbyListChangedEventArgs{
                lobbyList = queryResponse.Results
            });
        } catch (LobbyServiceException e){
            Debug.LogError($"Failed to query lobbies: {e.Message}");
        }
    }

    public async void CreateLobby(string lobbyName, bool isPrivate){
        try {
            joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 4, new CreateLobbyOptions{
                IsPrivate = isPrivate,
            });

            NetworkManager.Singleton.StartHost();
            Debug.Log("Create Game " + joinedLobby.LobbyCode.ToString());
        } catch (LobbyServiceException e){
            Debug.LogError($"Failed to create lobby: {e.Message}");
        }
    }

    public async void QuickJoin(){
        try  {
            joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
            NetworkManager.Singleton.StartClient();
            Debug.Log("Join Game" + joinedLobby.LobbyCode.ToString());
        } catch (LobbyServiceException e){
            Debug.LogError($"Failed to join lobby: {e.Message}");
        }
    }

    public Lobby GetLobby(){
        return joinedLobby;
    }

    public async void DeleteLobby(){

        if (joinedLobby != null){
            try {
                await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);

                joinedLobby = null;
            } catch (LobbyServiceException e){
                Debug.LogError($"Failed to delete lobby: {e.Message}");
            }
        }
    }

    public async void LeaveLobby(){
        try {
            await LobbyService.Instance.RemovePlayerAsync(
                joinedLobby.Id,
                AuthenticationService.Instance.PlayerId
            );
            joinedLobby = null;
        } catch (LobbyServiceException e){
            Debug.LogError($"Failed to leave lobby: {e.Message}");
        }
    }

    public async void KickPlayerFromLobby(string playerId){
        try {
            if (IsLobbyHost()){
                await LobbyService.Instance.RemovePlayerAsync(
                    joinedLobby.Id,
                    playerId
                );
            }
        } catch (LobbyServiceException e){
            Debug.LogError($"Failed to  lobby: {e.Message}");
        }
    }
}
