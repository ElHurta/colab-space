using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGameButton : MonoBehaviour
{
    public void OnPointerEnter(){
        ReticleManager.Instance.Timer_Enter(1);
    }

    public void OnPointerExit(){
        ReticleManager.Instance.Timer_Exit();
    }
}