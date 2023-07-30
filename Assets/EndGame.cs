using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject endScreen;
    public LayerMask pLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position,3,pLayer);
        if(collider != null)
        {
            collider.gameObject.GetComponent<InsanitySystem>().canDie = false;
            collider.gameObject.GetComponent<InsanitySystem>().insanity = 0;
            endScreen.GetComponent<Image>().color = Color.Lerp(endScreen.GetComponent<Image>().color,new Color(1,1,1,1),0.2f*Time.deltaTime*60);
        }
    }
}
