using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat_Display : MonoBehaviour
{

    public NPC_Behavior npc;

    public Text titleObject;
    public Image statbar1, statbar2, statbar3;
    public GameObject statBox;

    public void Start()
    {
        statBox.gameObject.SetActive(false);
    }

    public void InputNPC(NPC_Behavior newNPC)
    {
        if(npc != null)
        {
            npc.isPossessed = false;
        }

        npc = newNPC;

        if(npc == null)
        {
            statBox.gameObject.SetActive(false);
            return;
        }
        else
        {
            statBox.gameObject.SetActive(true);
            npc.isPossessed = true;

            titleObject.text = npc.NPCTitle;
        }
        


    }

    public void LateUpdate()
    {
        if (npc == null) return;

        statbar1.fillAmount = npc.normalizedStat1;
        statbar2.fillAmount = npc.normalizedStat2;
        statbar3.fillAmount = npc.normalizedStat3;
    }


}
