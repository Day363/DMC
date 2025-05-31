using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_gun : MonoBehaviour
{
    public float radius = 5f;                  
    public float moveDuration = 2f;            
    public GameObject player;
    public GameObject bullet;
    public float power;
    public GameObject trail;
    public bool trigger = false;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    public Quaternion bulletrot;
    private float elapsedTime = 0f;
    private bool moving = true;
    private Quaternion startRotation;

    void Start()
    {
        GetComponent<trapal_gun_rotate>().target = player.transform;
        // 상단 반원에서 랜덤 각도
        float angle = Random.Range(0f, 180f) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;

        startPosition = transform.position;
        targetPosition = player.transform.position + offset;

        Vector2 toPlayer = (player.transform.position - transform.position).normalized;
        Vector2 awayFromPlayer = -toPlayer;
        float awayAngle = Mathf.Atan2(awayFromPlayer.y, awayFromPlayer.x) * Mathf.Rad2Deg + 90;
        startRotation = Quaternion.Euler(0f, 0f, awayAngle);

        transform.rotation = startRotation;
    }

    void FixedUpdate()
    {
        if (!moving) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / moveDuration);

        // 감속 곡선 (Ease Out: 빠르게 시작해서 느리게 끝남)
        float curvedT = 1f - Mathf.Pow(1f - t, 2f); // 감속(2차) 커브

        transform.position = Vector3.Lerp(startPosition, targetPosition, curvedT);

        if (t >= 1f)
        {
            moving = false;
            StartCoroutine(ShootDelay());
            
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1.4f);
        GetComponent<trapal_gun_rotate>().shoot = true;
        bulletrot = Quaternion.Euler(0, 0, transform.eulerAngles.z - 90f);
        GameObject currentbullet = Instantiate(bullet, transform.position, bulletrot);
        currentbullet.GetComponent<trapal_bullet>().player = player;
        float angle = currentbullet.transform.eulerAngles.z;
        GetComponent<Animator>().SetBool("shoot", true);
        yield return new WaitForSeconds(0.7f);
        trail.SetActive(true);
        float radians = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        trigger = true;
        GetComponent<Rigidbody2D>().AddForce(direction * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
