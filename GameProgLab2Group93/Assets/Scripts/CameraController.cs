using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float distanceToPlayer = 3.0f;

    void Update()
    {
        transform.position = player.position - transform.forward * distanceToPlayer;
    }
}
