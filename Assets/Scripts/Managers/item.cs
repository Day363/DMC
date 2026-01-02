using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class item : ScriptableObject
{
    public enum itemtag { item, rapport}

    public itemtag itemtag_;
    public string itemName;
    public Sprite itemImage;
    public bool usable;
    [TextArea(5, 10)]
    public string itemdescription;
    [TextArea(5, 10)]
    public string stroydescription;
}
