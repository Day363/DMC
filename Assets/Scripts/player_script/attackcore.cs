using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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

    public GameObject letterbox;

    public GameObject gamemanager;

    public SkillQueUi skillQueueUI;

    public GameObject standbyskillslider;
    public GameObject waitskillslider;
    public GameObject waitskillprefap;
    public GameObject skillwaittext;
    public GameObject cycletext;
    public GameObject player;
    public List<Weapon> weaponlist;
    public List<Skill> attacklist_original;
    public List<string> skillstring;
    public List<Skill> attacklist_notset;
    public List<SkillReady> attacklist = new List<SkillReady> { };
    public bool canattack = true;
    public bool isdelay = true;
    public int arrayindex = 0;
    public int listnumber = 0;
    public int cycle = 1;
    public int attacknumber = 0;
    public int skillnameint = 0;
    public float angle;
    public GameObject currentskill;
    public GameObject currentskill2;
    public List<Skill> frontlinkenforceskills;
    public int normalskillindex = 0;
    public Skill amalgamskill;
    public int cycleint;
    public List<cynthskill> avaliableskills = new List<cynthskill> { };
    public List<int> avaliablecycles = new List<int> { };
    public int LCM;
    public List<List<Skill>> attacklists = new List<List<Skill>> { };
    public List<string> skillstring2 = new List<string> { };
    public List<List<SkillReady>> lastlist = new List<List<SkillReady>> { };
    public List<Skill> skillsets = new List<Skill> { };

    public List<standbyskill> standbyskills = new List<standbyskill> { };
    public List<string> skillstring_1 = new List<string> { };
    public standbyskill curstandbyskill;

    private void Start()
    {
        attacklist = new();
        
    }

    public void LetterBoxDown()
    {
        letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
    }

    public void UseStandbySkill()
    {
        LetterBoxDown();

        canattack = false;
        curstandbyskill = standbyskills[0];

        if (player.transform.position.x < gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x)
        {
            player.transform.localScale = new Vector3(1, 1, 1);
            player.GetComponent<PlayerMove>().dir = 1;

            player.transform.position = new Vector3(gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x - curstandbyskill.length, player.transform.position.y, 0);

            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(UseStandby());
        }
        if (player.transform.position.x > gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x)
        {
            player.transform.localScale = new Vector3(-1, 1, 1);
            player.GetComponent<PlayerMove>().dir = -1;

            player.transform.position = new Vector3(gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x + curstandbyskill.length, player.transform.position.y, 0);

            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(UseStandby());
        }
    }

    IEnumerator UseStandby()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        player.GetComponent<Animator>().SetTrigger(curstandbyskill.animationtrigger);
        standbyskills.RemoveAt(0);
    }

    public void Copy()
    {
        attacklist_notset = new List<Skill>(attacklist_original);
    }

    public void Array2() //아마도 고아임
    {
        
        lastlist.Clear();
        foreach (List<Skill> skilllists in attacklists)
        {
            int amalgam = 0;
            int normal = 0;
            foreach (Skill skill in skilllists)
            {
                if (skill.amalagam == true)
                {
                    amalgam++;
                }
                if (skill.normalskill == true)
                {
                    normal++;
                }
            }

            attacklist.Clear();
            skillsets = new List<Skill>(skilllists);
            for (int i = 0; i < normal - amalgam; i++)
            {
                Array(skillsets);
                Debug.Log(attacklist.Count);
            }

            lastlist.Add(new List<SkillReady>(attacklist));
        }
    }

    public void StandBySkillFind()
    {
        skillstring_1.Clear();
        foreach (Skill skill in attacklist_notset)
        {
            skillstring_1.Add(skill.skillmarkname);
        }

        foreach (Weapon weapon in weaponlist)
        {
            foreach (standbyskill standbyskill in weapon.standbyskilllist)
            {
                if (skillstring_1.Count < standbyskill.skillarreyto.Count)
                    break;

                for (int i = 0; i <= skillstring_1.Count - standbyskill.skillarreyto.Count; i++)
                {
                    bool match = true;

                    for (int j = 0; j < standbyskill.skillarreyto.Count; j++)
                    {
                        if (skillstring_1[i + j] != standbyskill.skillarreyto[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        standbyskills.Add(standbyskill);
                    }
                }
            }

            foreach (standbyskill standbyskill1 in standbyskills)
            {
                GameObject currenttext = Instantiate(waitskillprefap, standbyskillslider.transform);
                currenttext.GetComponent<TMP_Text>().text = $"[{standbyskill1.skillname}]";
            }
        }
    }

    public void Organize()
    {
        Copy();
        skillstring.Clear();
        avaliableskills.Clear();
        foreach (Skill skill in attacklist_notset)
        {
            skillstring.Add(skill.skillmarkname);
        }

        foreach (Weapon weapon in weaponlist)
        {
            foreach (cynthskill cynthskill in weapon.cynthskilllist)
            {
                if (skillstring.Count < cynthskill.condition.Count)
                    break;

                for (int i = 0; i <= skillstring.Count - cynthskill.condition.Count; i++)
                {
                    bool match = true;

                    for (int j = 0; j < cynthskill.condition.Count; j++)
                    {
                        if (skillstring[i + j] != cynthskill.condition[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        avaliableskills.Add(cynthskill);
                    }
                }
            }
        }

        avaliablecycles.Clear();
        if (avaliableskills.Count != 0)
        {
            foreach (cynthskill cynthskill_tocycle in avaliableskills)
            {
                avaliablecycles.Add(cynthskill_tocycle.cycle);
            }
        }
        
        if (avaliablecycles.Count > 1)
        {

            LCM = avaliablecycles[0];

            for (int i = 1; i < avaliablecycles.Count; i++)
            {
                int a = LCM;
                int b = avaliablecycles[1];

                while (b != 0)
                {
                    int temp = b;
                    b = a % b;
                    a = temp;
                }

                int gcd = a;

                LCM = LCM * avaliablecycles[i] / gcd;
            }
        }

        if (avaliablecycles.Count == 1)
        {
            LCM = avaliablecycles[0];
        }

        if (avaliablecycles.Count < 1)
        {
            LCM = 10;
        }

        attacklists.Clear();
        for (int i = 1; i <= LCM; i++)
        {
            attacklists.Add(new List<Skill>(attacklist_notset));
        }

        foreach (cynthskill cynthskill_to in avaliableskills)
        {
            for (int i = cynthskill_to.cycle - 1; i < LCM; i = i + cynthskill_to.cycle)
            {
                skillstring2.Clear();
                foreach (Skill skill in attacklists[i])
                {
                    skillstring2.Add(skill.skillmarkname);
                }

                for (int j = 0; j <= skillstring2.Count - cynthskill_to.condition.Count; j++)
                {
                    bool isMatch = true;

                    for (int l = 0; l < cynthskill_to.condition.Count; l++)
                    {
                        if (skillstring2[j + l] != cynthskill_to.condition[l])
                        {
                            isMatch = false;
                            break;
                        }
                    }

                    if (isMatch)
                    {
                        for (int v = 0; v < cynthskill_to.condition.Count - 1; v++)
                        {
                            skillstring2.RemoveAt(j);
                        }
                        skillstring2[j] = "x";


                        for (int v = 0; v < cynthskill_to.condition.Count - 1; v++)
                        {
                            attacklists[i].RemoveAt(j);
                        }
                        attacklists[i][j] = cynthskill_to.skill;  
                        break;
                    }
                }
            }
        }

        //foreach (List<Skill> skills in attacklists) //지극히정상적으로 작동함, 오류없음
        //{
        //    foreach (Skill skill in skills)
        //    {
        //        Debug.Log(skill.skillmarkname);
        //    }
        //}

    }

    //스킬을 묶어서 리스트에 넣었으면, 묶었던 스킬들은 삭제해야 리스트가 밀리지않음
    //주의사항, 완성시 함수는 리스트에 들어있는 일반스킬의 갯수만큼 실행해야함;
    //전방연계는 안만들것같음, 생각해보니까 쓸모가 없음
    public void Array(List<Skill> notset)
    {
        var attacklist_notset = new List<Skill>(notset);

        if (attacklist_notset[arrayindex].normalskill && attacklist_notset.Count != 1) //리스트의 첫번째 요소가 일반스킬인지 확인
        {
            if (!attacklist_notset[arrayindex + 1].amalagam) //일반스킬이라면, 그 앞에 융합이 존재하는지 확인
            {
                string skillname = $"{attacklist_notset[arrayindex].skillmarkname}{skillnameint}";
                
                SkillReady skillready = new SkillReady(skillname, null, attacklist_notset[arrayindex], null);
                attacklist.Add(skillready);
                
                attacklist_notset.RemoveAt(arrayindex);
                skillsets = attacklist_notset;
                skillnameint += 1;
            }

            else if (attacklist_notset[arrayindex + 1].amalagam) //일반스킬이고, 그 앞에 융합이 존재
            {
                string skillname = $"{attacklist_notset[arrayindex].skillmarkname}{skillnameint}";

                SkillReady skillready = new SkillReady(skillname, null, attacklist_notset[arrayindex], attacklist_notset[arrayindex + 2]);
                attacklist.Add(skillready);

                attacklist_notset.RemoveAt(arrayindex + 2);
                attacklist_notset.RemoveAt(arrayindex + 1);
                attacklist_notset.RemoveAt(arrayindex);
                skillsets = attacklist_notset;
                skillnameint += 1;
            }
        }

        else if (attacklist_notset[arrayindex].enforceskill) //만약 일반스킬이 아니라면 강화 스킬인지 확인해야함
        {
            if (attacklist_notset[arrayindex].backlink) //강화 스킬이라면 후방연계 스킬인지 확인해야함
            {
                if (attacklist_notset[arrayindex + 1].normalskill) //후방연계 스킬인데 바로 뒤에 일반스킬이 있는경우
                {
                    if (attacklist_notset.Count > arrayindex + 2)
                    {
                        if (!attacklist_notset[arrayindex + 2].amalagam) //후방연계 스킬인데 바로 뒤에 일반스킬이 있지만 그 뒤에 융합이 없는 경우
                        {
                            string skillname = $"{attacklist_notset[arrayindex + 1].skillmarkname}{skillnameint}";

                            frontlinkenforceskills.Add(attacklist_notset[arrayindex]);
                            List<Skill> newforces = new List<Skill>(frontlinkenforceskills);
                            SkillReady skillready = new SkillReady(skillname, newforces, attacklist_notset[arrayindex + 1], null);
                            attacklist.Add(skillready);
                            attacklist_notset.RemoveAt(arrayindex + 1);
                            attacklist_notset.RemoveAt(arrayindex);
                            skillsets = attacklist_notset;
                            frontlinkenforceskills.Clear();
                            skillnameint += 1;
                        }

                        else if (attacklist_notset[arrayindex + 2].amalagam)
                        {
                            string skillname = $"{attacklist_notset[arrayindex + 1].skillmarkname}{skillnameint}";

                            frontlinkenforceskills.Add(attacklist_notset[arrayindex]);
                            List<Skill> newforces = new List<Skill>(frontlinkenforceskills);
                            SkillReady skillready = new SkillReady(skillname, newforces, attacklist_notset[arrayindex + 1], attacklist_notset[arrayindex + 3]);
                            attacklist.Add(skillready);
                            attacklist_notset.RemoveAt(arrayindex + 3);
                            attacklist_notset.RemoveAt(arrayindex + 2);
                            attacklist_notset.RemoveAt(arrayindex + 1);
                            attacklist_notset.RemoveAt(arrayindex);
                            skillsets = attacklist_notset;
                            frontlinkenforceskills.Clear();
                            skillnameint += 1;
                        }
                    }

                    else if (attacklist_notset.Count < 3)
                    {
                        string skillname = $"{attacklist_notset[arrayindex + 1].skillmarkname}{skillnameint}";

                        frontlinkenforceskills.Add(attacklist_notset[arrayindex]);
                        List<Skill> newforces = new List<Skill>(frontlinkenforceskills);
                        SkillReady skillready = new SkillReady(skillname, newforces, attacklist_notset[arrayindex + 1], null);
                        attacklist.Add(skillready);
                        attacklist_notset.RemoveAt(arrayindex + 1);
                        attacklist_notset.RemoveAt(arrayindex);
                        skillsets = attacklist_notset;
                        frontlinkenforceskills.Clear();
                        skillnameint += 1;
                    }


                }
               

                else if (attacklist_notset[arrayindex + 1].backlink) //후방연계 스킬인데 바로 뒤가 일반스킬이 아니라 후방연계스킬인경우
                {
                    for (int i = 0; !attacklist_notset[i].normalskill; i++) //리스트를 순차적으로 흝으며 일반스킬이 나올때까지 후방연계스킬들을 묶음
                    {
                        frontlinkenforceskills.Add(attacklist_notset[i]);
                        normalskillindex = i + 1;
                    }

                    if (attacklist_notset.Count > normalskillindex + 1)
                    {
                        if (attacklist_notset[normalskillindex + 1].amalagam)
                        {
                            Debug.Log("y");
                            amalgamskill = attacklist_notset[normalskillindex + 2];
                        }
                        else
                        {
                            amalgamskill = null;
                        }
                    }
                    
                    

                    string skillname = $"{attacklist_notset[arrayindex].skillmarkname}{skillnameint}";

                    List<Skill> newforces = new List<Skill>(frontlinkenforceskills);
                    SkillReady skillready = new SkillReady(skillname, newforces, attacklist_notset[normalskillindex], amalgamskill);
                    attacklist.Add(skillready);

                    if (amalgamskill == null)
                    {
                        for (int i = normalskillindex; i >= 0; i--)
                        {
                            attacklist_notset.RemoveAt(i);
                        }
                    }
                    else
                    {
                        Debug.Log(amalgamskill.skillmarkname);
                        for (int i = normalskillindex + 2; i >= 0; i--)
                        {
                            
                            attacklist_notset.RemoveAt(i);
                        }
                    }
                    skillsets = attacklist_notset;

                    frontlinkenforceskills.Clear();
                    amalgamskill = null;
                    normalskillindex = 0;
                    skillnameint += 1;
                }
            }
        }


        else if (attacklist_notset[arrayindex].normalskill && attacklist_notset.Count == 1) //일반스킬 혼자 남았을때
        {
            string skillname = $"{attacklist_notset[arrayindex].skillmarkname}{skillnameint}";

            SkillReady skillready = new SkillReady(skillname, null, attacklist_notset[arrayindex], null);
            attacklist.Add(skillready);
            attacklist_notset.RemoveAt(arrayindex);
            skillsets = attacklist_notset;
        }
    }

    public void Waitskillarray()
    {
        foreach (List<SkillReady> skillReadies in lastlist)
        {
            foreach (SkillReady skillReady in skillReadies)
            {
                GameObject waittext = Instantiate(waitskillprefap, waitskillslider.transform);
                waittext.GetComponent<TMP_Text>().text = SkillWaitText(skillReady);
            }
        }
    }

    public string SkillWaitText(SkillReady skillReady)
    {
        string waittext;
        if (skillReady.Enforceskills == null && skillReady.Amalgamed == null)
        {
            waittext = $"[{skillReady.Normalskill.skillmarkname}]";
            return waittext;
        }
        else if (skillReady.Amalgamed == null && skillReady.Enforceskills != null)
        {
            waittext = "";
            foreach (Skill enforceskill in skillReady.Enforceskills)
            {
                waittext += $"[{enforceskill.skillmarkname}";
            }
            waittext += $"[{skillReady.Normalskill.skillmarkname}]";
            foreach (Skill enforceskill in skillReady.Enforceskills)
            {
                waittext += "]";
            }
            return waittext;
        }
        else if (skillReady.Amalgamed != null && skillReady.Enforceskills == null)
        {
            waittext = $"[[{skillReady.Normalskill.skillmarkname}]-[특수-융합]-[{skillReady.Amalgamed.skillmarkname}]]";
            return waittext;
        }
        else if (skillReady.Amalgamed != null && skillReady.Enforceskills != null)
        {
            waittext = "";
            foreach (Skill enforceskill in skillReady.Enforceskills)
            {
                waittext += $"[{enforceskill.skillmarkname}";
            }
            waittext += $"[[{skillReady.Normalskill.skillmarkname}]-[특수-융합]-[{skillReady.Amalgamed.skillmarkname}]]";
            foreach (Skill enforceskill in skillReady.Enforceskills)
            {
                waittext += "]";
            }
            return waittext;
        }
        else
        {
            return "오류";
        }

    }

    public void TextReplace()
    {
        if (lastlist[listnumber][attacknumber].Enforceskills == null && lastlist[listnumber][attacknumber].Amalgamed == null)
        {
            skillwaittext.GetComponent<TMP_Text>().text = $"[{lastlist[listnumber][attacknumber].Normalskill.skillmarkname}]";
        }
        else if (lastlist[listnumber][attacknumber].Amalgamed == null && lastlist[listnumber][attacknumber].Enforceskills != null)
        {
            skillwaittext.GetComponent<TMP_Text>().text = "";
            foreach (Skill enforceskill in lastlist[listnumber][attacknumber].Enforceskills)
            {
                skillwaittext.GetComponent<TMP_Text>().text += $"[{enforceskill.skillmarkname}";
            }
            skillwaittext.GetComponent<TMP_Text>().text += $"[{lastlist[listnumber][attacknumber].Normalskill.skillmarkname}]";
            foreach (Skill enforceskill in lastlist[listnumber][attacknumber].Enforceskills)
            {
                skillwaittext.GetComponent<TMP_Text>().text += "]";
            }
        }
        else if (lastlist[listnumber][attacknumber].Amalgamed != null && lastlist[listnumber][attacknumber].Enforceskills == null)
        {
            skillwaittext.GetComponent<TMP_Text>().text = $"[[{lastlist[listnumber][attacknumber].Normalskill.skillmarkname}]-[특수-융합]-[{lastlist[listnumber][attacknumber].Amalgamed.skillmarkname}]]";
        }
        else if (lastlist[listnumber][attacknumber].Amalgamed != null && lastlist[listnumber][attacknumber].Enforceskills != null)
        {
            skillwaittext.GetComponent<TMP_Text>().text = "";
            foreach (Skill enforceskill in lastlist[listnumber][attacknumber].Enforceskills)
            {
                skillwaittext.GetComponent<TMP_Text>().text += $"[{enforceskill.skillmarkname}";
            }
            skillwaittext.GetComponent<TMP_Text>().text += $"[[{lastlist[listnumber][attacknumber].Normalskill.skillmarkname}]-[특수-융합]-[{lastlist[listnumber][attacknumber].Amalgamed.skillmarkname}]]";
            foreach (Skill enforceskill in lastlist[listnumber][attacknumber].Enforceskills)
            {
                skillwaittext.GetComponent<TMP_Text>().text += "]";
            }
        }
    }

    public void CycleReplace()
    {
        cycletext.GetComponent<TMP_Text>().text = $"{cycle}순환";
    }

    public void Update()
    {
        if (testbool)
        {
            testbool = false;
            
            Organize();
            StandBySkillFind();
            Array2();

            TextReplace();
            CycleReplace();
            Waitskillarray();
            skillQueueUI.InitializeSkillList(lastlist);
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
                if (attacknumber < lastlist[listnumber].Count)
                {
                    // UI 갱신
                    skillQueueUI.UseNextSkill();
                }

                   
                if (lastlist[listnumber][attacknumber].Normalskill)
                {
                    if (lastlist[listnumber][attacknumber].Amalgamed == null)
                    {
                        if (lastlist[listnumber][attacknumber].Normalskill.animationskill != true)
                        {
                            currentskill = Instantiate(lastlist[listnumber][attacknumber].Normalskill.skillprefab[0], transform.position, transform.rotation);
                            currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;

                            if (lastlist[listnumber][attacknumber].Enforceskills != null)
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
                        }
                        else if (lastlist[listnumber][attacknumber].Normalskill.animationskill)
                        {
                            player.GetComponent<Animator>().SetTrigger("running");
                            player.GetComponent<Animator>().SetBool(lastlist[listnumber][attacknumber].Normalskill.animationtrigger, true);
                        }

                        attacknumber = attacknumber + 1;
                    }

                    else if (lastlist[listnumber][attacknumber].Amalgamed != null)
                    {
                        currentskill = Instantiate(lastlist[listnumber][attacknumber].Normalskill.skillprefab[0], transform.position, transform.rotation);
                        currentskill2 = Instantiate(lastlist[listnumber][attacknumber].Amalgamed.skillprefab[0], transform.position, transform.rotation);
                        currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                        currentskill2.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                        attacknumber = attacknumber + 1;
                    }
                }
                

                if (attacknumber == lastlist[listnumber].Count)
                {
                    listnumber++;
                    attacknumber = 0;
                    cycle++;
                    CycleReplace();
                }

                if (listnumber == lastlist.Count - 1)
                {
                    listnumber = 0;
                    skillQueueUI.InitializeSkillList(lastlist);
                }

                TextReplace();
            }

        }
    }

    
}
