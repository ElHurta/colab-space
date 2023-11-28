using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLobbiesBtn : MonoBehaviour
{
    public void OnPointerEnter(){
        ReticleManager.Instance.Timer_Enter(3);
    }

    public void OnPointerExit(){
        ReticleManager.Instance.Timer_Exit();
    }
}
