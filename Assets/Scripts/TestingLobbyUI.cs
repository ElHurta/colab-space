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
    [SerializeField] private Transform lobbyBtn;
    [SerializeField] private Transform lobbiesContainer;

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
            Debug.Log("Show Lobbies");
            lobbyListUI.Show();
            gameObject.SetActive(false);
        });

        lobbyBtn.gameObject.SetActive(false);
    }

    private void Start(){
        SpaceGameLobby.Instance.OnLobbyListChanged += HandleLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
    }

    private void HandleLobbyListChanged(object sender, SpaceGameLobby.OnLobbyListChangedEventArgs e){
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList){
        foreach (Transform child in lobbiesContainer){
            if (child != lobbyBtn){
                Destroy(child.gameObject);
            }
        }

        foreach (Lobby lobby in lobbyList){
            Transform lobbyBtnInstance = Instantiate(lobbyBtn, lobbiesContainer);
            lobbyBtnInstance.gameObject.SetActive(true);
            lobbyBtnInstance.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }

    private void OnDestroy(){
        SpaceGameLobby.Instance.OnLobbyListChanged -= HandleLobbyListChanged;
    }
    
    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }   
}
