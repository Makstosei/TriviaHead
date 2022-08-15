using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRunTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.Instance.isLockedControl(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.Instance.isLockedControl(false);
        }
    }



}
