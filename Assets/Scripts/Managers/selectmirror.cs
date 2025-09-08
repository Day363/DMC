using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class selectmirror : MonoBehaviour
{
    public GameObject player;

    public Vector3 wheretospwan;

    public GameObject cammanager;
    public GameObject attackcore;
    public bool widercam;
    public Collider2D widecollider;

    public GameObject fbutton;
    public bool selectactive;

    public GameObject background0;
    public GameObject wall;
    public GameObject[] backgrounds;
    public GameObject client;

    public GameObject thisPack;

    public Light2D worldlight;
    public float worldlightint;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fbutton.SetActive(true);
            selectactive = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fbutton.SetActive(false);
            selectactive = false;
        }

    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("fbutton") && selectactive)
        {
            player.transform.position = wheretospwan;

            worldlight.intensity = worldlightint;

            if (widercam)
            {
                cammanager.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = widecollider;
                cammanager.GetComponent<CinemachineConfiner2D>().InvalidateCache();

            }

           
            wall.SetActive(false);
            background0.SetActive(false);
            foreach (GameObject background in backgrounds)
            {
                background.SetActive(true);

            }
            client.SetActive(true);
            foreach (Transform mirror in thisPack.transform)
            {
                mirror.gameObject.SetActive(false);
            }

        }

    }



}
