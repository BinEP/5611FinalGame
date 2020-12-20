using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipabbleItem : MonoBehaviour
{
    public int uses;

    public static void equip()
    {
        //GlobalVars.equipabbleItem = ??;
    }

    public void useItem()
    {
        print("using");
        if (uses > 0)
        {
            uses--;
            activate();
        }

        if (uses == 0)
        {
            unequip();
        }
    }

    public void unequip()
    {
        GlobalVars.equipabbleItem = null;
    }

    protected void activate()
    {

    }
}
