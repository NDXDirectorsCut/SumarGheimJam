using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsanityChange : MonoBehaviour
{
    public LayerMask layers;
    [Range(0,5)]
    public float radius;
    public float insanityChange;
    public bool changeSpawn;
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
        collider = Physics2D.OverlapCircle(transform.position,radius,layers);


        if(collider != null && used == false)
        {
            if(collider.transform.GetComponent<InsanitySystem>() != null)
            {
                used = true;
                collider.transform.GetComponent<InsanitySystem>().insanity += insanityChange;
                if(changeSpawn == true)
                {
                    collider.transform.GetComponent<InsanitySystem>().respawnPos = transform.position;
                }
                if(deleteOnCollect == true)
                {
                    Destroy(gameObject);
                }
                if(reusable == true)
                {
                    StartCoroutine(Reuse(1));
                }
            }
        }

    }

    IEnumerator Reuse(float delay)
    {
        yield return new WaitForSeconds(delay);
        used = false;
    }
}
