using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLineManager : MonoBehaviour
{
    public SuikaManager suikaManager;
    public Transform cauldron;
    public float cauldronHeightTrigger = 2f;

    void Update()
    {
        bool cekReach;

        foreach (Transform potion in cauldron)
        {
            if (potion.GetComponent<SuikaObject>().isTriggeringRedLine)
            {
                float potionHeightOnCauldron = potion.localPosition.y;

                if (potionHeightOnCauldron >= cauldronHeightTrigger)
                {
                    HeightReach();
                    return;
                }
            }
        }

        HeightNotReach();
    }
    void HeightReach()
    {
        suikaManager.Alert(true);
        Debug.Log("Bola mencapai ketinggian tertentu dalam cauldron!");
    }

    void HeightNotReach()
    {
        suikaManager.Alert(false);
    }
}
