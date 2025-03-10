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
        // 마우스 위치를 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // 오브젝트에서 마우스 위치로의 방향 벡터 계산
        Vector3 direction = mousePosition - transform.position;

        // 각도 계산 (라디안을 도로 변환)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 오브젝트 회전 적용
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


