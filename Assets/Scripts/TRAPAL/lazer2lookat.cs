using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer2lookat : MonoBehaviour
{
    public bool isshoot = true;

    public GameObject player;
    public GameObject cammanager;

    public List<GameObject> halolist;

    public bool look = true;
    public float anglea;

    public GameObject warning;
    public GameObject shoot;

    public void Start()
    {
        StartCoroutine(StartCharge());
    }

    public void FixedUpdate()
    {
        if (look)
        {
            transform.LookAt(player.transform);
        }
        
    }

    IEnumerator StartCharge()
    {
        if (isshoot)
        {
            yield return new WaitForSeconds(2f);
            Charge();
        }
        
    }

    public void Charge()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        anglea = angle;

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
        yield return new WaitForSeconds(0.7f);
        Shoot();
    }

    public void Shoot()
    {
        Instantiate(shoot, gameObject.transform.position, Quaternion.Euler(0f, 0f, anglea));
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

    IEnumerator SelfDes()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }
}
