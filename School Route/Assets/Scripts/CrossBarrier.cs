using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Kid>()) GameManager.instance.kidIndex++;
    }
}
