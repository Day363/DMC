using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class selectmirror : MonoBehaviour
{
    public GameObject attackcore;

    public GameObject fbutton;
    public bool selectactive;

    public GameObject background0;
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

    void Update()
    {
        if (Input.GetButtonDown("fbutton") && selectactive)
        {
            worldlight.intensity = worldlightint;

            attackcore.GetComponent<attackcore>().BattleStart();

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
            attackcore.GetComponent<attackcore>().BattleStart();
        }

    }
}
