using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class indexer0_weaponland : MonoBehaviour
{
    public bool landed = false;
    public int landangle;
    public float dividnum = 4.5f;
    public int effectindex;
    public GameObject rainmanager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ADafa");
        if (collision.gameObject.name == "indexer0")
        {
            Debug.Log("adafa");
            boss_hpbar.StackInstance effectinstance = collision.gameObject.GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == battalemanager.Instance.stackdatas[effectindex].effectName);
            if (effectinstance == null || effectinstance.currentStack < 2)
            {
                Debug.Log("qt3eaf");
                collision.gameObject.GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[effectindex], 1);
                collision.gameObject.GetComponent<indexer0_script>().RainmanagerCount();
                GetComponent<Animator>().SetBool("teleport", true);
            }
            
        }
            
    }

    public void CamVib()
    {
        battalemanager.Instance.cameramanager.GetComponent<CameraManager>().CamVibration0_5();
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
        GetComponent<enemyattack>().canattack = false;
        GetComponent<enemyattack>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
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

    public void Destroyself()
    {
        Destroy(gameObject);
    }

    public void SoundPlay()
    {
        soundmanager soundmanager = battalemanager.Instance.gameObject.GetComponent<soundmanager>();
        int i = Random.Range(0, 5);
        if (i == 0)
        {
            soundmanager.SoundPlay("indexer0_weaponland1");
        }
        else if (i == 1)
        {
            soundmanager.SoundPlay("indexer0_weaponland2");
        }
        else if (i == 2)
        {
            soundmanager.SoundPlay("indexer0_weaponland3");
        }
        else if (i == 3)
        {
            soundmanager.SoundPlay("indexer0_weaponland4");
        }
        else if (i == 4)
        {
            soundmanager.SoundPlay("indexer0_weaponland5");
        }
    }
}
