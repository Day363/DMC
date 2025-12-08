using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[RequireComponent(typeof(TMP_Text))]
public class TextEffectManager : MonoBehaviour
{
    TMP_Text tmp;
    string originalText;

    struct ShakeRange
    {
        public int startIndex;
        public int endIndex;
        public float strength;
        public float speed;
    }

    struct CameraShakeEvent
    {
        public int triggerIndex;
        public float strength;
        public float duration;
    }

    struct FunctionEvent
    {
        public int fuctiontriggerIndex;
        public string fuctionname;
    }

    List<ShakeRange> shakeRanges = new List<ShakeRange>();
    List<CameraShakeEvent> cameraEvents = new List<CameraShakeEvent>();
    List<FunctionEvent> fuctionEvents = new List<FunctionEvent>();

    void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }

    public void SetText(string text)
    {
        originalText = text;
        ParseTags();
    }

    void ParseTags()
    {
        shakeRanges.Clear();
        cameraEvents.Clear();
        fuctionEvents.Clear();

        string workingText = originalText;

        string camPattern = @"<camerashake=(\d+(?:\.\d+)?),(\d+(?:\.\d+)?)>";
        int offset = 0;
        workingText = Regex.Replace(workingText, camPattern, match =>
        {
            float strength = float.Parse(match.Groups[1].Value);
            float duration = float.Parse(match.Groups[2].Value);

            int trigger = match.Index - offset;
            cameraEvents.Add(new CameraShakeEvent
            {
                triggerIndex = trigger,
                strength = strength,
                duration = duration
            });

            offset += match.Length;
            return "";
        }, RegexOptions.Singleline);

        string shakePattern = @"<shake=(\d+(?:\.\d+)?),(\d+(?:\.\d+)?)>(.*?)<\/shake>";
        MatchCollection matches = Regex.Matches(workingText, shakePattern, RegexOptions.Singleline);
        int removed = 0;
        foreach (Match m in matches)
        {
            float strength = float.Parse(m.Groups[1].Value);
            float speed = float.Parse(m.Groups[2].Value);
            string inner = m.Groups[3].Value;

            int startIndex = m.Index - removed;
            int length = inner.Length;

            shakeRanges.Add(new ShakeRange
            {
                startIndex = startIndex,
                endIndex = startIndex + length,
                strength = strength,
                speed = speed
            });

            removed += m.Length - inner.Length;
        }
        workingText = Regex.Replace(workingText, shakePattern, "$3", RegexOptions.Singleline);

        string fuctionPattern = @"<function=([^>]+)>";
        int delete = 0;
        workingText = Regex.Replace(workingText, fuctionPattern, match =>
        {
            string fuctionname_ = match.Groups[1].Value;

            int trigger = match.Index - offset;
            fuctionEvents.Add(new FunctionEvent
            {
                fuctiontriggerIndex = trigger,
                fuctionname = fuctionname_
            });

            delete += match.Length;
            return "";
        }, RegexOptions.Singleline);




        tmp.text = workingText;
        tmp.ForceMeshUpdate();
    }

    public void CheckEvents(int visibleCount)
    {
        List<int> executedCamera = new List<int>();
        List<int> executedFuction = new List<int>();
        for (int i = 0; i < cameraEvents.Count; i++)
        {
            if (visibleCount >= cameraEvents[i].triggerIndex)
            {
                CameraManager.Instance?.ShakeCamera(cameraEvents[i].strength, cameraEvents[i].duration);
                executedCamera.Add(i);
            }
        }
        for (int i = executedCamera.Count - 1; i >= 0; i--)
        {
            cameraEvents.RemoveAt(executedCamera[i]);
        }
        for (int f = 0; f < fuctionEvents.Count; f++)
        {
            if (visibleCount >= fuctionEvents[f].fuctiontriggerIndex)
            {
                battalemanager.Instance.gameObject.GetComponent<counselfunctionmanager>().FuctionStart(fuctionEvents[f].fuctionname);
                executedFuction.Add(f);
            }
        }
        for (int f = executedFuction.Count - 1; f >= 0; f--)
        {
            fuctionEvents.RemoveAt(executedFuction[f]);
        }
    }

    void LateUpdate()
    {
        if (shakeRanges.Count == 0) return;

        tmp.ForceMeshUpdate();
        var textInfo = tmp.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            foreach (var range in shakeRanges)
            {
                if (i >= range.startIndex && i < range.endIndex)
                {
                    int matIndex = textInfo.characterInfo[i].materialReferenceIndex;
                    int vertIndex = textInfo.characterInfo[i].vertexIndex;
                    var verts = textInfo.meshInfo[matIndex].vertices;

                    Vector3 offset = new Vector3(
                        (Mathf.PerlinNoise(Time.time * range.speed, i * 0.3f) - 0.5f) * 2f * range.strength,
                        (Mathf.PerlinNoise(i * 0.3f, Time.time * range.speed) - 0.5f) * 2f * range.strength,
                        0f
                    );

                    for (int j = 0; j < 4; j++)
                        verts[vertIndex + j] += offset;
                }
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            tmp.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}