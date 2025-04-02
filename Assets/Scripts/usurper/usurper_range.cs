using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usurper_range : MonoBehaviour
{
    public GameObject target;
    public float rotspeed;
    public bool canrot = false;
    public Vector3 moveDir;
    public float rotateamount;

    private void Update()
    {
        if (canrot)
        {
            Vector3 moveDir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));

        }
    }

    public void RotStart()
    {
        canrot = true;
    }

    public void RotStop()
    {
        canrot = false;
    }

    public void StartShoot()
    {
        StartCoroutine(Range());
    }

    IEnumerator Range()
    {
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetBool("short", true);
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("shoot", true);
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("bow", true);
    }
}
