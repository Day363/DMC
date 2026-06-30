using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boss_stackUIManager : MonoBehaviour
{
    public GameObject stackUIPrefab; // StackUI 프리팹
    public Transform stackContainer; // UI 표시할 부모 오브젝트
    public GameObject self;

    private List<GameObject> currentStackUIObjects = new List<GameObject>();

    // 스택 UI 갱신 함수
    public void RefreshUI(boss_hpbar bh)
    {
        // 기존 UI 삭제
        foreach (var obj in currentStackUIObjects)
        {
            Destroy(obj);
        }
        currentStackUIObjects.Clear();


        

        // 플레이어 스택 정보 받아와서 UI 생성
        foreach (var stackInstance in bh.activeStacks)
        {
            if (stackInstance == null || stackInstance.stackData == null)
                continue;

            GameObject go = Instantiate(stackUIPrefab, stackContainer);

            if (go == null)
                continue;

            currentStackUIObjects.Add(go);

            Image iconImage = go.GetComponentInChildren<Image>();
            TextMeshProUGUI countText = go.GetComponentInChildren<TextMeshProUGUI>();

            if (iconImage != null)
                iconImage.sprite = stackInstance.stackData.icon;

            if (countText != null)
                countText.text = stackInstance.currentStack.ToString();

            if (stackInstance.stackData.animation != null)
            {
                ReplaceClip(go, stackInstance.stackData.animation);
            }
            else
            {
                Animator anim = go.GetComponentInChildren<Animator>();

                if (anim != null)
                    anim.enabled = false;
            }

            if (go.transform.childCount > 0 &&
                go.transform.GetChild(0).TryGetComponent<stacktooltip>(out stacktooltip stt))
            {
                stt.stackdata = stackInstance.stackData;

                if (go.transform.childCount > 1)
                {
                    stencilequelwithparent stencil =
                        go.transform.GetChild(1).GetComponent<stencilequelwithparent>();

                    if (stencil != null)
                        stencil.SetMaterial();
                }

                Animator anim = go.transform.GetChild(0).GetComponent<Animator>();

                if (anim != null)
                    anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            }
        }

        foreach (var stackInstance in bh.nextcycleStacks)
        {
            GameObject go = Instantiate(stackUIPrefab, stackContainer);
            currentStackUIObjects.Add(go);

            Image iconImage = go.GetComponentInChildren<Image>();
            TextMeshProUGUI countText = go.GetComponentInChildren<TextMeshProUGUI>();

            if (iconImage != null)
                iconImage.sprite = stackInstance.stackData.icon;

            if (countText != null)
                countText.text = stackInstance.currentStack.ToString();

            if (stackInstance.stackData.animation != null)
            {
                ReplaceClip(go, stackInstance.stackData.animation);
            }
            else
            {
                go.GetComponentInChildren<Animator>().enabled = false;
            }

            iconImage.color = new Color(1, 1, 1, 0.4f);

            if (go.transform.GetChild(0).TryGetComponent<stacktooltip>(out stacktooltip stt))
            {
                stt.stackdata = stackInstance.stackData;
                go.transform.GetChild(1).GetComponent<stencilequelwithparent>().SetMaterial();
                go.transform.GetChild(0).GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
            }
        }
    }

    void ReplaceClip(GameObject target, AnimationClip clip)
    {
        Animator animator = target.GetComponentInChildren<Animator>();
        var overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        overrideController.GetOverrides(overrides);

        // 첫 번째 클립만 교체 (간단 버전)
        overrides[0] = new KeyValuePair<AnimationClip, AnimationClip>(overrides[0].Key, clip);

        overrideController.ApplyOverrides(overrides);
        animator.runtimeAnimatorController = overrideController;
    }
}
