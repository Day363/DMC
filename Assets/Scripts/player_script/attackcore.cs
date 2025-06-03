using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReady
{
    public string Skillname { get; set; }
    public List<Skill> Enforceskills { get; set; }
    public Skill Normalskill { get; set; }
    public Skill Amalgamed { get; set; }

    public SkillReady(string skillname ,List<Skill> enforceskills, Skill normalskill, Skill amalgamed)
    {
        Skillname = skillname;
        Enforceskills = enforceskills;
        Normalskill = normalskill;
        Amalgamed = amalgamed;
    }
}

public class attackcore : MonoBehaviour
{
    public bool testbool;

    public GameObject player;
    public List<Skill> attacklist_notset;
    public List<SkillReady> attacklist;
    public bool canattack = true;
    public bool isdelay = true;
    public int arrayindex = 0;
    public int attacknumber = 0;
    public int skillnameint = 0;
    public float angle;
    public GameObject currentskill;
    public GameObject currentskill2;
    public List<Skill> frontlinkenforceskills;
    public int normalskillindex = 0;
    public Skill amalgamskill;

    private void Start()
    {
        attacklist = new();
    }

    //스킬을 묶어서 리스트에 넣었으면, 묶었던 스킬들은 삭제해야 리스트가 밀리지않음
    //주의사항, 완성시 함수는 리스트에 들어있는 일반스킬의 갯수만큼 실행해야함;
    //전방연계는 안만들것같음, 생각해보니까 쓸모가 없음
    public void Array()
    {
        if (attacklist_notset[arrayindex].normalskill && attacklist_notset.Count != 1) //리스트의 첫번째 요소가 일반스킬인지 확인
        {
            if (!attacklist_notset[arrayindex + 1].amalagam) //일반스킬이라면, 그 앞에 융합이 존재하는지 확인
            {
                string skillname = $"{attacklist_notset[arrayindex].skillmarkname}{skillnameint}";
                
                SkillReady skillready = new SkillReady(skillname, null, attacklist_notset[arrayindex], null);
                attacklist.Add(skillready);
                
                attacklist_notset.RemoveAt(arrayindex);
                skillnameint += 1;
                return;
            }

            else if (attacklist_notset[arrayindex + 1].amalagam) //일반스킬이고, 그 앞에 융합이 존재
            {
                string skillname = $"{attacklist_notset[arrayindex].skillmarkname}{skillnameint}";

                SkillReady skillready = new SkillReady(skillname, null, attacklist_notset[arrayindex], attacklist_notset[arrayindex + 2]);
                attacklist.Add(skillready);

                attacklist_notset.RemoveAt(arrayindex + 2);
                attacklist_notset.RemoveAt(arrayindex + 1);
                attacklist_notset.RemoveAt(arrayindex);
                skillnameint += 1;
                return;
            }

        }

        else if (attacklist_notset[arrayindex].enforceskill) //만약 일반스킬이 아니라면 강화 스킬인지 확인해야함
        {
            if (attacklist_notset[arrayindex].backlink) //강화 스킬이라면 후방연계 스킬인지 확인해야함
            {
                if (attacklist_notset[arrayindex + 1].normalskill) //후방연계 스킬인데 바로 뒤에 일반스킬이 있는경우
                {
                    if (!attacklist_notset[arrayindex + 2].amalagam) //후방연계 스킬인데 바로 뒤에 일반스킬이 있지만 그 뒤에 융합이 없는 경우
                    {
                        string skillname = $"{attacklist_notset[arrayindex + 1].skillmarkname}{skillnameint}";

                        frontlinkenforceskills.Add(attacklist_notset[arrayindex]);
                        SkillReady skillready = new SkillReady(skillname, frontlinkenforceskills, attacklist_notset[arrayindex + 1], null);
                        attacklist.Add(skillready);
                        attacklist_notset.RemoveAt(arrayindex + 1);
                        attacklist_notset.RemoveAt(arrayindex);
                        frontlinkenforceskills.Clear();
                        skillnameint += 1;
                        return;
                    }    

                    else if (attacklist_notset[arrayindex + 2].amalagam)
                    {
                        string skillname = $"{attacklist_notset[arrayindex + 1].skillmarkname}{skillnameint}";

                        frontlinkenforceskills.Add(attacklist_notset[arrayindex]);
                        SkillReady skillready = new SkillReady(skillname, frontlinkenforceskills, attacklist_notset[arrayindex + 1], attacklist_notset[arrayindex + 3]);
                        attacklist.Add(skillready);
                        attacklist_notset.RemoveAt(arrayindex + 3);
                        attacklist_notset.RemoveAt(arrayindex + 2);
                        attacklist_notset.RemoveAt(arrayindex + 1);
                        attacklist_notset.RemoveAt(arrayindex);
                        frontlinkenforceskills.Clear();
                        skillnameint += 1;
                        return;
                    }
                }

                else if (attacklist_notset[arrayindex + 1].backlink) //후방연계 스킬인데 바로 뒤가 일반스킬이 아니라 후방연계스킬인경우
                {
                    for (int i = 0; !attacklist_notset[i].normalskill; i++) //리스트를 순차적으로 흝으며 일반스킬이 나올때까지 후방연계스킬들을 묶음
                    {
                        frontlinkenforceskills.Add(attacklist_notset[i]);
                        normalskillindex = i + 1;
                    }

                    if (attacklist_notset[normalskillindex + 1].amalagam)
                    {
                        amalgamskill = attacklist_notset[normalskillindex + 2];
                    }
                    else
                    {
                        amalgamskill = null;
                    }

                    string skillname = $"{attacklist_notset[arrayindex].skillmarkname}{skillnameint}";

                    SkillReady skillready = new SkillReady(skillname, frontlinkenforceskills, attacklist_notset[normalskillindex], amalgamskill);
                    attacklist.Add(skillready);

                    if (amalgamskill = null)
                    {
                        for (int i = normalskillindex; i >= 0; i--)
                        {
                            attacklist_notset.RemoveAt(i);
                        }
                    }
                    else
                    {
                        for (int i = normalskillindex + 2; i >= 0; i--)
                        {
                            attacklist_notset.RemoveAt(i);
                        }
                    }
                    
                    frontlinkenforceskills.Clear();
                    amalgamskill = null;
                    normalskillindex = 0;
                    skillnameint += 1;
                    return;
                }
                return;
            }
            return;
        }

        else if (attacklist_notset[arrayindex].normalskill && attacklist_notset.Count == 1) //일반스킬 혼자 남았을때
        {
            string skillname = $"{attacklist_notset[arrayindex].skillmarkname}{skillnameint}";

            SkillReady skillready = new SkillReady(skillname, null, attacklist_notset[arrayindex], null);
            attacklist.Add(skillready);
            attacklist_notset.RemoveAt(arrayindex);
            return;
        }
    }


