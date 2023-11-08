using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Button clientBtn;
    [SerializeField] private Button hostBtn;

    private void Awake(){
        clientBtn.onClick.AddListener(() =>{
            Debug.Log("Client");
            NetworkManager.Singleton.StartClient();
        });
        hostBtn.onClick.AddListener(() =>{
            Debug.Log("Host");
            NetworkManager.Singleton.StartHost();
        });
    }
}
