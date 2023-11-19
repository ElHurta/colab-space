using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MoveControllerOnline : NetworkBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private List<UnityEngine.Vector3> spawnPositions;

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        HandleMovementServerAuth();
    }

    private void HandleMovementServerAuth() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        HandleMovementServerRPC(x, z);
    }

    [ServerRpc(RequireOwnership = false)]
    private void HandleMovementServerRPC(float x, float z) {

        UnityEngine.Vector3 move = new UnityEngine.Vector3(x, 0, z);
        float moveDistance = speed * Time.deltaTime;
        transform.position += move * moveDistance;
    }

    public override void OnNetworkSpawn() {
        transform.position = spawnPositions[(int)OwnerClientId];
        base.OnNetworkSpawn();
    }
}
