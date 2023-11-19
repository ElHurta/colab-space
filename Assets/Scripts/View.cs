using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public float sentitivity = 150f;
    public Transform player1;
    public float xRotation = 0f;
    public float yRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {    
            float mouseX = Input.GetAxis("Mouse X") * sentitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sentitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 75f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player1.Rotate(Vector3.up * mouseX);
        }
    }
}
