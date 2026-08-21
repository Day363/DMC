using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashcontrol : MonoBehaviour
{
    public arceffectlossdraphen slash1_1;
    public arceffectlossdraphen slash1_2;
    public List<GameObject> slashs = new List<GameObject> { };
    
    public void Slash1()
    {
        slash1_1.Decrease();
    }

    public void Slash2()
    {
        slash1_2.Decrease();
    }

    public void Active()
    {
        slash1_1.Increase();
    }

    public void HitBox(int i)
    {
        GameObject slash = slashs[i];
        StartCoroutine(HitBoxActive(slash));
    }

    IEnumerator HitBoxActive(GameObject slash)
    {
        

        slash.SetActive(true);

        slash.GetComponent<PolygonCollider2D>().enabled = true;
        

        yield return new WaitForSeconds(0.1f);
        slash.GetComponent<enemyattack>().hit = true;
        

        yield return new WaitForSeconds(0.1f);
        slash.GetComponent<PolygonCollider2D>().enabled = false;
        slash.GetComponent<enemyattack>().hit = false;
        slash.SetActive(false);
    }
}
