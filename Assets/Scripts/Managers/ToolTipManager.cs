using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager Instance;
    public Dictionary<string, Stack> tooltipMap;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void Start()
    {
        tooltipMap = new Dictionary<string, Stack>
        {
            { "bleed", battalemanager.Instance.stackdatas[2]}
        };
    }
}
