using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GhostChange
{
    public float insanityRequired;
    public float newDelay;
}

public class InsanitySystem : MonoBehaviour
{
    public RectTransform progressBar;
    public GameObject fadeToBlack;
    public Vector3 respawnPos;
    public float spawnTime = 1.5f;
    //public GhostChange[] ghostChanges;
    public Ghost ghostScript;
    public Schizo effects;
    public SwitchMusic music;
    public int level;
    int prevLevel;
    /*public*/ float width;
    /*public*/ float height;
    [Range(0,1)]
    public float lerp;
    public float insanity;
    public float decay;
    public bool canDie = true;
    //[Space(20)]

    // Start is called before the first frame update
    void Start()
    {
        prevLevel = level;
        //Debug.Log(progressBar.parent.gameObject.GetComponent<RectTransform>().sizeDelta);
        width = progressBar.parent.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        height = progressBar.parent.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        respawnPos = transform.position;
        
    }

    IEnumerator Respawn()
    {
        canDie = false;
        yield return new WaitForSeconds(spawnTime);
        transform.position = respawnPos;
        music.gameObject.SetActive(true);
        insanity = 100f;
        transform.GetComponent<PlayerMove>().enabled = true;

        yield return new WaitForSeconds(0.5f);
        ghostScript.gameObject.SetActive(true);
        canDie = true;
    }

    // Update is called once per frame
    void Update()
    {
        insanity = Mathf.Clamp(insanity,0,100);

        if(insanity <=0)
        {
            fadeToBlack.GetComponent<Image>().color = Color.Lerp(fadeToBlack.GetComponent<Image>().color,new Color(0,0,0,1),0.2f*Time.deltaTime*60);
            transform.GetComponent<PlayerMove>().enabled = false;
            music.gameObject.SetActive(false);
            ghostScript.gameObject.SetActive(false);
            if(canDie == true)
                StartCoroutine(Respawn());
        }
        else
        {
            fadeToBlack.GetComponent<Image>().color = Color.Lerp(fadeToBlack.GetComponent<Image>().color,new Color(1,1,1,0),0.2f*Time.deltaTime*60);
        }

        Vector2 progressSize = new Vector2((insanity/100)*width,height);
        progressBar.sizeDelta = Vector2.Lerp(progressBar.sizeDelta,progressSize,lerp);
        insanity -= decay*Time.deltaTime;
        
        effects.insanity = (100-insanity)*1.25f;
        music.transition = (insanity/100)*2-.5f;

        if(insanity >=90 && insanity <= 100)
        {
            //Debug.Log("level 0");
            level = 0;
            ghostScript.secondsDelay = 3;
            ghostScript.transform.position = ghostScript.tele;
            ghostScript.sync = false;
            //ghostScript.enabled = false;
            ghostScript.gameObject.SetActive(true);
        }
        if(insanity >=50 && insanity < 90)
        {
            level = 1;
            //Debug.Log("level 1");
            //StartCoroutine(ghostScript.Reuse());
            ghostScript.secondsDelay = 3;
            ghostScript.enabled = true;
            ghostScript.gameObject.SetActive(true);
        }
        if(insanity >=25 && insanity < 50)
        {
            level = 2;
            //Debug.Log("level 2");
            //StartCoroutine(ghostScript.Reuse());
            ghostScript.secondsDelay = 2.5f;
            ghostScript.enabled = true;
            ghostScript.gameObject.SetActive(true);
        }
        if(insanity > 0 && insanity < 25)
        {
            level = 3;
            //StartCoroutine(ghostScript.Reuse());
            ghostScript.secondsDelay = 1.8f;
            ghostScript.enabled = true;
            ghostScript.gameObject.SetActive(true);
        }
        if(level != prevLevel && level != 0)
        {
            prevLevel = level;  
            ghostScript.used = true;
            //Debug.Log("used = true From System.cs");
            //collider.transform.GetComponent<InsanitySystem>().insanity += insanityChange;
            ghostScript.sync = false;
            ghostScript.gameObject.transform.position = ghostScript.tele;
            StartCoroutine(ghostScript.Reuse());
        }
        
        //Debug.Log((progressBar.sizeDelta.x/width)*100);
    }
}
