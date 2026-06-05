using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class piereceeffect : MonoBehaviour
{
    public void Start()
    {
        transform.GetChild(0).transform.localPosition = new Vector3(transform.GetChild(0).transform.localPosition.x + Random.Range(-0.2f, 0.2f), transform.GetChild(0).transform.localPosition.y + Random.Range(-0.3f, 0.3f), 0);
        transform.GetChild(0).transform.DOLocalMoveX(transform.GetChild(0).transform.localPosition.x - 1.5f, 0.1f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0.15f);
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.16f);
        Destroy(gameObject);
    }
}
