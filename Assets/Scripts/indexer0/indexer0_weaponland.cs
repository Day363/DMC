using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_weaponland : MonoBehaviour
{
    public bool landed = false;
    public int landangle;
    public float dividnum = 4.5f;
    public string weaponname;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "indexer0")
        {
            if (!collision.gameObject.GetComponent<indexer0_script>().weapons.Contains(weaponname))
            {
                collision.gameObject.GetComponent<indexer0_script>().weapons.Add(weaponname);
                Destroy(gameObject);
            }
            
        }
            
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<Animator>().SetBool("shoot", true);
    }

    public void StartCooltime()
    {
        StartCoroutine(ShootDelay());
    }

    public void LandFinish()
    {
        landed = true;
    }

    public void Naturallanding()
    {
        landangle = Random.Range(-31, 31);
        if (landangle >= 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - landangle / dividnum, 0);
        }
        if (landangle <= 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + landangle / dividnum, 0);
        }
        transform.rotation = Quaternion.Euler(0, 0, landangle);
    }
}
