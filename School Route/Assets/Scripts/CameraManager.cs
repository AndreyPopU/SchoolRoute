using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [HideInInspector]public Vector3 target;
    private float distance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.parent.position, target);

        if (distance < 1) return;
        else transform.parent.position = Vector3.Lerp(transform.parent.position, target, .0125f); // Move there
    }
}
