using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounselManager_test : MonoBehaviour
{
    public GameObject counselingentity;
    public GameObject answerbuttonset;

    public void Response1()
    {
        answerbuttonset.SetActive(false);
        counselingentity.GetComponent<counseltext_test>().bundlenumber = counselingentity.GetComponent<counseltext_test>().route1;
        counselingentity.GetComponent<counseltext_test>().answeringnow = false;
        counselingentity.GetComponent<counseltext_test>().telling = false;
        counselingentity.GetComponent<counseltext_test>().textnumber = 0;
    }

    public void Response2()
    {
        answerbuttonset.SetActive(false);
        counselingentity.GetComponent<counseltext_test>().bundlenumber = counselingentity.GetComponent<counseltext_test>().route2;
        counselingentity.GetComponent<counseltext_test>().answeringnow = false;
        counselingentity.GetComponent<counseltext_test>().telling = false;
        counselingentity.GetComponent<counseltext_test>().textnumber = 0;
    }

    public void Response3()
    {
        answerbuttonset.SetActive(false);
        counselingentity.GetComponent<counseltext_test>().bundlenumber = counselingentity.GetComponent<counseltext_test>().route3;
        counselingentity.GetComponent<counseltext_test>().answeringnow = false;
        counselingentity.GetComponent<counseltext_test>().telling = false;
        counselingentity.GetComponent<counseltext_test>().textnumber = 0;
    }
}
