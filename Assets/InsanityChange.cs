using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsanityChange : MonoBehaviour
{
    public LayerMask layers;
    [Range(0,5)]
    public float radius;
    public float insanityChange;
    public bool used;
    public bool reusable;
    //public float reusableDelay;
    public bool deleteOnCollect;
    public bool ghost;
    public Ghost ghostScript;
    Vector3 tele;
    // Start is called before the first frame update
    void Start()
    {
        tele = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider;
        if(ghost == false)
             collider = Physics2D.OverlapCircle(transform.position,radius,layers);
        else
        {
             collider = Physics2D.OverlapCapsule(transform.position + Vector3.up * 0.75f, new Vector2(.8f, 2.8f), CapsuleDirection2D.Vertical, 0, layers);
        }

        if(collider != null && used == false)
        {
            if(used == true && ghost == true)
            {
                transform.position = tele;
            }   
            if(collider.transform.GetComponent<InsanitySystem>() != null)
            {
                used = true;
                collider.transform.GetComponent<InsanitySystem>().insanity += insanityChange;
                if(deleteOnCollect == true)
                {
                    Destroy(gameObject);
                }
                if(reusable == true)
                {
                    //StartCoroutine(Reuse(1));
                }
                if(ghost == true)
                {
                    //ghostScript
                    ghostScript.enabled = false;
                    transform.position = tele;
                }
            }
        }

        

    }

    //IEnumerator Reuse(float delay)
    //{
        
    //}
}
