using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class draphen_boss : MonoBehaviour
{
    public GameObject player;
    public GameObject worldlight;
    public GameObject effectpos;
    public GameObject calum;
    public List<GameObject> calumList = new List<GameObject>{ };
    public bool walk;
    public float walkspped;
    public bool firstmet;
    public int direction = -1;

    public Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Animator>().SetTrigger("walk");
        walk = true;
    }

    public void Update()
    {
        if (!firstmet && Vector3.Distance(transform.position, player.transform.position) < 10)
        {
            firstmet = true;
            walk = false;
            battalemanager.Instance.Battlestart();
            GetComponent<boss_hpbar>().Attack();
            
        }

        if (walk)
        {
            rb.velocity = new Vector3(walkspped * -transform.localScale.x, rb.velocity.y);
        }
    }

    

    public void LookPlayer()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            direction = 1;
        }
        else if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            direction = -1;
        }
    }

    public void Focus1()
    {
        StartCoroutine(Focus1_co());
    }

    IEnumerator Focus1_co()
    {
        DOTween.To(() => worldlight.GetComponent<Light2D>().intensity, u => worldlight.GetComponent<Light2D>().intensity = u, 0.2f, 0.7f).SetEase(Ease.OutQuart);
        int x_ = Random.Range(23, 35);
        for (int i = 0; i < x_; i++)
        {
            float x = Mathf.Pow(Random.value, 4.5f);
            float y = Mathf.Pow(Random.value, 4.5f);

            x *= Random.Range(0, 2) == 0 ? -1 : 1;
            y *= Random.Range(0, 2) == 0 ? -1 : 1;

            Vector3 randompos = new Vector3(x * 8.5f, y * 8.5f, 0);

            GameObject curcalcum = Instantiate(calum, effectpos.transform);
            curcalcum.transform.localPosition = randompos;
            
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void CalumDisappear()
    {
       
        DOTween.To(() => worldlight.GetComponent<Light2D>().intensity, x => worldlight.GetComponent<Light2D>().intensity = x, 1f, 0.7f).SetEase(Ease.OutQuart);
    }

    public void AfterImage()
    {
        GetComponent<afterimagetest>().StartGenerate();
        //GetComponent<SpriteRenderer>().material = glitchmat;
    }

    public void EndAfterImage()
    {
        GetComponent<afterimagetest>().EndGenerate();
        //GetComponent<SpriteRenderer>().material = normalmat;

    }

    public void MoveLittleForwardTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * 50f * Vector2.left, ForceMode2D.Impulse);
        
    }

    public void MoveForwardTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * 80f * Vector2.left, ForceMode2D.Impulse);
    }

    public void MoveOverTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * 170f * Vector2.left, ForceMode2D.Impulse);
    }

    public void MovebackTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * 40f * Vector2.right, ForceMode2D.Impulse);
    }

    public void Sheathing()
    {
        GetComponent<Animator>().SetBool("sheathed", true);
    }

    public void Baldo()
    {
        GetComponent<Animator>().SetBool("sheathed", false);
    }

    public void NextAttack()
    {
        GetComponent<boss_hpbar>().Attack();
    }
}
