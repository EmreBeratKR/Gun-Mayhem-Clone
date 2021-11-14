using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Gun gun = transform.Find("Gun Slot").GetChild(0).GetComponent<Gun>();
            gun.fire();
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Gun gun = transform.Find("Gun Slot").GetChild(0).GetComponent<Gun>();
            gun.set_firing(false);
        }
    }
}
