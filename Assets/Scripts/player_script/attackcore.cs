using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackcore : MonoBehaviour
{
    public List<GameObject> attacklist;
    public List<GameObject> cyclecount;
    public GameObject testattackcore;
    public bool canattack = true;
    public bool isdelay = true;
    public int attacknumber = 0;

    public void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (canattack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                attacklist[attacknumber].SetActive(true);
                attacknumber = attacknumber + 1;
                if (attacknumber == attacklist.Count)
                {
                    attacknumber = 0;
                    testattackcore.GetComponent<playerslashtest>().PassiveCycle();
                }
            }

        }
    }

    
}
