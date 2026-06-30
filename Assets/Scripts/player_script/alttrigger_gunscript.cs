using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alttrigger_gunscript : MonoBehaviour
{
    public GameObject fractal;

    public void WhenShoot()
    {
        if (GetComponent<player_gunprefap>().attackcore.GetComponent<attackcore>().weaponsmagazine.Find(x => x.Weapon == gameObject.GetComponent<player_gunprefap>().weapon).Remainmagazine <= 0)
        {
            foreach (GameObject currentenemy in battalemanager.Instance.currentenemys)
            {
                float randomint = Random.Range(0, 361);
                GameObject currentskill = Instantiate(fractal, currentenemy.transform.position, Quaternion.Euler(0, 0, randomint));
                currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = GetComponent<player_gunprefap>().player;
                if (currentskill.TryGetComponent<player_gunprefap>(out player_gunprefap pg))
                {
                    pg.weapon = GetComponent<player_gunprefap>().weapon;
                    pg.attackcore = GetComponent<player_gunprefap>().attackcore;
                    pg.player = GetComponent<player_gunprefap>().player;
                }
            }
            
        }
    }
}
