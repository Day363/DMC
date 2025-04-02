using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usurper_star_script : MonoBehaviour
{
    public GameObject layer;
    public GameObject player;
    public GameObject misile;
    public int ang;
    public float movespeed;
    public float uppos;
    public float turnspped;
    public float turn;
    public bool canturn = false;
    public bool canmove = true;
    public Vector2 movepos;
    public Quaternion bulletang;


    public void Update()
    {
        if (canmove)
        {
            movepos = new Vector2(player.transform.position.x, player.transform.position.y + uppos);
            gameObject.transform.position = Vector2.MoveTowards(transform.position, movepos, movespeed);
        }

        if(canturn)
        {
            turn = turn + turnspped;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, turn);
            layer.transform.rotation = Quaternion.Euler(0, 0, turn * 1.5f);
        }
    }

    public void Attack4()
    {
        StartCoroutine(bulletdelay());
    }

    IEnumerator bulletdelay()
    {
        ang = Random.Range(1, 361);
        bulletang = Quaternion.Euler(0, 0, ang);
        misile.GetComponent<usurper_bullet_script>().target = player;
        Instantiate(misile, gameObject.transform.position, bulletang);
        ang = Random.Range(1, 361);
        bulletang = Quaternion.Euler(0, 0, ang);
        yield return new WaitForSeconds(0.5f);
        Instantiate(misile, gameObject.transform.position, bulletang);
        ang = Random.Range(1, 361);
        bulletang = Quaternion.Euler(0, 0, ang);
        yield return new WaitForSeconds(0.5f);
        Instantiate(misile, gameObject.transform.position, bulletang);
        ang = Random.Range(1, 361);
        yield return new WaitForSeconds(0.5f);
        bulletang = Quaternion.Euler(0, 0, ang);
        Instantiate(misile, gameObject.transform.position, bulletang);
    }
    

    public void Layerappear()
    {
        layer.SetActive(true);
    }

    public void Turnstart()
    {
        canturn = true;
    }
}
