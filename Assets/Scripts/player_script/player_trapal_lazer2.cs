using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_trapal_lazer2 : MonoBehaviour
{
    public GameObject player;
    public float damagenum;

    public GameObject target;
    public GameObject cammanager;

    public List<GameObject> halolist;

    public bool look = true;
    public bool startcharge = true;
    public float anglea;

    public GameObject warning;
    public GameObject shoot;

    public float angle;
    public float autoangle;

    public void Start()
    {
        StartCoroutine(StartCharge());
    }

    public void FixedUpdate()
    {
        if (look)
        {
            
            if (target != null)
            {
                transform.LookAt(target.transform);
            }
           
            

        }

    }

    IEnumerator StartCharge()
    {
        if (look)
        {
            if (startcharge)
            {
                yield return new WaitForSeconds(0.4f);
                Charge();
            }
            
        }
        
        

    }

    public void Charge()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            anglea = angle;
        }
        else
        {
            anglea = autoangle;
        }
        

        look = false;

        Instantiate(warning, gameObject.transform.position, Quaternion.Euler(0f, 0f, angle));

        foreach (GameObject halos in halolist)
        {
            halos.GetComponent<trapal_lazer2_turn>().charge = true;
        }

        StartCoroutine(Shootco());
    }

    IEnumerator Shootco()
    {
        yield return new WaitForSeconds(0.3f);
        Shoot();
    }

    public void Shoot()
    {
        if (target == null)
        {
            anglea = autoangle;
        }
        
        GameObject curlazer = Instantiate(shoot, gameObject.transform.position, Quaternion.Euler(0f, 0f, anglea));
        curlazer.GetComponent<playerattackdamage>().player = player;
        curlazer.GetComponent<playerattackdamage>().damagenum = damagenum;
        foreach (GameObject halos in halolist)
        {
            halos.GetComponent<trapal_lazer2_turn>().Shoot();
        }
        cammanager.GetComponent<CameraManager>().CamVibration1();
        StartCoroutine(SelfDes());
    }

    public void ShootNotDes()
    {
        foreach (GameObject halos in halolist)
        {
            halos.GetComponent<trapal_lazer2_turn>().LittleShoot();
        }
        cammanager.GetComponent<CameraManager>().CamVibration1();
    }

    public void SelfDesM()
    {
        StartCoroutine(SelfDes());
    }

    IEnumerator SelfDes()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }
}
