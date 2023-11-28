using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLobbiesBtn : MonoBehaviour
{
    public void OnPointerEnter(){
        ReticleManager.Instance.Timer_Enter(2);
    }

    public void OnPointerExit(){
        ReticleManager.Instance.Timer_Exit();
    }
}
