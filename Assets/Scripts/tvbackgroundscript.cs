using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tvbackgroundscript : MonoBehaviour
{
    public int randomint;
    public bool isdelay = false;
    public int countstart;
    public int count;
    public int cooltime;
    public int timecool;

    public void Awake()
    {
        countstart = Random.Range(1, timecool);
        count = countstart;
    }

    private void Update()
    {
        randomint = Random.Range(1, 100);

        if (!isdelay)
        {
            if (randomint <= 5)
            {
                GetComponent<Animator>().SetBool("red", true);
                isdelay = true;
            }

            if (randomint <= 15 && randomint > 5)
            {
                GetComponent<Animator>().SetBool("zzz", true);
                isdelay = true;
            }

            if (randomint > 15 && randomint <= 20)
            {
                GetComponent<Animator>().SetBool("eye", true);
                isdelay = true;
            }

            if (randomint > 20 && randomint < 30)
            {
                GetComponent<Animator>().SetBool("beep", true);
                isdelay = true;
            }
        }
        
    }

    private void FixedUpdate()
    {
        count = count + 1;
        if (count > cooltime)
        {
            GetComponent<Animator>().SetBool("red", false);
            GetComponent<Animator>().SetBool("zzz", false);
            GetComponent<Animator>().SetBool("eye", false);
            GetComponent<Animator>().SetBool("beep", false);
        }

        if (count > timecool)
        {
            count = 0;
            isdelay = false;
        }
    }
}
