using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class descriptionUI : MonoBehaviour
{
    public GameObject myUi;
    public GameObject otherUi1;
    public GameObject otherUi2;

    public void SetDesc()
    {
        myUi.SetActive(true);
        if (otherUi1 != null)
        {
            otherUi1.SetActive(false);
        }
        if (otherUi2 != null)
        {
            otherUi2.SetActive(false);
        }

        
    }
}
