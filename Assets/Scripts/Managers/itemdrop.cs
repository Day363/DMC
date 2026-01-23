using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class dropdox
{
    public GameObject item;
    public int count;
    public float percent;
}

public class itemdrop : MonoBehaviour
{
    public List<dropdox> itemdroplist;
    public GameObject dropposition;
    public Vector2 sidepower;
    public Vector2 uppower;

    public void Drop()
    {
        foreach (dropdox itembox in itemdroplist)
        {
            float rand = Random.Range(0f, 1f);

            if (rand < itembox.percent)
            {
                for (int i = 0; i < itembox.count; i++)
                {
                    GameObject curitem = Instantiate(itembox.item, dropposition.transform.position, Quaternion.identity);
                    curitem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(sidepower.x, sidepower.y), Random.Range(uppower.x, uppower.y)), ForceMode2D.Impulse);
                }
            }
        }
    }
}
