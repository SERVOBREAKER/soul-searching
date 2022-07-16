using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC_Behavior : MonoBehaviour
{

    public string NPCTitle;
    public Text titleObject;

[Header("Stat Values")]
    [Range(0.0f,10.0f)] public float stat1;
    [Range(0.0f,10.0f)] public float stat2;
    [Range(0.0f,10.0f)] public float stat3;

    private float currentStat1, currentStat2, currentStat3;

    public float Normalize(float min, float max, float value)
    {return(value - min)/(max - min);}

[Header("Stat Sprites")]
    public Image statBar1;
    public Image statBar2;
    public Image statBar3;

[Header("Stat Modifiers")]
    public Float_Data statDrain1;
    public Float_Data statDrain2;
    public Float_Data statDrain3;

    [SerializeField] private bool isDraining = false;


    private void Start()
    {
        DisplayTitle();

        currentStat1 = stat1;
        currentStat2 = stat2;
        currentStat3 = stat3;

        //titleObject.gameObject.SetActive(true);

        statBar1.gameObject.SetActive(false);
        statBar2.gameObject.SetActive(false);
        statBar3.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (isDraining)
        {
            DrainStat();
        }
    }

//Handling of title text objects.
    private void DisplayTitle()
    {titleObject.text = NPCTitle;}

    private void RemoveTitle()
    {titleObject.text = null;}


//Handling of stats objects.
    private void DisplayStats()
    {
        statBar1.fillAmount = Normalize(0,stat1,currentStat1);
        statBar2.fillAmount = Normalize(0,stat2,currentStat2);
        statBar3.fillAmount = Normalize(0,stat3,currentStat3);
    }

    private void DrainStat()
    {
        currentStat1 -= statDrain1.value * Time.deltaTime;
        currentStat2 -= statDrain2.value * Time.deltaTime;
        currentStat3 -= statDrain3.value * Time.deltaTime;

        DisplayStats();
    }


//NPC mouse-over actions.

    private void OnMouseDown()
    {
        isDraining = !isDraining;
    }

    private void OnMouseEnter()
    {
        titleObject.transform.Translate(0,1,0);
    }

    private void OnMouseOver()
    {
        statBar1.gameObject.SetActive(true);
        statBar2.gameObject.SetActive(true);
        statBar3.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        titleObject.transform.Translate(0,-1,0);
        statBar1.gameObject.SetActive(false);
        statBar2.gameObject.SetActive(false);
        statBar3.gameObject.SetActive(false);
    }

}
