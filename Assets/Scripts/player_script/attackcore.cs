using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackcore : MonoBehaviour
{
    public GameObject player;
    public List<Normalskill> attacklist;
    public bool canattack = true;
    public bool isdelay = true;
    public int attacknumber = 0;
    public float angle;
    public GameObject currentskill;

    public void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (canattack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentskill =  Instantiate(attacklist[attacknumber].skillprefab, transform.position, transform.rotation);
                currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                attacknumber = attacknumber + 1;
                if (attacknumber == attacklist.Count)
                {
                    attacknumber = 0;
                }
            }

        }
    }

    
}
