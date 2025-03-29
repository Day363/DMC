using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usurper_baldodetect : MonoBehaviour
{
    public GameObject usurper;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            usurper.GetComponent<usurper_script>().Counselcam();
            usurper.GetComponent<usurper_script>().BaldoDetected();
        }
    }
}
