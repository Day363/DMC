using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;


public class Magazine
{
    public Weapon Weapon;
    public int Remainmagazine;
    public int Remaincycle;

    public Magazine(Weapon weapon)
    {
        Weapon = weapon;
        Remainmagazine = Mathf.Clamp(weapon.magazine, 0, weapon.magazine);
        Remaincycle = Mathf.Clamp(weapon.magazinecycle, 0, weapon.magazinecycle);
    }

    public void IfShoot()
    {
        Remainmagazine = Mathf.Clamp(Remainmagazine - 1, 0, Weapon.magazine);
    }

    public void Reload()
    {
        if (Remaincycle > 0)
        {
            Remaincycle--;
            Remainmagazine = Weapon.magazine;
        }
    }
}

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
    public GameObject cammanager;
    public GameObject skillselectui;
    public GameObject viewpoint;
    public GameObject skillarreyUi;
    public TMP_Text passivetext;
    public TMP_Text normalskilltext;
    public TMP_Text arreyskilltext;
    public GameObject skillsetlist;
    public GameObject skilllistUi;
    public GameObject weaponimageUi;
    public GameObject weaponlistUi;
    public GameObject world_light;
    public GameObject dashmanager;
    public GameObject focusslider1;
    public GameObject focusslider2;
    public GameObject cursor1;
    public GameObject cursor2;
    public TMP_Text cursorskill;
    public TMP_Text cursordefense;


    public GameObject letterbox;

    public GameObject gamemanager;

    public SkillQueUi skillQueueUI;

    public GameObject defenseskilltest;
    public GameObject weaponimage;
    public GameObject standbyskillslider;
    public GameObject waitskillslider;
    public GameObject waitskillprefap;
    public GameObject skillwaittext;
    public GameObject cycletext;
    public GameObject circumtext;
    public GameObject magazinetext;
    public GameObject player;
    public List<Weapon> weaponlist;
    public List<Skill> attacklist_original;
    public List<string> skillstring;
    public List<Skill> attacklist_notset;
    public List<SkillReady> attacklist = new List<SkillReady> { };
    public bool canattack = true;
    public bool isdelay = true;
    public bool dash = false;
    public bool focusing = false;
    public float currentfocus;
    public Coroutine dashcoroutine;
    public int arrayindex = 0;
    public int listnumber = 0;
    public int cycle = 1;
    public int circum = 0;
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

    public bool iscycle = false;

    public string amalgamedanimationtrigger;

    public List<Magazine> weaponsmagazine = new List<Magazine>();

    public float worldlightintensity;

    private void Start()
    {
        attacklist = new();
    }

    public void BattleStart()
    {
        skillselectui.SetActive(true);
        Time.timeScale = 0f;
        MakeRangeWeaponMagzineList();
    }

    public void BossDamaged()
    {
        player.GetComponent<skillfunction>().Trapal_Penetrate_When_attacked();
        player.GetComponent<Passivefunction>().HitEnemy();
    }

    public void MakeRangeWeaponMagzineList()
    {
        weaponsmagazine.Clear();

        foreach (Weapon weapon in weaponlist)
        {
            if (weapon.range)
            {
                weaponsmagazine.Add(new Magazine(weapon));
            }
        }
    }


    public void StartCircum()//주기시작
    {
        attacknumber = 0;
        listnumber = 0;

        circum++;
        cycle = 0;
        CircumReplace();
        player.GetComponent<Passivefunction>().WhenCircumStart();
        
        foreach (Magazine magazine in weaponsmagazine)
        {
            magazine.Reload();
        }
    }

    public void StartCycle()
    {
        player.GetComponent<Passivefunction>().WhenCycleStart();
        Focuslength();
    }

    public void EndCycle()
    {
        player.GetComponent<playerstatus>().RemoveStackWhenCycleEnd();
    }

    public void WeaponListUI()
    {
        foreach (Weapon weapon in weaponlist)
        {
            GameObject currentweaponimage = Instantiate(weaponimageUi, weaponlistUi.transform);
            weaponskillUi weaponskillUi_Script = currentweaponimage.GetComponent<weaponskillUi>();
            weaponskillUi_Script.viewpoint = viewpoint;
            weaponskillUi_Script.passivetext = passivetext;
            weaponskillUi_Script.normalskilltext = normalskilltext;
            weaponskillUi_Script.arreyskilltext = arreyskilltext;
            weaponskillUi_Script.attackcore = gameObject;
            weaponskillUi_Script.weapon = weapon;
            weaponskillUi_Script.skilllistUi = skilllistUi;
            weaponskillUi_Script.skillsetlist = skillsetlist;
            currentweaponimage.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = weapon.weaponimage;
            currentweaponimage.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = weapon.weaponname;

            if (weapon.penetrate == true)
            {
                currentweaponimage.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (weapon.penetrate == false)
            {
                currentweaponimage.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            }

            if (weapon.slash == true)
            {
                currentweaponimage.transform.GetChild(0).GetChild(3).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (weapon.slash == false)
            {
                currentweaponimage.transform.GetChild(0).GetChild(3).GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            }

            if (weapon.blow == true)
            {
                currentweaponimage.transform.GetChild(0).GetChild(4).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (weapon.blow == false)
            {
                currentweaponimage.transform.GetChild(0).GetChild(4).GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            }
        }
    }

    public void LetterBoxDown()
    {
        letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
    }

    public void LetterBoxUp()
    {
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
    }

    public void UseStandbySkill()
    {
        if (standbyskills.Count > 0)
        {
            LetterBoxDown();
            curstandbyskill = standbyskills[0];
            canattack = false;
            player.GetComponent<PlayerMove>().canmove = false;

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
        else
        {
            
        }
    }

    public void EndStandbySkill()
    {
        gamemanager.GetComponent<battalemanager>().currentenemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        gamemanager.GetComponent<battalemanager>().currentenemy.GetComponent<Animator>().SetBool("idle", true);
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
        player.GetComponent<PlayerMove>().canmove = true;
        skillselectui.SetActive(true);
        Time.timeScale = 0f;
        canattack = false;
    }

    public void NostandByskill()
    {
        gamemanager.GetComponent<battalemanager>().currentenemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<PlayerMove>().canmove = true;
        skillselectui.SetActive(true);
        Time.timeScale = 0f;
        canattack = false;
    }

    IEnumerator UseStandby()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);
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

    public void StandBySkillPassiveActive()
    {
        List<string> passivestrings = new List<string> { };
        foreach (standbyskill standbyskill in standbyskills)
        {
            passivestrings.Add(standbyskill.passive);
        }
        foreach (Weapon weapon in weaponlist)
        {
            foreach (string passivestring in weapon.passivelist)
            {
                passivestrings.Add(passivestring);
            }  
        }
        player.GetComponent<Passivefunction>().SetBoolsFromList(passivestrings);
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

    public void WaitskillarrayUi()
    {
        for (int i = skillarreyUi.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = skillarreyUi.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        foreach (List<SkillReady> skillReadies in lastlist)
        {
            foreach (SkillReady skillReady in skillReadies)
            {
                GameObject waittext = Instantiate(waitskillprefap, skillarreyUi.transform);
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

    public void AttcknumberPlus()
    {
        
        attacknumber = attacknumber + 1;
      
    }

    public void WeaponimageReplace()
    {
        if (lastlist[listnumber][attacknumber].Normalskill.currentweapon.weaponimage != null)
        weaponimage.GetComponent<Image>().sprite = lastlist[listnumber][attacknumber].Normalskill.currentweapon.weaponimage;


    }

    public void DefenseTextReplace()
    {
        defenseskilltest.GetComponent<TMP_Text>().text = $"[{lastlist[listnumber][attacknumber].Normalskill.currentweapon.defenseskill.skillmarkname}]";
        cursordefense.text = defenseskilltest.GetComponent<TMP_Text>().text;
    }

    public void MagazineTextReplace()
    {
        if (lastlist[listnumber][attacknumber].Normalskill.currentweapon.range)
        {
            Magazine curmagazine = weaponsmagazine.Find(x => x.Weapon == lastlist[listnumber][attacknumber].Normalskill.currentweapon);
            magazinetext.GetComponent<TMP_Text>().text = $"[{curmagazine.Remaincycle}/{curmagazine.Remainmagazine}]";
        }
        else
        {
            magazinetext.GetComponent<TMP_Text>().text = "[∞]";
        }
        
    }
    
    public void TextReplace()
    {
        TMP_Text skillwaittext_tMP_Text = skillwaittext.GetComponent<TMP_Text>();
        if (lastlist[listnumber][attacknumber].Enforceskills == null && lastlist[listnumber][attacknumber].Amalgamed == null)
        {
            skillwaittext_tMP_Text.text = $"[{lastlist[listnumber][attacknumber].Normalskill.skillmarkname}]";
        }
        else if (lastlist[listnumber][attacknumber].Amalgamed == null && lastlist[listnumber][attacknumber].Enforceskills != null)
        {
            skillwaittext_tMP_Text.text = "";
            foreach (Skill enforceskill in lastlist[listnumber][attacknumber].Enforceskills)
            {
                skillwaittext_tMP_Text.text += $"[{enforceskill.skillmarkname}";
            }
            skillwaittext_tMP_Text.text += $"[{lastlist[listnumber][attacknumber].Normalskill.skillmarkname}]";
            foreach (Skill enforceskill in lastlist[listnumber][attacknumber].Enforceskills)
            {
                skillwaittext_tMP_Text.text += "]";
            }
        }
        else if (lastlist[listnumber][attacknumber].Amalgamed != null && lastlist[listnumber][attacknumber].Enforceskills == null)
        {
            skillwaittext_tMP_Text.text = $"[[{lastlist[listnumber][attacknumber].Normalskill.skillmarkname}]-[특수-융합]-[{lastlist[listnumber][attacknumber].Amalgamed.skillmarkname}]]";
        }
        else if (lastlist[listnumber][attacknumber].Amalgamed != null && lastlist[listnumber][attacknumber].Enforceskills != null)
        {
            skillwaittext_tMP_Text.text = "";
            foreach (Skill enforceskill in lastlist[listnumber][attacknumber].Enforceskills)
            {
                skillwaittext_tMP_Text.text += $"[{enforceskill.skillmarkname}";
            }
            skillwaittext_tMP_Text.text += $"[[{lastlist[listnumber][attacknumber].Normalskill.skillmarkname}]-[특수-융합]-[{lastlist[listnumber][attacknumber].Amalgamed.skillmarkname}]]";
            foreach (Skill enforceskill in lastlist[listnumber][attacknumber].Enforceskills)
            {
                skillwaittext_tMP_Text.text += "]";
            }
        }
        cursorskill.text = skillwaittext_tMP_Text.text;
    }

    public void Focuslength()
    {
        player.GetComponent<playerstatus>().focus = attacklist_original.Count;
        player.GetComponent<playerstatus>().focusbar.maxValue = player.GetComponent<playerstatus>().focus;
        player.GetComponent<playerstatus>().focusbar.value = player.GetComponent<playerstatus>().focus;
        currentfocus = player.GetComponent<playerstatus>().focus;

    }

    public void FocusReload()
    {
        player.GetComponent<playerstatus>().focusbar.value = player.GetComponent<playerstatus>().focus;
        currentfocus = player.GetComponent<playerstatus>().focus;
    }

    public void CycleReplace()
    {
        cycletext.GetComponent<TMP_Text>().text = $"{cycle}순환";
    }

    public void CircumReplace()
    {
        circumtext.GetComponent<TMP_Text>().text = $"{circum}주기";
    }

    public void SkillarreyUi()
    {
        Organize();
        //StandBySkillFind();
        Array2();
        WaitskillarrayUi();
    }

    public void ArreyComplete()
    {
        canattack = true;


        Focuslength();
        Organize();
        StandBySkillFind();
        StandBySkillPassiveActive();
        Array2();

        WeaponimageReplace();
        DefenseTextReplace();
        TextReplace();
        CycleReplace();
        Waitskillarray();
        skillQueueUI.InitializeSkillList(lastlist);
    }

    public void Update()
    {
        if (testbool)
        {
            testbool = false;
            canattack = false;
            WeaponListUI();
        }

        
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (canattack)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (attacknumber < lastlist[listnumber].Count)
                {
                    // UI 갱신
                    skillQueueUI.UseNextSkill();
                }

                if (lastlist[listnumber][attacknumber].Normalskill.currentweapon.defenseskill.counter)
                {
                    player.GetComponent<playerhit>().counteranimationtrigger = lastlist[listnumber][attacknumber].Normalskill.currentweapon.defenseskill.countertrigger;
                }

                if (lastlist[listnumber][attacknumber].Normalskill.currentweapon.defenseskill.animationskill)
                {
                    player.GetComponent<Animator>().SetTrigger(lastlist[listnumber][attacknumber].Normalskill.currentweapon.defenseskill.animationtrigger);
                }
                if (lastlist[listnumber][attacknumber].Normalskill.currentweapon.defenseskill.functionskill)
                {
                    player.GetComponent<skillfunction>().ExecuteCommand(lastlist[listnumber][attacknumber].Normalskill.currentweapon.defenseskill.function);
                }
                
                AttcknumberPlus();

                if (attacknumber == lastlist[listnumber].Count)
                {
                    EndCycle();
                    listnumber++;
                    attacknumber = 0;
                    cycle++;
                    //iscycle = true;
                    CycleReplace();
                    StartCycle();
                }

                if (listnumber == lastlist.Count)
                {
                    
                    listnumber = 0;
                    attacknumber = 0;
                    skillQueueUI.InitializeSkillList(lastlist);
                }

                WeaponimageReplace();
                DefenseTextReplace();
                TextReplace();
                MagazineTextReplace();
            }

            if (Input.GetMouseButtonDown(0) && !dashmanager.activeSelf)
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
                        if (lastlist[listnumber][attacknumber].Normalskill.functionskill)
                        {
                            player.GetComponent<skillfunction>().ExecuteCommand(lastlist[listnumber][attacknumber].Normalskill.funtionname);
                        }

                        if (lastlist[listnumber][attacknumber].Normalskill.animationskill != true)
                        {
                            if (lastlist[listnumber][attacknumber].Normalskill.prefabspawntoenemy == false)
                            {
                                if (lastlist[listnumber][attacknumber].Normalskill.prefabskill)
                                {
                                    currentskill = Instantiate(lastlist[listnumber][attacknumber].Normalskill.skillprefab[0], transform.position, transform.rotation);
                                    currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;

                                    if (currentskill.TryGetComponent<player_gunprefap>(out player_gunprefap pg))
                                    {
                                        pg.weapon = lastlist[listnumber][attacknumber].Normalskill.currentweapon;
                                        pg.attackcore = gameObject;
                                        pg.player = player;
                                    }
                                }
                                
       
                            }
                            else if (lastlist[listnumber][attacknumber].Normalskill.prefabspawntoenemy)
                            {
                                float randomint = Random.Range(0, 361);
                                currentskill = Instantiate(lastlist[listnumber][attacknumber].Normalskill.skillprefab[0], gamemanager.GetComponent<battalemanager>().currentenemy.transform.position, Quaternion.Euler(0, 0, randomint));
                                currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                                if (currentskill.TryGetComponent<player_gunprefap>(out player_gunprefap pg))
                                {
                                    pg.weapon = lastlist[listnumber][attacknumber].Normalskill.currentweapon;
                                    pg.attackcore = gameObject;
                                    pg.player = player;
                                }
                            }
                            

                            if (lastlist[listnumber][attacknumber].Enforceskills != null)
                            {
                                if (lastlist[listnumber][attacknumber].Normalskill.repeat)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.speed)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.force)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.bout)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.wide)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.mental)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.weight)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.heat)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.reversal)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.space)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.vibration)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.crack)
                                {

                                }
                                if (lastlist[listnumber][attacknumber].Normalskill.explosion)
                                {

                                }
                            }

                        }
                        if (lastlist[listnumber][attacknumber].Normalskill.animationskill)
                        {
                            player.GetComponent<Animator>().SetTrigger(lastlist[listnumber][attacknumber].Normalskill.animationtrigger);
                        }
                    }

                    else if (lastlist[listnumber][attacknumber].Amalgamed != null)
                    {
                        if (!lastlist[listnumber][attacknumber].Normalskill.animationskill && !lastlist[listnumber][attacknumber].Amalgamed.animationskill)
                        {
                            currentskill = Instantiate(lastlist[listnumber][attacknumber].Normalskill.skillprefab[0], transform.position, transform.rotation);
                            currentskill2 = Instantiate(lastlist[listnumber][attacknumber].Amalgamed.skillprefab[0], transform.position, transform.rotation);
                            if (currentskill.TryGetComponent<player_gunprefap>(out player_gunprefap pg))
                            {
                                pg.weapon = lastlist[listnumber][attacknumber].Normalskill.currentweapon;
                                pg.attackcore = gameObject;
                                pg.player = player;
                            }
                            if (currentskill2.TryGetComponent<player_gunprefap>(out player_gunprefap pg2))
                            {
                                pg2.weapon = lastlist[listnumber][attacknumber].Amalgamed.currentweapon;
                                pg2.attackcore = gameObject;
                                pg2.player = player;
                            }
                            currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                            currentskill2.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                        }
                        
                        else if (lastlist[listnumber][attacknumber].Normalskill.animationskill && !lastlist[listnumber][attacknumber].Amalgamed.animationskill)
                        {
                            player.GetComponent<Animator>().SetTrigger(lastlist[listnumber][attacknumber].Normalskill.animationtrigger);
                            currentskill2 = Instantiate(lastlist[listnumber][attacknumber].Amalgamed.skillprefab[0], transform.position, transform.rotation);
                            if (currentskill2.TryGetComponent<player_gunprefap>(out player_gunprefap pg2))
                            {
                                pg2.weapon = lastlist[listnumber][attacknumber].Amalgamed.currentweapon;
                                pg2.attackcore = gameObject;
                                pg2.player = player;
                            }
                            currentskill2.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                        }

                        else if (!lastlist[listnumber][attacknumber].Normalskill.animationskill && lastlist[listnumber][attacknumber].Amalgamed.animationskill)
                        {
                            player.GetComponent<Animator>().SetTrigger(lastlist[listnumber][attacknumber].Amalgamed.animationtrigger);
                            currentskill = Instantiate(lastlist[listnumber][attacknumber].Normalskill.skillprefab[0], transform.position, transform.rotation);
                            if (currentskill.TryGetComponent<player_gunprefap>(out player_gunprefap pg))
                            {
                                pg.weapon = lastlist[listnumber][attacknumber].Normalskill.currentweapon;
                                pg.attackcore = gameObject;
                                pg.player = player;
                            }
                            currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                        }

                        else if (lastlist[listnumber][attacknumber].Normalskill.animationskill && lastlist[listnumber][attacknumber].Amalgamed.animationskill)
                        {
                            player.GetComponent<Animator>().SetTrigger(lastlist[listnumber][attacknumber].Normalskill.animationtrigger);
                            amalgamedanimationtrigger = lastlist[listnumber][attacknumber].Amalgamed.animationtrigger;
                        }
                    }
                }

                

                AttcknumberPlus();

                if (attacknumber == lastlist[listnumber].Count)
                {
                    EndCycle();
                    listnumber++;
                    attacknumber = 0;
                    cycle++;
                    //iscycle = true;
                    CycleReplace();
                    StartCycle();
                }

                if (listnumber == lastlist.Count)
                {
                    Debug.Log("t");
                    listnumber = 0;
                    attacknumber = 0;
                    skillQueueUI.InitializeSkillList(lastlist);
                }

                WeaponimageReplace();
                DefenseTextReplace();
                TextReplace();
                MagazineTextReplace();



            }
            if (Input.GetMouseButtonDown(0) && dashmanager.activeSelf && dashmanager.GetComponent<dashline>().nowtargetting)
            {
                cursor1.SetActive(true);
                cursor2.SetActive(true);

                Color targetColor = new Color(255, 255, 255, 0);
                focusslider1.GetComponent<Image>().DOColor(targetColor, 1.5f).SetEase(Ease.OutQuart).SetId("focusbar");
                focusslider2.GetComponent<Image>().DOColor(targetColor, 1.5f).SetEase(Ease.OutQuart).SetId("focusbar");

                focusing = false;
                dashmanager.SetActive(false);
                player.GetComponent<PlayerMove>().canmove = true;
                player.GetComponent<Animator>().enabled = true;
                Time.timeScale = 1f;
                DOTween.Kill("light");
                world_light.GetComponent<Light2D>().intensity = worldlightintensity;

                Vector2 directionToEnemy = (gamemanager.GetComponent<battalemanager>().currentenemy.transform.position - player.transform.position).normalized;
                player.transform.position = gamemanager.GetComponent<battalemanager>().currentenemy.transform.position;
                Debug.Log(lastlist[listnumber][attacknumber].Normalskill.currentweapon.dashskill.dashafterpower);
                player.GetComponent<PlayerMove>().canmove = false;
                dash = true;
                StopCoroutine(dashcoroutine);
                DOTween.Kill("flash");
                player.GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", 0f);
                player.GetComponent<Rigidbody2D>().AddForce(directionToEnemy * lastlist[listnumber][attacknumber].Normalskill.currentweapon.dashskill.dashafterpower, ForceMode2D.Impulse);


                if (lastlist[listnumber][attacknumber].Normalskill.currentweapon.dashskill.animationskill)
                {
                    player.GetComponent<Animator>().SetTrigger(lastlist[listnumber][attacknumber].Normalskill.currentweapon.dashskill.animationtrigger);
                }
                if (lastlist[listnumber][attacknumber].Normalskill.currentweapon.dashskill.prefabskill)
                {
                    foreach (GameObject skill in lastlist[listnumber][attacknumber].Normalskill.currentweapon.dashskill.skillprefab)
                    {
                        Debug.Log("sds");
                        currentskill = Instantiate(skill, transform.position, transform.rotation);
                        currentskill.transform.GetChild(0).GetComponent<playerattackdamage>().player = player;
                    }
                    
                }

                AttcknumberPlus();

                if (attacknumber == lastlist[listnumber].Count)
                {
                    EndCycle();
                    listnumber++;
                    attacknumber = 0;
                    cycle++;
                    //iscycle = true;
                    CycleReplace();
                    StartCycle();
                }

                if (listnumber == lastlist.Count)
                {
                    Debug.Log("t");
                    listnumber = 0;
                    attacknumber = 0;
                    skillQueueUI.InitializeSkillList(lastlist);
                }

                WeaponimageReplace();
                DefenseTextReplace();
                TextReplace();
                MagazineTextReplace();
            }
            

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentfocus > 0)
        {
            if (canattack && player.GetComponent<PlayerMove>().canmove)
            {
                cammanager.GetComponent<CameraManager>().CamStable();

                if (player.transform.position.x - gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x > 0)
                {
                    player.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (player.transform.position.x - gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x < 0)
                {
                    player.transform.localScale = new Vector3(1, 1, 1);
                }
                cursor1.SetActive(false);
                cursor2.SetActive(false);

                DOTween.Kill("focusbar");
                focusslider1.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                focusslider2.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                focusing = true;
                dashmanager.SetActive(true);
                player.GetComponent<PlayerMove>().canmove = false;
                player.GetComponent<Animator>().enabled = false;
                player.GetComponent<SpriteRenderer>().sprite = lastlist[listnumber][attacknumber].Normalskill.currentweapon.dashskill.dashready;
                if (player.GetComponent<playerstatus>().currentparrystop != null)
                {
                    StopCoroutine(player.GetComponent<playerstatus>().currentparrystop);
                }
                if (player.GetComponent<playerhit>().currenthitstop != null)
                {
                    StopCoroutine(player.GetComponent<playerhit>().currenthitstop);
                }
                
                Time.timeScale = 0f;
                DOTween.Kill("light");
                worldlightintensity = world_light.GetComponent<Light2D>().intensity;
                DOTween.To(() => world_light.GetComponent<Light2D>().intensity, x => world_light.GetComponent<Light2D>().intensity = x, worldlightintensity - 0.5f, 1f).SetEase(Ease.OutQuart).SetId("light").SetUpdate(true);

                dashcoroutine = StartCoroutine(DashFlash());
            }  
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && currentfocus < 0)
        {
            //집중이 부족함 메세지
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (canattack && !player.GetComponent<PlayerMove>().canmove)
            {
                cursor1.SetActive(true);
                cursor2.SetActive(true);

                Color targetColor = new Color(255, 255, 255, 0);
                focusslider1.GetComponent<Image>().DOColor(targetColor, 1.5f).SetEase(Ease.OutQuart).SetId("focusbar");
                focusslider2.GetComponent<Image>().DOColor(targetColor, 1.5f).SetEase(Ease.OutQuart).SetId("focusbar");

                focusing = false;
                dashmanager.SetActive(false);
                player.GetComponent<PlayerMove>().canmove = true;
                player.GetComponent<Animator>().enabled = true;
                Time.timeScale = 1f;
                DOTween.Kill("light");
                DOTween.To(() => world_light.GetComponent<Light2D>().intensity, x => world_light.GetComponent<Light2D>().intensity = x, worldlightintensity, 0.4f).SetEase(Ease.OutQuart).SetId("light").SetUpdate(true);

                StopCoroutine(dashcoroutine);
                DOTween.Kill("flash");
                player.GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", 0f);
            }
        }

        if (dash)
        {
            if (player.GetComponent<Rigidbody2D>().velocity.x > 1f)
            {
                if (player.GetComponent<Rigidbody2D>().velocity.x < 2f)
                {
                    player.GetComponent<PlayerMove>().canmove = true;
                    dash = false;
                }
            }
            else
            {
                if (player.GetComponent<Rigidbody2D>().velocity.x > -2f)
                {
                    player.GetComponent<PlayerMove>().canmove = true;
                    dash = false;
                }
            }
            
        }

        if (focusing)
        {
            currentfocus -= 2f * Time.unscaledDeltaTime;
            currentfocus = Mathf.Clamp(currentfocus, 0f, player.GetComponent<playerstatus>().focusbar.maxValue);
            player.GetComponent<playerstatus>().focusbar.value = Mathf.Lerp(player.GetComponent<playerstatus>().focusbar.value, currentfocus, Time.unscaledDeltaTime * 5f);
        }
    }

    IEnumerator DashFlash()
    {
        while (true)
        {
            DOTween.Kill("flash");
            player.GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", 1f);
            DOTween.To(() => player.GetComponent<SpriteRenderer>().material.GetFloat("_flashamount"), value => player.GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", value), 0f, 1.5f).SetEase(Ease.OutQuart).SetUpdate(true).SetId("flash");
            yield return new WaitForSecondsRealtime(1.5f);
        }
        
    }

    public void AmalgamedAnimation()
    {
        if (amalgamedanimationtrigger != null)
        {
            player.GetComponent<Animator>().SetTrigger(amalgamedanimationtrigger);
            amalgamedanimationtrigger = null;
        }
    }

    
}
