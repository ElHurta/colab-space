using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinLobbyBtn : MonoBehaviour
{
    public void OnPointerEnter(){
        ReticleManager.Instance.Timer_Enter(4);
    }

    public void OnPointerExit(){
        ReticleManager.Instance.Timer_Exit();
    }
}
