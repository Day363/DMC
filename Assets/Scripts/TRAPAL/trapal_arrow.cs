using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_arrow : MonoBehaviour
{
    public GameObject trapal;
    public GameObject player;
    public GameObject effect;

    public trapal_passive tp;

    public void Start()
    {
        trapal_passive.OnCertainEnd += Judge;
        tp = trapal.GetComponent<trapal_passive>();

        transform.localScale = Vector3.zero;

        float scale = Random.Range(0.5f, 1.5f);

        transform.DOScale(new Vector3(scale, scale, 1f), 1f).SetEase(Ease.OutQuart);

        Vector2 direction = trapal.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.DORotate(new Vector3(0, 0, angle + 1800 - 90f), 3f, RotateMode.FastBeyond360).SetEase(Ease.OutQuart);
    }

    public void Judge()
    {
        if (tp.certaindestroy >= 12)
        {
            ShotTrapal();
        }
        else
        {
            PlayerTrapal();
        }
    }

    public void ShotTrapal()
    {
        StartCoroutine(ShotTrapal_co());
    }

    IEnumerator ShotTrapal_co()
    {
        Vector2 direction = trapal.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.DORotate(new Vector3(0, 0, angle - 90f), 0.1f, RotateMode.FastBeyond360).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.1f);
        transform.DOMove(trapal.transform.position, 0.1f);
        trapal_passive.OnCertainEnd -= Judge;
        Instantiate(effect, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().DOFade(0, 3f);
        yield return new WaitForSeconds(3.1f);
        Destroy(gameObject);
    }

    public void PlayerTrapal()
    {
        StartCoroutine(PlayerTrapal_co());
    }

    IEnumerator PlayerTrapal_co()
    {
        player.GetComponent<PlayerMove>().canmove = false; 

        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.DORotate(new Vector3(0, 0, angle - 90f), 0.1f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.1f);
        transform.DOMove(player.transform.position, 0.1f);
        trapal_passive.OnCertainEnd -= Judge;
        Instantiate(effect, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().DOFade(0, 3f);
        yield return new WaitForSeconds(3.1f);
        Destroy(gameObject);
        
    }
}
