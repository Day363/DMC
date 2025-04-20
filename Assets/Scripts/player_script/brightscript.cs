using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brightscript : MonoBehaviour
{
    [SerializeField]
    public GameObject attackcore;

    public void CantAttack()
    {
        if (attackcore.GetComponent<attackcore>().isdelay)
        {
            attackcore.GetComponent<attackcore>().canattack = false;
            attackcore.GetComponent<attackcore>().canrotate = false;
        }
    }

    public void SetCoolEnd()
    {
        attackcore.GetComponent<attackcore>().canattack = true;
        attackcore.GetComponent<attackcore>().canrotate = true;
    }

    public void Setdisappear()
    {
        gameObject.SetActive(false);

    }
}
