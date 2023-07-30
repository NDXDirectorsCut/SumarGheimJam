using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public GameObject fade;
    public GameObject startScreen;
    public Button butt;
    public bool fadeLerp;
    public bool DocLerp;
    // Start is called before the first frame update
    void Start()
    {
        butt = gameObject.GetComponent<Button>();
        butt.onClick.AddListener(PeClick);
    }

    // Update is called once per frame
    void PeClick()
    {
        //SceneManager.LoadScene("backup", LoadSceneMode.Single);
        StartCoroutine(StartGame());
    }

    void Update()
    {
        if(fadeLerp == true)
            fade.GetComponent<Image>().color = Color.Lerp(fade.GetComponent<Image>().color,new Color(0,0,0,1),0.2f*Time.deltaTime*60);
        if(DocLerp == true)
            startScreen.GetComponent<Image>().color = Color.Lerp(startScreen.GetComponent<Image>().color,new Color(1,1,1,1),0.2f*Time.deltaTime*60);
    }

    IEnumerator StartGame()
    {
        fadeLerp = true;
        yield return new WaitForSeconds(1);
        DocLerp = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("backup", LoadSceneMode.Single);
    }

}