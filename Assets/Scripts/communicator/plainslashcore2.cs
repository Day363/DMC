using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class plainslashcore2 : MonoBehaviour
{
    public GameObject slash;

    public float time;
    public float maxscale;
    public float minscale;

    public float radius;


    public void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Quaternion rot = transform.parent != null ? transform.parent.rotation : transform.rotation;

        GameObject curslash = Instantiate(slash, transform.position, rot);

        float scale = Random.Range(minscale, maxscale);
        curslash.transform.localScale = new Vector3(scale, scale, 1);

        curslash.transform.Rotate(0, 0, Random.Range(0f, 360f));

        yield return new WaitForSeconds(time);
        StartCoroutine(Spawn());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 prevPoint = transform.position + new Vector3(radius, 0, 0);

        for (int i = 1; i <= 64; i++)
        {
            float angle = i * Mathf.PI * 2f / 64;

            Vector3 nextPoint = transform.position +
                new Vector3(
                    Mathf.Cos(angle) * radius,
                    Mathf.Sin(angle) * radius,
                    0
                );

            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
}
