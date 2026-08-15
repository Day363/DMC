using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class draphenvutsceneobject : MonoBehaviour
{
    public CameraManager CameraManager;
    public CinemachineVirtualCamera cam;

    public void Camvib()
    {
        CameraManager.AnyCamVib(cam);
        CameraManager.CamVibration0_5();
    }
}
