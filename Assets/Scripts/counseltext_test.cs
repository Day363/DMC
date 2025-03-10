using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

[System.Serializable]
public class RowArray
{
    [TextArea]
    public string[] text;
}
public class counseltext_test : MonoBehaviour
{
    public GameObject player;
    public GameObject slashcore;
    public GameObject cam;
    public GameObject self;
    public TMP_Text counseltext;
    public GameObject answerbuttonset;
    public GameObject counselmanager;
    public TMP_Text[] answerbuttons;
    public GameObject counsellngbar;
    public GameObject TV;
    public RowArray[] textlist;
    public RowArray[] answerlist;
    public int bundlenumber = 0;
    public int answernumber = 0;
    public int textnumber = 0;
    public float delay;
    public bool telling = false;
    public bool counselling = false;
    public bool answeringnow = false;
    private string[] textbundle;
    private string[] answerbundle;
    public string route1text;
    public string route2text;
    public string route3text;
    public int route1;
    public int route2;
    public int route3;
    public string buttontext1;
    public string buttontext2;
    public string buttontext3;
    public string answerroutetext;
    public int answerroute;
    public int animationcontrol;
    public int cinemachinecontrol;
    public bool end = false;

    private void Awake()
    {
        counselmanager.GetComponent<CounselManager_test>().counselingentity = self;
        counseltext.text = "...";
    }

    private void Update()
    {   if (counselling)
        {
            if (Input.GetMouseButtonDown(0) && !telling)
            {
                counseltext.text = null;
                telling = true;

                textbundle = textlist[bundlenumber].text;

                if (textbundle[textnumber].Contains("^"))
                {
                    textnumber = 0;
                    bundlenumber = bundlenumber + 1;
                    textbundle = textlist[bundlenumber].text;
                }

                if (textbundle[textnumber].Contains("@"))
                {
                    animationcontrol = int.Parse(Regex.Replace(textbundle[textnumber], @"\D", ""));
                    GetComponent<entityanimation>().Animationtrigger(animationcontrol);
                    textnumber++;
                }

                if (textbundle[textnumber].Contains("$"))
                {
                    cinemachinecontrol = int.Parse(Regex.Replace(textbundle[textnumber], @"\D", ""));
                    GetComponent<cinemachinecontrol>().Cinemachinetrigger(cinemachinecontrol);
                    textnumber++;
                }

                if (textbundle[textnumber].Contains("Sn"))
                {
                    cinemachinecontrol = int.Parse(Regex.Replace(textbundle[textnumber], @"\D", ""));
                    GetComponent<cinemachinecontrol>().Cinemachinetriggern(cinemachinecontrol);
                    textnumber++;
                }

                if (textbundle[textnumber].Contains("&"))
                {
                    answerroutetext = Regex.Replace(textbundle[textnumber], @"\D", "");
                    answerroute = int.Parse(answerroutetext);
                    answernumber = answerroute;

                    answerbundle = answerlist[answernumber].text;
                    route1text = Regex.Replace(answerbundle[0], @"\D", "");
                    route1 = int.Parse(route1text);
                    route2text = Regex.Replace(answerbundle[1], @"\D", "");
                    route2 = int.Parse(route2text);
                    route3text = Regex.Replace(answerbundle[2], @"\D", "");
                    route3 = int.Parse(route3text);
                    buttontext1 = Regex.Replace(answerbundle[0], @"[0-9]", "");
                    buttontext2 = Regex.Replace(answerbundle[1], @"[0-9]", "");
                    buttontext3 = Regex.Replace(answerbundle[2], @"[0-9]", "");

                    answeringnow = true;
                    answerbuttonset.SetActive(true);
                    answerbuttons[0].text = buttontext1;
                    answerbuttons[1].text = buttontext2;
                    answerbuttons[2].text = buttontext3;
                }

                if (textbundle[textnumber].Contains("fIght"))
                {
                    counselling = false;
                    counsellngbar.SetActive(false);
                    end = true;
                    slashcore.GetComponent<playerslashtest>().canattack = true;
                    counselling = false;
                }

                if (textbundle[textnumber].Contains("cLear"))
                {
                    counselling = false;
                    counsellngbar.SetActive(false);
                    self.SetActive(false);
                    end = true;
                    TV.SetActive(true);
                    player.GetComponent<PlayerMove>().canmove = true;
                    cam.SetActive(false);
                    slashcore.GetComponent<playerslashtest>().canattack = true;
                    counselling = false;
                }

                if (textbundle[textnumber].Contains("fAil"))
                {
                    counselling = false;
                    counsellngbar.SetActive(false);
                    self.SetActive(false);
                    end = true;
                    TV.SetActive(true);
                    player.GetComponent<PlayerMove>().canmove = true;
                    cam.SetActive(false);
                    slashcore.GetComponent<playerslashtest>().canattack = true;
                    counselling = false;
                }

                if (!answeringnow && !end)
                {
                    StartCoroutine(Typing(textbundle[textnumber], delay));
                    textnumber++;
                }
            }
        }
    }

    IEnumerator Typing(string text, float d)
    {
        int count = 0;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                counseltext.text += text[count].ToString();
                count++;
            }

            yield return new WaitForSeconds(d);
        }

        telling = false;
    }

    
}
