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

    //[SerializeField] 
    private float currentStat1, currentStat2, currentStat3;

    public float Normalize(float min, float max, float value)
    {return(value - min)/(max - min);}

[HideInInspector]
    public float normalizedStat1, normalizedStat2, normalizedStat3;

[Header("Stat Modifiers")]
    public Float_Data statDrain1;
    public Float_Data statDrain2;
    public Float_Data statDrain3;

    public Float_Data regenRate1;
    public Float_Data regenRate2;
    public Float_Data regenRate3;

[Header("Stat Sprites")]
    public Image statBar1;
    public Image statBar2;
    public Image statBar3;
    public SpriteRenderer backdropSprite;
    
[HideInInspector]
    public bool isPossessed = false;

    public GameObject particle;

    private Stat_Display statDisplay;

    private float titleStartY;


    private void Start()
    {
        DisplayTitle();

        currentStat1 = stat1;
        currentStat2 = stat2;
        currentStat3 = stat3;

        isPossessed = false;

        //titleObject.gameObject.SetActive(true);

        SetObjectStates(false);

        backdropSprite.size = new Vector2(3f,0.5f);

        statDisplay = FindObjectOfType<Stat_Display>();

        titleStartY = titleObject.transform.localPosition.y;

    }

    private void LateUpdate()
    {
        if (isPossessed)
        {
            DrainStat();
            particle.gameObject.SetActive(true);
        }

        if (!isPossessed)
        {
            GainStat();
            particle.gameObject.SetActive(false);
        }

        if(Input.GetMouseButtonDown(1))
        {
            isPossessed = false;
            statDisplay.InputNPC(null);
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
        normalizedStat1 = Normalize(0,stat1,currentStat1);
        normalizedStat2 = Normalize(0,stat2,currentStat2);
        normalizedStat3 = Normalize(0,stat3,currentStat3);

        statBar1.fillAmount = normalizedStat1;
        statBar2.fillAmount = normalizedStat2;
        statBar3.fillAmount = normalizedStat3;
    }

    private void GainStat()
    {
        if (currentStat1 <= stat1) {currentStat1 += regenRate1.value * Time.deltaTime;}
        if (currentStat2 <= stat2) {currentStat2 += regenRate2.value * Time.deltaTime;}
        if (currentStat3 <= stat3) {currentStat3 += regenRate3.value * Time.deltaTime;}

        DisplayStats();
    }

    private void DrainStat()
    {

        if (currentStat1 >= 0) {currentStat1 -= statDrain1.value * Time.deltaTime;}
        if (currentStat2 >= 0) {currentStat2 -= statDrain2.value * Time.deltaTime;}
        if (currentStat3 >= 0) {currentStat3 -= statDrain3.value * Time.deltaTime;}

        DisplayStats();
    }


//NPC mouse-over actions.

    private void OnMouseDown()
    {
        statDisplay.InputNPC(this);

        particle.gameObject.SetActive(true);
        backdropSprite.size = new Vector2(3f,0.5f);
        titleObject.transform.localPosition = new Vector3(0,titleStartY,0);

        SetObjectStates(false);
    }

    private void OnMouseEnter()
    {

        if (isPossessed) return;

        backdropSprite.size = new Vector2(3f,2f);
        titleObject.transform.localPosition = new Vector3(0,titleStartY + 1.35f,0);
        SetObjectStates(true);
    }


    private void OnMouseExit()
    {
        if(isPossessed) return;

        backdropSprite.size = new Vector2(3f,0.5f);
        titleObject.transform.localPosition = new Vector3(0,titleStartY,0);
        SetObjectStates(false);
    }


    private void SetObjectStates(bool state)
    {
        statBar1.gameObject.SetActive(state);
        statBar2.gameObject.SetActive(state);
        statBar3.gameObject.SetActive(state);
    }

}
