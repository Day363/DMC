using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillQueUi : MonoBehaviour
{
    public GameObject skillTextPrefab; // TextMeshProUGUI 프리팹
    public Transform contentParent; // Content 오브젝트
    private Queue<GameObject> skillTextObjects = new Queue<GameObject>();

    public string SkillWaitText(SkillReady skillReady)
    {
        string waittext;
        if (skillReady.Enforceskills == null && skillReady.Amalgamed == null)
        {
            waittext = $"[{skillReady.Normalskill.skillmarkname}]";
            waittext += "->";
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
            waittext += "->";
            return waittext;
        }
        else if (skillReady.Amalgamed != null && skillReady.Enforceskills == null)
        {
            waittext = $"[[{skillReady.Normalskill.skillmarkname}]-[특수-융합]-[{skillReady.Amalgamed.skillmarkname}]]";
            waittext += "->";
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
            waittext += "->";
            return waittext;
        }
        else
        {
            return "오류";
        }

    }

    public void InitializeSkillList(List<List<SkillReady>> skillQueue)
    {
        // 기존 UI 제거
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        skillTextObjects.Clear();

        // 새로운 스킬 리스트 추가
        foreach (var skills in skillQueue)
        {
            foreach (var skill in skills)
            {
                AddSkillToUI(SkillWaitText(skill));
            }

        }
    }

    public void AddSkillToUI(string skillName)
    {
        GameObject skillTextGO = Instantiate(skillTextPrefab, contentParent);
        var textComponent = skillTextGO.GetComponent<TextMeshProUGUI>();
        textComponent.text = skillName;
        skillTextObjects.Enqueue(skillTextGO);
    }

    public void UseNextSkill()
    {
        if (skillTextObjects.Count > 0)
        {
            GameObject usedSkill = skillTextObjects.Dequeue();
            Destroy(usedSkill);
        }
    }
}
