using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Weapon> weaponinv = new List<Weapon>();
    public List<Rapport> rapportinv = new List<Rapport>();
    public weaponslot[] weaponslots;

    private void Update()
    {
        for (int i = 1; i <= weaponinv.Count; i++)
        {
            weaponslots[i - 1].weaponimage = weaponinv[i - 1].weaponimage;
        }
    }
}
