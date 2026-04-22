using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class skillselectUi : MonoBehaviour
{
    public bool isselect = false;

    public GameObject attackcore;
    public Skill skill;
    public GameObject skilllist;
    public GameObject skillbutton;
    public GameObject descriptionUi;
    public RectTransform rect;
    public bool isHover;

    void Update()
    {
        bool nowHover = RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition);

        if (nowHover && !isHover)
        {
            isHover = true; 
            ScaleUp();
        }
        else if (!nowHover && isHover)
        {
            isHover = false;
            ScaleDown();
        }
    }

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }


    public void ScaleUp()
    {
        rect.DOKill();
        GetComponent<RectTransform>().DOScale(1.3f, 0.1f).SetId("UIScale1").SetUpdate(true);
    }

    public void ScaleDown()
    {
        rect.DOKill();
        GetComponent<RectTransform>().DOScale(1f, 0.4f).SetId("UIScale1").SetUpdate(true);
    }

    public void SkillAddToList()
    {
        if (isselect == false)
        {
            Selected(); 
        }
    }

    public void Start()
    {
        attackcore = battalemanager.Instance.attackcore;
    }

    public void Selected()
    {
        soundmanager.instance.SoundPlay("click2");
        attackcore = battalemanager.Instance.attackcore;

        isselect = true;

        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0.2f);

        if (descriptionUi != null)
        {
            descriptionUi.GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0.1f);
        }

        attackcore.GetComponent<attackcore>().attacklist_original.Add(skill);
        GameObject curskillbutton = Instantiate(skillbutton, skilllist.transform);
        skilllist.GetComponent<skillselectarrey>().original_skilllist.Add(skill);
        curskillbutton.GetComponent<skillbuttondisappear>().currentskill = skill;
        curskillbutton.GetComponent<skillbuttondisappear>().attackcore = attackcore;
        attackcore.GetComponent<attackcore>().SkillarreyUi();

        if (skill.normalskill)
        {
            curskillbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = $"[{skill.skillmarkname}]->";
        }
        else if (skill.enforceskill)
        {
            if (skill.amalagam)
            {
                curskillbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = $"[[     ]{skill.skillmarkname}[     ]]->";
            }
            else
            {
                curskillbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = $"[{skill.skillmarkname}[     ]]->";
            }

        }


        curskillbutton.GetComponent<skillbuttondisappear>().button = gameObject;
        curskillbutton.GetComponent<skillbuttonsize>().ButtonSize();
    }

    public void UnSelected()
    {
        isselect = false;

        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0.7f);

        if (descriptionUi != null)
        {
            descriptionUi.GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0.4f);
        }
    }
}
