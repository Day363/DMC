using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Rapport : ScriptableObject
{
    public item.itemtag itemtag_;

    public string itemName;
    public Sprite itemImage;
    [TextArea(5, 10)]
    public string itemdescription;
    [TextArea(5, 10)]
    public string stroydescription;
}
