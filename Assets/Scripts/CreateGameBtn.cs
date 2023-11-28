using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameBtn : MonoBehaviour
{
    public void OnPointerEnter(){
        ReticleManager.Instance.Timer_Enter(0);
    }

    public void OnPointerExit(){
        ReticleManager.Instance.Timer_Exit();
    }
}
