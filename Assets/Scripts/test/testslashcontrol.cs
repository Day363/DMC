using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testslashcontrol : MonoBehaviour
{
    public arceffectloss slash1_1;
    public arceffectloss slash1_2;
    public arceffectloss slash1_3;
    public arceffectloss slash1_4;
    public arceffectloss slash1_5;
    public arceffectloss slash1_6;
    public GameObject hitbox1;
    public GameObject hitbox2;
    public GameObject piercehitbox;
    public GameObject pincers_hitbox;

    private Coroutine curcoroutine;
    private Coroutine curcoroutine2;

    public void Slash1()
    {
        slash1_1.Decrease();
        slash1_2.Decrease();
        slash1_3.Decrease();
        slash1_4.Decrease();
        slash1_5.Decrease();
        slash1_6.Decrease();

        
    }

    public void HitBox(float values)
    {

        if (curcoroutine != null)
        {
            StopCoroutine(curcoroutine);
        }

        curcoroutine = StartCoroutine(HitBoxActive(values, values));
    }

    public void PiercehitBox(float values)
    {

        if (curcoroutine2 != null)
        {
            StopCoroutine(curcoroutine2);
        }

        curcoroutine2 = StartCoroutine(PiercehitBox_co(values));
    }

    public void Pincershitbox()
    {
        curcoroutine2 = StartCoroutine(Pincershitbox_co());
    }

    IEnumerator PiercehitBox_co(float calulation1)
    {
        piercehitbox.SetActive(true);
        piercehitbox.GetComponent<enemyattack>().calculation = calulation1;

        piercehitbox.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        piercehitbox.GetComponent<enemyattack>().hit = true;
        yield return new WaitForSeconds(0.1f);
        piercehitbox.GetComponent<BoxCollider2D>().enabled = false;
        piercehitbox.GetComponent<enemyattack>().hit = false;
        piercehitbox.SetActive(false);
    }

    IEnumerator Pincershitbox_co()
    {
        pincers_hitbox.SetActive(true);

        pincers_hitbox.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        pincers_hitbox.GetComponent<communicator2_pincers_hitbox>().hit = true;
        yield return new WaitForSeconds(0.5f);
        pincers_hitbox.GetComponent<BoxCollider2D>().enabled = false;
        pincers_hitbox.GetComponent<communicator2_pincers_hitbox>().hit = false;
        pincers_hitbox.SetActive(false);
    }

    IEnumerator HitBoxActive(float calulation1, float calulation2)
    {
        hitbox1.SetActive(true);
        if (hitbox2 != null)
        {
            hitbox2.SetActive(true);
        }

        hitbox1.GetComponent<enemyattack>().calculation = calulation1;
        if (hitbox2 != null)
        {
            hitbox2.GetComponent<enemyattack>().calculation = calulation2;
        }
        

        hitbox1.GetComponent<PolygonCollider2D>().enabled = true;
        if (hitbox2 != null)
        { 
            hitbox2.GetComponent<PolygonCollider2D>().enabled = true;
        }
            
        yield return new WaitForSeconds(0.05f);
        hitbox1.GetComponent<enemyattack>().hit = true;
        if (hitbox2 != null)
        {
            hitbox2.GetComponent<enemyattack>().hit = true;
        }
            
        yield return new WaitForSeconds(0.1f);
        hitbox1.GetComponent<PolygonCollider2D>().enabled = false;
        hitbox1.GetComponent<enemyattack>().hit = false;
        if (hitbox2 != null)
        {
            hitbox2.GetComponent<PolygonCollider2D>().enabled = false;
            hitbox2.GetComponent<enemyattack>().hit = false;
        }

        hitbox1.SetActive(false);
        if (hitbox2 != null)
        {
            hitbox2.SetActive(false);
        }


    }
}
