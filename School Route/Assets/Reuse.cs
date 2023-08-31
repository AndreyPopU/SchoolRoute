using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reuse : MonoBehaviour
{
    public Transform reusePosition;
    private bool disabledSpawners;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Car>())
        {
            other.transform.position = reusePosition.position;
            
            if (!disabledSpawners)
            {
                foreach (CarSpawner spawner in GetComponentInParent<Road>().spawner)
                    Destroy(spawner);

                disabledSpawners = true;
            }
        }
    }
}
