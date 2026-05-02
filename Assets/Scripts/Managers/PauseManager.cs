using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public float currenttimescale;

    public bool canpause = false;

    public bool ispause = false;
    public bool manupause = false;
    public bool escapeManu = false;

    private void Update()
    {
        if (ispause || manupause)
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !escapeManu && canpause)
        {
            currenttimescale = Time.timeScale;
            escapeManu = true;
            manupause = true;

            uimanager.Instance.pausemanu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && escapeManu)
        {
            Continue();
        }
    }

    public void Continue()
    {
        uimanager.Instance.uicronometer.GetComponent<uicronometer>().Restart();

        escapeManu = false;
        manupause = false;
        Time.timeScale = currenttimescale;

        uimanager.Instance.pausemanu.SetActive(false);
    }

    public void Continue_slow()
    {
        uimanager.Instance.uicronometer.GetComponent<uicronometer>().Restart_slow();

        canpause = false;
        escapeManu = false;
    }

}
