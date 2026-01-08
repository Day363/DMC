using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gamerestart : MonoBehaviour
{
    public GameObject cronometer;


    public void Restart()
    {
        cronometer.GetComponent<cronometer_script>().RestartTurn();
    }

    IEnumerator Restart_co()
    {
        yield return new WaitForSeconds(7.5f);
        SceneManager.LoadScene("mirrorselect");
    }
}
