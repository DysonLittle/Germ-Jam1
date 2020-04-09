using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    [SerializeField]
    float pullSpeed = 0.0f;

    private void OnTriggerStay(Collider other)
    {
        float step = pullSpeed * Time.deltaTime;
        other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, step);
    }
}
