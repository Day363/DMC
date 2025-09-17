using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using DG.Tweening;
using System;

public enum Chattarget {Player, Enemy}

[System.Serializable]
public class Dialogue
{
    public Chattarget target;

    [TextArea(5, 10)]
    public string dialogue;

    public float basicdelay = 0.1f;
    public float dotdelay = 0.5f;

    public string fuctionname;

    public bool end;

    public bool gotoanswer;
    public int answerindex;
}

[System.Serializable]
public class Answer
{
    public string answer;
    public int nextdialogueindex;
    public string fuction;
}

[System.Serializable]
public class Answers
{
    public List<Answer> answers;
}

[System.Serializable]
public class DialogueData
{
    public List<Dialogue> dialogueLines;
}

public class chatmanager : MonoBehaviour
{
    public Dictionary<string, Action> fuctionMap;

    public bool chating;
    public bool whilesaying;
    public bool skip;

    public GameObject cammanager;
    public GameObject player;
    public GameObject letterbox;
    public List<DialogueData> dialogues;
    public List<Answers> answers;
    public DialogueData currentdialogues;
    public GameObject playerchatbox;
    public GameObject enemychatbox;
    public int chatnumber;

    public GameObject beforechatbox;
    public Coroutine currentchatco;

    void Start()
    { 
        fuctionMap = new Dictionary<string, Action>
        {
            { "test", Test }
        };
    }

    public void Update()
    {
        if (chating)
        {
            if (Input.GetMouseButtonDown(0) && !whilesaying)
            {
                if (currentdialogues.dialogueLines[chatnumber].target == Chattarget.Enemy)
                {
                    if (currentchatco != null)
                    {
                        StopCoroutine(currentchatco);
                    }
                    
                    currentchatco = StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], enemychatbox));
                    chatnumber++;
                }
                else if (currentdialogues.dialogueLines[chatnumber].target == Chattarget.Player)
                {
                    if (currentchatco != null)
                    {
                        StopCoroutine(currentchatco);
                    }
                    currentchatco = StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], playerchatbox));
                    chatnumber++;
                }
                
            }
            else if (Input.GetMouseButtonDown(0) && whilesaying)
            {
                skip = true;
            }
        }

    }

    public void CallDialogue(int i)
    {
        currentdialogues = dialogues[i];
        chating = true;
        playerchatbox.SetActive(true);
        enemychatbox.SetActive(true);
        if (currentdialogues.dialogueLines[chatnumber].target == Chattarget.Enemy)
        {
            if (currentchatco != null)
            {
                StopCoroutine(currentchatco);
            }
            currentchatco = StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], enemychatbox));
            chatnumber++;
        }
        else if (currentdialogues.dialogueLines[chatnumber].target == Chattarget.Player)
        {
            if (currentchatco != null)
            {
                StopCoroutine(currentchatco);
            }
            currentchatco = StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], playerchatbox));
            chatnumber++;
        }
    }

    IEnumerator Chat(Dialogue chat, GameObject currentchatbox)
    {
        if (beforechatbox != null)
        {
            if (beforechatbox != currentchatbox)
            {
                beforechatbox.transform.DOScaleY(0, 0.7f);
            }
        }
        
        beforechatbox = currentchatbox;

        currentchatbox.transform.localScale = new Vector3(2.4f, 0, 2.4f);

        whilesaying = true;

        TMP_Text tmp = currentchatbox.transform.GetChild(0).GetComponent<TMP_Text>();
        TextEffectManager shaker = tmp.GetComponent<TextEffectManager>();
        if (shaker != null)
            shaker.SetText(chat.dialogue);
        else
            tmp.text = chat.dialogue;
        tmp.ForceMeshUpdate();
        int totalvisible = tmp.textInfo.characterCount;
        tmp.maxVisibleCharacters = 0;
        int visible = 0;

        currentchatbox.transform.DOScaleY(2.4f, 0.7f);
        yield return new WaitForSeconds(1f);

        while (visible < totalvisible)
        {
            if (skip)
            {
                tmp.maxVisibleCharacters = totalvisible; 
                skip = false;
                whilesaying = false;
                if (chat.end)
                {
                    yield return new WaitForSeconds(1f);
                    cammanager.GetComponent<CameraManager>().LookPlayer();
                    letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
                    player.GetComponent<PlayerMove>().canmove = true;
                    chatnumber = 0;
                    chating = false;
                    playerchatbox.SetActive(false);
                    enemychatbox.SetActive(false);
                }
                break;                         
            }

            visible++;
            tmp.maxVisibleCharacters = visible;
            Debug.Log(tmp.maxVisibleCharacters);

            if (shaker != null)
                shaker.CheckEvents(visible);

            char c = GetPrintedCharAtIndex(tmp, visible - 1);
            float wait = chat.basicdelay;
            if (IsPunctuation(c)) wait += chat.dotdelay;

            yield return new WaitForSeconds(wait);
        }

        whilesaying = false;

        if (chat.end)
        {
            yield return new WaitForSeconds(1f);
            cammanager.GetComponent<CameraManager>().LookPlayer();
            letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
            player.GetComponent<PlayerMove>().canmove = true;
            chatnumber = 0;
            chating = false;
            playerchatbox.SetActive(false);
            enemychatbox.SetActive(false);
        }
    }

    char GetPrintedCharAtIndex(TMP_Text t, int visibleIndex)
    {
        if (visibleIndex < 0 || visibleIndex >= t.textInfo.characterCount) return '\0';
        var ci = t.textInfo.characterInfo[visibleIndex];
        return ci.character;
    }

    bool IsPunctuation(char c)
    {
        return c == '.' || c == ',' || c == '!' || c == '?' || c == ';' || c == ':' || c == '¡¦';
    }

    public void Test()
    {
        Debug.Log("dialogue:test fuction");
    }
}
