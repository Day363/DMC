using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer2lookat : MonoBehaviour
{
    public bool isshoot = true;
    public bool canshoot = true;
    public bool canwarning = true;

    public GameObject player;
    public GameObject cammanager;

    public List<GameObject> halolist;

    public bool look = true;
    public float anglea;

    public GameObject warning;
    public GameObject shoot;

    public float chargetime = 2f;

    public void Start()
    {
        StartCoroutine(StartCharge());
    }

    public void FixedUpdate()
    {
        if (look)
        {
            if (player != null)
            {
                transform.LookAt(player.transform);
            }
            
        }
        
    }

    IEnumerator StartCharge()
    {
        if (isshoot)
        {
            yield return new WaitForSeconds(chargetime);
            Charge();
        }
        
    }

    public void Charge()
    {
        
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        anglea = angle;

        look = false;

        if (canwarning)
        {
            if (canshoot)
            {
                Instantiate(warning, gameObject.transform.position, Quaternion.Euler(0f, 0f, angle));
            }
        }
        else
        {
            Instantiate(warning, gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        
        

        foreach (GameObject halos in halolist)
        {
            halos.GetComponent<trapal_lazer2_turn>().charge = true;
        }

        StartCoroutine(Shootco());
    }

    IEnumerator Shootco()
    {
        yield return new WaitForSeconds(0.7f);
        if (canshoot)
        {
            Shoot();
        }
        
    }

    public void Shoot2()
    {
        GameObject currentshoot = Instantiate(shoot, gameObject.transform.position, Quaternion.Euler(0f, 0f, 0));
        currentshoot.GetComponent<enemyattack>().player = player;
        foreach (GameObject halos in halolist)
        {
            halos.GetComponent<trapal_lazer2_turn>().Shoot();
        }
        cammanager.GetComponent<CameraManager>().CamVibration1();
        StartCoroutine(SelfDes());
    }

    public void Shoot()
    {
        GameObject currentshoot = Instantiate(shoot, gameObject.transform.position, Quaternion.Euler(0f, 0f, anglea));
        currentshoot.GetComponent<enemyattack>().player = player;
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

    public void Dest()
    {
        StartCoroutine(SelfDes());
    }

    IEnumerator SelfDes()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }
}
