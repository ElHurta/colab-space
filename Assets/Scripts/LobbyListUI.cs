using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TestingLobbyUI testingLobbyUI;

    private void Awake(){
        closeButton.onClick.AddListener(() =>{
            Hide();
            testingLobbyUI.Show();
        });
    }

    private void Start() {
        Hide();
    }
    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}
