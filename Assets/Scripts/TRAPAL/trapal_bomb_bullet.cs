using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_bomb_bullet : MonoBehaviour
{
    public GameObject deny;
    public GameObject particle;
    public GameObject cameramanager;

    public Vector2 forcex;
    public Vector2 forcey;

    public void Start()
    {
        Vector2 force = new Vector2(Random.Range(forcex.x, forcey.y), Random.Range(forcey.x, forcey.y));
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Floor")
        {
            StartCoroutine(Bombco());
        }
    }

    IEnumerator Bombco()
    {
        GameObject currentdeny = Instantiate(deny, transform.position, Quaternion.identity);
        cameramanager.GetComponent<CameraManager>().CamVibration1();
        Destroy(particle);
        currentdeny.transform.localScale = new Vector3(0, 0, 1f);
        currentdeny.transform.DOScale(new Vector3(6f, 6f, 1f), 0.2f).SetEase(Ease.OutExpo).SetId("dobif");
        yield return new WaitForSeconds(0.2f);
        DOTween.Kill("dobif");
        currentdeny.transform.DOScale(new Vector3(0f, 0f, 1f), 0.6f).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(0.7f);
        Destroy(currentdeny);
        Destroy(gameObject);
    }
}
