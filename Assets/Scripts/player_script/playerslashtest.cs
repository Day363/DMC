using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerslashtest : MonoBehaviour
{
    public GameObject[] slashs;
    public bool canattack = true;
    public int slashnumber = 0;
    
    public void Update()
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // ������Ʈ���� ���콺 ��ġ���� ���� ���� ���
        Vector3 direction = mousePosition - transform.position;

        // ���� ��� (������ ���� ��ȯ)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ������Ʈ ȸ�� ����
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (canattack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (slashnumber == 2)
                {
                    slashnumber = 0;
                }
                slashs[slashnumber].SetActive(true);
                slashs[slashnumber].GetComponent<Animator>().SetBool("attack", true);
                slashnumber = slashnumber + 1;
            }
        }
        
    }
}


