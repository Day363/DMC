using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public enum Chattarget {Player, Enemy}

[System.Serializable]
public class Dialogue
{
    public Chattarget target;
    public string objectname;

    [TextArea(5, 10)]
    public string dialogue;

    public float basicdelay = 0.1f;
    public float dotdelay = 0.5f;

    public bool cantskip = false;

    public bool end;
    public bool end_lookplayer = true;
    public bool end_letterboxin = true;

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
    public string dialoguename;//°³¹ß¿ë
    public string playervoice = "player_chat";
    public string enemyvoice;
    public List<Dialogue> dialogueLines;
}

public class chatmanager : MonoBehaviour
{
    public bool conserned;

    public static event Action OnchatEnd;

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

    public string curvoice;

    private void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        cammanager = battalemanager.Instance.cameramanager;
        player = battalemanager.Instance.player;
        letterbox = battalemanager.Instance.letterbox;
        playerchatbox = battalemanager.Instance.playerchatbox;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshRefs();
        chatnumber = 0;
    }

    private void RefreshRefs()
    {
        cammanager = battalemanager.Instance.cameramanager;
        player = battalemanager.Instance.player;
        letterbox = battalemanager.Instance.letterbox;
        playerchatbox = battalemanager.Instance.playerchatbox;
    }

    public void Update()
    {
        if (!chating) return;

        if (chatnumber >= currentdialogues.dialogueLines.Count)
        {
            chating = false;
            return;
        }

        if (Input.GetMouseButtonDown(0) && !whilesaying)
        {
            if (currentchatco != null)
            {
                StopCoroutine(currentchatco);
            }

            var line = currentdialogues.dialogueLines[chatnumber];

            GameObject currentenemy = battalemanager.Instance.currentenemys.Find(obj => obj.name == currentdialogues.dialogueLines[chatnumber].objectname);
            enemychatbox = currentenemy.GetComponent<boss_hpbar>().chatbox;

            if (line.target == Chattarget.Enemy)
            {
                currentchatco = StartCoroutine(Chat(line, enemychatbox));
            }
            else if (line.target == Chattarget.Player)
            {
                currentchatco = StartCoroutine(Chat(line, playerchatbox));
            }

            chatnumber++;
        }
        else if (Input.GetMouseButtonDown(0) && whilesaying)
        {
            skip = true;
        }
    }

    public void StopChatting()
    {
        StopCoroutine(currentchatco);
    }

    public void CallDialogue(int i)
    {
        currentdialogues = dialogues[i];
        chating = true;
        playerchatbox.SetActive(true);

        foreach (GameObject enemy in battalemanager.Instance.currentenemys)
        {
            enemy.GetComponent<boss_hpbar>().chatbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            enemy.GetComponent<boss_hpbar>().chatbox.SetActive(true);
        }
        
        if (currentdialogues.dialogueLines[chatnumber].target == Chattarget.Enemy)
        {
            curvoice = currentdialogues.enemyvoice;
            if (currentchatco != null)
            {
                StopCoroutine(currentchatco);
            }

            GameObject currentenemy = battalemanager.Instance.currentenemys.Find(obj => obj.name == currentdialogues.dialogueLines[chatnumber].objectname);
            enemychatbox = currentenemy.GetComponent<boss_hpbar>().chatbox;

            currentchatco = StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], enemychatbox));
            chatnumber++;
        }
        else if (currentdialogues.dialogueLines[chatnumber].target == Chattarget.Player)
        {
            curvoice = currentdialogues.playervoice;
            if (currentchatco != null)
            {
                StopCoroutine(currentchatco);
            }
            currentchatco = StartCoroutine(Chat(currentdialogues.dialogueLines[chatnumber], playerchatbox));
            chatnumber++;
        }
    }

    string MaskExceptSpace(string input)
    {
        char[] chars = input.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (!char.IsWhiteSpace(chars[i]))
                chars[i] = '¡á';
        }

        return new string(chars);
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
            if (skip && !chat.cantskip)
            {
                tmp.maxVisibleCharacters = totalvisible; 
                skip = false;
                whilesaying = false;
                if (shaker != null)
                    shaker.CheckEvents(totalvisible);
                if (chat.gotoanswer)
                {

                }
                if (chat.end)
                {
                    yield return new WaitForSeconds(1f);
                    if (chat.end_lookplayer)
                    {
                        cammanager.GetComponent<CameraManager>().LookPlayer();
                    }
                    if (chat.end_letterboxin)
                    {
                        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
                    }
                    player.GetComponent<PlayerMove>().canmove = true;
                    chatnumber = 0;
                    chating = false;
                    playerchatbox.SetActive(false);
                    if (enemychatbox != null)
                    {
                        enemychatbox.SetActive(true);
                    }
                }
                break;                         
            }

            visible++;
            tmp.maxVisibleCharacters = visible;

            char c = GetPrintedCharAtIndex(tmp, visible - 1);
            //SoundPlay(c, curvoice);

            if (shaker != null)
                shaker.CheckEvents(visible);

            c = GetPrintedCharAtIndex(tmp, visible - 1);
            float wait = chat.basicdelay;
            if (IsPunctuation(c)) wait += chat.dotdelay;

            yield return new WaitForSeconds(wait);
        }

        

        if (chat.gotoanswer)
        {

        }
        if (chat.end)
        {
            yield return new WaitForSeconds(1f);
            if (chat.end_lookplayer)
            {
                cammanager.GetComponent<CameraManager>().LookPlayer();
            }
            if (chat.end_letterboxin)
            {
                letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
            }         
            player.GetComponent<PlayerMove>().canmove = true;
            chatnumber = 0;
            chating = false;
            playerchatbox.SetActive(false);
            if (enemychatbox != null)
            {
                foreach (GameObject enemy in battalemanager.Instance.currentenemys)
                {
                    enemy.GetComponent<boss_hpbar>().chatbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
                    enemy.GetComponent<boss_hpbar>().chatbox.SetActive(false);
                }
            }

            OnchatEnd?.Invoke();
        }

        whilesaying = false;
    }

    char GetPrintedCharAtIndex(TMP_Text t, int visibleIndex)
    {
        if (visibleIndex < 0 || visibleIndex >= t.textInfo.characterCount) return '\0';
        var ci = t.textInfo.characterInfo[visibleIndex];
        return ci.character;
    }

    //public void SoundPlay(char i, string sound)
    //{
    //    if (char.IsWhiteSpace(i)) return;
    //    if (IsPunctuation(i)) return; 

    //    if (sound != null)
    //    GetComponent<soundmanager>().SoundPlay(sound);
    //}

    bool IsPunctuation(char c)
    {
        return c == '.' || c == ',' || c == '!' || c == '?' || c == ';' || c == ':' || c == '¡¦';
    }

    public void Test()
    {
        Debug.Log("dialogue:test fuction");
    }
}