    public void Update()
    {
        if (testbool)
        {
            testbool = false;
            Array();
        }


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (canattack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (attacklist[attacknumber].Normalskill)
                {
                    if (attacklist[attacknumber].Amalgamed == null)
                    {
                        currentskill = Instantiate(attacklist[attacknumber].Normalskill.skillprefab[0], transform.position, transform.rotation);
                        currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;

                        if (attacklist[attacknumber].Enforceskills != null)
                        {
                            if (attacklist[attacknumber].Normalskill.repeat)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.speed)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.force)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.bout)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.wide)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.mental)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.weight)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.heat)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.reversal)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.space)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.vibration)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.crack)
                            {

                            }
                            if (attacklist[attacknumber].Normalskill.explosion)
                            {

                            }
                        }

                        attacknumber = attacknumber + 1;
                    }
                    else if (attacklist[attacknumber].Amalgamed != null)
                    {
                        currentskill = Instantiate(attacklist[attacknumber].Normalskill.skillprefab[0], transform.position, transform.rotation);
                        currentskill2 = Instantiate(attacklist[attacknumber].Amalgamed.skillprefab[0], transform.position, transform.rotation);
                        currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                        currentskill2.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                        attacknumber = attacknumber + 1;
                    }
                }
                
                if (attacknumber == attacklist.Count)
                {
                    attacknumber = 0;
                }
            }

        }
    }

    
}
