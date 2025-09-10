using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
}

[System.Serializable]
public class DialogueData
{
    public List<Dialogue> dialogueLines;
}

public class chatmanager : MonoBehaviour
{
    public bool chating;
    public bool whilesaying;
    public bool skip;

    public GameObject player;
    public GameObject letterbox;
    public List<DialogueData> dialogues;
    public DialogueData currentdialogues;
    public GameObject playerchatbox;
    public GameObject enemychatbox;
    public int chatnumber;

    public void Update()
    {
        if (chating)
        {
            if (Input.GetMouseButtonDown(0) && whilesaying!)
            {
                if (currentdialogues.dialogueLines[chatnumber].target == Chattarget.Enemy)
                {
                    StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], enemychatbox));
                    chatnumber++;
                }
                else if (currentdialogues.dialogueLines[chatnumber].target == Chattarget.Player)
                {
                    StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], playerchatbox));
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
            StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], enemychatbox));
            chatnumber++;
        }
        else if (currentdialogues.dialogueLines[chatnumber].target == Chattarget.Player)
        {
            StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], playerchatbox));
            chatnumber++;
        }
    }

    IEnumerator Chat(Dialogue chat, GameObject currentchatbox)
    {
        whilesaying = true;

        TMP_Text tmp = currentchatbox.transform.GetChild(0).GetComponent<TMP_Text>();
        tmp.text = chat.dialogue;
        tmp.ForceMeshUpdate();
        int totalvisible = tmp.textInfo.characterCount;
        tmp.maxVisibleCharacters = 0;
        int visible = 0;

        while (visible < totalvisible)
        {
            if (skip)
            {
                tmp.maxVisibleCharacters = totalvisible; 
                skip = false;
                whilesaying = false;
                yield break;                         
            }

            visible++;
            tmp.maxVisibleCharacters = visible;

            char c = GetPrintedCharAtIndex(tmp, visible - 1);
            float wait = chat.basicdelay;
            if (IsPunctuation(c)) wait += chat.dotdelay;

            yield return new WaitForSeconds(wait);
        }

        whilesaying = false;

        if (chat.end)
        {
            letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
            player.GetComponent<PlayerMove>().canmove = true;
            chatnumber = 0;
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
}
