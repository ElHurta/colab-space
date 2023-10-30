using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MoveController : NetworkBehaviour
{
    public CharacterController controller;
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = (transform.right * x) + (transform.forward * z);
        controller.Move(movement * speed * Time.deltaTime);
    }
}
