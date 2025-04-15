using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position = target.position;
    }
}
