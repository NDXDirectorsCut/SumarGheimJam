using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public Button butt;
    // Start is called before the first frame update
    void Start()
    {
        butt = gameObject.GetComponent<Button>();
        butt.onClick.AddListener(PeClick);
    }

    // Update is called once per frame
    void PeClick()
    {
        SceneManager.LoadScene("backup", LoadSceneMode.Single);
    }
}