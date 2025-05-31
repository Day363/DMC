using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_terminal : MonoBehaviour
{
    public GameObject[] buttons;
    public int count;
    public float blanktime;

    private void Start()
    {
        StartCoroutine(Setbutton());
    }

    IEnumerator Setbutton()
    {
        yield return new WaitForSeconds(0.05f);
        count = Random.Range(0, 25);
        buttons[count].SetActive(true);
        StartCoroutine(Blank(buttons[count]));
        StartCoroutine(Setbutton());
    }

    IEnumerator Blank(GameObject button)
    {
        blanktime = Random.Range(0.1f, 1f);
        yield return new WaitForSeconds(blanktime);
        button.SetActive(false);
    }
}
