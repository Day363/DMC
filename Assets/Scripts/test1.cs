using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class test1 : MonoBehaviour
{
    public GameObject player;
    public GameObject eye;
    public GameObject eyeposition;
    public GameObject slasheffect;
    public GameObject spaceslash;
    public GameObject croiz;
    public GameObject cronometer;
    public GameObject jail;
    public GameObject curjail;
    public GameObject effect3;
    public GameObject effect4;
    public GameObject effectpos;

    public GameObject bookcleaner;
    public Light2D globallight;

    public float dash1power;
    public float dash2power;
    public float movespeed;

    public bool whileattack = false;
    public bool canwalk = false;
    public int direction = 1;

    public void FixedUpdate()
    {
        if (canwalk && !whileattack && transform.parent.position.x - player.transform.position.x < 0)
        {
            direction = 1;
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }
        else if (canwalk && !whileattack && transform.parent.position.x - player.transform.position.x > 0)
        {
            direction = -1;
            transform.parent.localScale = new Vector3(1, 1, 1);
        }

        if (!whileattack && Vector2.Distance(transform.parent.position, player.transform.position) < 10)
        {
            transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetBool("walk", false);
            whileattack = true;
            canwalk = false;
            Attack();
        }
        else if (!whileattack && canwalk && Vector2.Distance(transform.parent.position, player.transform.position) > 10)
        {
            GetComponent<Animator>().SetBool("walk", true);
            transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(movespeed * direction, 0);
        }
    }

    public void LookPlayer()
    {
        if (player.transform.position.x < transform.parent.position.x)
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Move()
    {
        DOTween.Kill("move");
        if (player.transform.position.x < transform.parent.position.x)
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
            float x = player.transform.position.x - Random.Range(5, 10);
            transform.parent.DOMoveX(x, 0.3f).SetEase(Ease.OutExpo).SetId("move");
        }
        else if (player.transform.position.x > transform.parent.position.x)
        {
            transform.parent.localScale = new Vector3(-1, 1, 1);
            float x = player.transform.position.x + Random.Range(5, 10);
            transform.parent.DOMoveX(x, 0.3f).SetEase(Ease.OutExpo).SetId("move");
        }

    }

    public void Move2()
    {
        DOTween.Kill("move");
        if (player.transform.position.x < transform.parent.position.x)
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
            float x = player.transform.position.x + 3f;
            transform.parent.DOMoveX(x, 0.3f).SetEase(Ease.OutExpo).SetId("move");
        }
        else if (player.transform.position.x > transform.parent.position.x)
        {
            transform.parent.localScale = new Vector3(-1, 1, 1);
            float x = player.transform.position.x - 3f;
            transform.parent.DOMoveX(x, 0.3f).SetEase(Ease.OutExpo).SetId("move");
        }
    }

    public void Move3()
    {
        DOTween.Kill("move");
        if (player.transform.position.x < transform.parent.position.x)
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
            float x = player.transform.position.x - 10f;
            transform.parent.DOMoveX(x, 0.15f).SetEase(Ease.OutExpo).SetId("move");
        }
        else if (player.transform.position.x > transform.parent.position.x)
        {
            transform.parent.localScale = new Vector3(-1, 1, 1);
            float x = player.transform.position.x + 10f;
            transform.parent.DOMoveX(x, 0.15f).SetEase(Ease.OutExpo).SetId("move");
        }
    }

    public void EyeSpawn()
    {
        GameObject cureye = Instantiate(eye, eyeposition.transform);
        cureye.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void Slasheffect2()
    {
        GameObject cureffect = Instantiate(slasheffect, player.transform.position, Quaternion.Euler(0, 0, Random.Range(135f, 45f)));
        cureffect.transform.position = player.transform.position;
        cureffect = Instantiate(slasheffect, player.transform.position, Quaternion.Euler(0, 0, Random.Range(135f, 45f)));
        cureffect.transform.position = player.transform.position;
    }

    public void TimeSlow()
    {
        StartCoroutine(TimeSlow_co());
    }

    IEnumerator TimeSlow_co()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 1f;
    }

    public void SpaceSlashSpawn()
    {
        GameObject curslash = Instantiate(spaceslash, player.transform.position, Quaternion.Euler(0, 0, Random.Range(10f, -10f)));
        curslash.transform.position = player.transform.position;
    }



    public void LightsDown()
    {
        StartCoroutine(LightsDown(globallight));
    }

    IEnumerator LightsDown(Light2D light)
    {
        float i = light.intensity;
        DOTween.To(() => light.intensity, x => light.intensity = x, 5f, 0.7f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late);
        yield return new WaitForSeconds(1f);
        DOTween.To(() => light.intensity, x => light.intensity = x, i, 3f).SetEase(Ease.InQuad).SetUpdate(UpdateType.Late);
    }

    public void CroizAppear()
    {
        croiz.GetComponent<croiz>().Appear();
    }

    public void CroizUp()
    {
        croiz.GetComponent<croiz>().Up();
    }

    public void CroizDown()
    {
        croiz.GetComponent<croiz>().Down();
        StartCoroutine(Start_co());
        curjail = Instantiate(jail, player.transform);
        player.GetComponent<Animator>().SetBool("stiffness", true);
        player.GetComponent<PlayerMove>().canmove = false;
    }

    IEnumerator Start_co()
    {
        yield return new WaitForSeconds(3.5f);
        GetComponent<Animator>().SetTrigger("start");
    }

    public void ClockAppear()
    {
        cronometer.GetComponent<cronometer0>().FadeIn();
    }

    public void PlayerNuckBack()
    {
        if (transform.parent.position.x > player.transform.position.x)
        {
            player.transform.DOMoveX(player.transform.position.x - 5f, 0.5f).SetEase(Ease.OutQuart);
        }
        else
        {
            player.transform.DOMoveX(player.transform.position.x + 5f, 0.5f).SetEase(Ease.OutQuart);
        }

    }

    public void JailBreak()
    {
        Instantiate(effect3, player.transform.position, Quaternion.identity);
        Destroy(curjail);
    }

    public void CroizDisappear()
    {
        croiz.GetComponent<croiz>().Disappear();
    }

    public void PlayerRealease()
    {
        player.GetComponent<Animator>().SetBool("stiffness", false);
        player.GetComponent<PlayerMove>().canmove = true;
    }

    public void SpawnEffect4()
    {
        StartCoroutine(SpawnEffect4_co());
    }

    IEnumerator SpawnEffect4_co()
    {
        GameObject cureffect = Instantiate(effect4);
        cureffect.transform.DOScaleX(1, 0.7f).SetEase(Ease.OutQuart);
        cureffect.transform.position = effectpos.transform.position;
        yield return new WaitForSeconds(5f);
        Destroy(cureffect);
    }

    public void Attack()
    {
        whileattack = true;
        int i = Random.Range(0, 3);
        if (i == 0)
        {
            GetComponent<Animator>().SetTrigger("attack1");
        }
        else if (i == 1)
        {
            GetComponent<Animator>().SetTrigger("attack2");
        }
        else if (i == 2)
        {
            GetComponent<Animator>().SetTrigger("attack3");
        }
    }

    public void AttackStart()
    {
        whileattack = true;
        canwalk = false;
    }

    public void AttackEnd()
    {
        whileattack = false;
        canwalk = true;
    }

    public void Dash1()
    {
        transform.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.left * transform.parent.localScale.x * dash1power, ForceMode2D.Impulse);
    }

    public void Dash2()
    {
        transform.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.left * transform.parent.localScale.x * dash2power, ForceMode2D.Impulse);
    }

    public void BattleStart()
    {
        PlayerRealease();
        battalemanager.Instance.Battlestart();
        canwalk = true;
    }
}
