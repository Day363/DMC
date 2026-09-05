using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alt_script : MonoBehaviour
{
    public GameObject target;
    public int direction;
    public GameObject wind;
    public GameObject effectpos;
    public GameObject guneffectpos1;
    public GameObject guneffectpos2;
    public GameObject guneffect;
    public GameObject bullet;

    public void LookPlayer()
    {
        if (transform.position.x > target.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            direction = 1;
        }
        else if (transform.position.x < target.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            direction = -1;
        }
    }

    public void MoveLittleForwardTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * 15f * Vector2.left, ForceMode2D.Impulse);

    }

    public void MoveForwardTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * 50f * Vector2.left, ForceMode2D.Impulse);
    }

    public void MoveOverTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * 170f * Vector2.left, ForceMode2D.Impulse);
    }

    public void MovebackTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * 10f * Vector2.right, ForceMode2D.Impulse);
    }

    public void MoveLittlebackTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * 3f * Vector2.right, ForceMode2D.Impulse);
    }

    public void Wind1()
    {
        GameObject currentwind = Instantiate(wind, effectpos.transform);
        currentwind.transform.localPosition = new Vector3(0, 0, 0);
        StartCoroutine(EffectKill(currentwind));
    }

    public void Wind2()
    {
        GameObject currentwind = Instantiate(wind, effectpos.transform);
        currentwind.transform.localPosition = new Vector3(0, 0, 0);
        currentwind.transform.rotation = Quaternion.Euler(Random.Range(-25f, 25f), 0, Random.Range(-25f, 25f));
        StartCoroutine(EffectKill(currentwind));
    }

    public void GunEffect1()
    {
        GameObject cureffect = Instantiate(guneffect, guneffectpos1.transform);
        cureffect.transform.localPosition = new Vector3(0, 0, 0);
        cureffect.transform.localRotation = Quaternion.Euler(cureffect.transform.localRotation.x, cureffect.transform.localRotation.y, guneffectpos1.transform.rotation.z);
        float add = Random.Range(0.7f, 1.3f);
        cureffect.transform.localScale = new Vector3(cureffect.transform.localScale.x * add, cureffect.transform.localScale.y * add, 1);
        StartCoroutine(EffectKill(cureffect));

        GameObject curbullet = Instantiate(bullet, guneffectpos1.transform.position, Quaternion.Euler(0, 0, guneffectpos1.transform.localEulerAngles.z * direction + Random.Range(-2.5f, 2.5f)));
        StartCoroutine(EffectKill(curbullet));
    }

    public void GunEffect2()
    {
        GameObject cureffect = Instantiate(guneffect, guneffectpos2.transform);
        cureffect.transform.localPosition = new Vector3(0, 0, 0);
        cureffect.transform.localRotation = Quaternion.Euler(cureffect.transform.localRotation.x, cureffect.transform.localRotation.y, guneffectpos2.transform.rotation.z);
        float add = Random.Range(0.7f, 1.3f);
        cureffect.transform.localScale = new Vector3(cureffect.transform.localScale.x * add, cureffect.transform.localScale.y * add, 1);
        StartCoroutine(EffectKill(cureffect));

        GameObject curbullet = Instantiate(bullet, guneffectpos2.transform.position, Quaternion.Euler(0, 0, guneffectpos2.transform.localEulerAngles.z * direction + Random.Range(-2.5f, 2.5f)));
        StartCoroutine(EffectKill(curbullet));
    }

    IEnumerator EffectKill(GameObject effect)
    {
        yield return new WaitForSeconds(3f);
        Destroy(effect);
    }

    public void NextAttack()
    {
        GetComponent<boss_hpbar>().Attack();
    }
}
