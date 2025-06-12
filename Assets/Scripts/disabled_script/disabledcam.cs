using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledcam : MonoBehaviour
{
    public GameObject cam;

    public GameObject deathattackobject;

    public void LookSelf()
    {
        cam.GetComponent<CameraManager>().LookEnemy();
    }

    public void LookPlayer()
    {
        cam.GetComponent<CameraManager>().LookPlayer();
    }

    public void Deathattack()
    {
        cam.GetComponent<CameraManager>().Looksmallpoint(deathattackobject);
    }
}
