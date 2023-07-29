using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Animator anim;

    [Header("Ghost Specific")]
    public float secondsDelay;
    public Transform player;
    public bool jumpDelayed;
    [Range(0,1)]
    public float copyLerp;

    [Header("Copied from PlayerMove")]
    public float moveSpeed;
    public float airMoveSpeed;
    public float maxSpeed;
    public float jumpForce;
    [Range(0,1)]
    public float moveLerp;
    public LayerMask layers;
    float horizontal;
    public bool grounded;
    Rigidbody2D physBody;
    // Start is called before the first frame update

    void OnEnable()
    {
        Invoke("PositionSelf", secondsDelay);
    }

    void PositionSelf()
    {
        transform.position = player.position;
    }

    IEnumerator CopyInput()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        //jumpDelayed = Input.GetButtonDown("Jump");
        Vector3 posCopy = player.position;
        Vector3 veloCopy = player.GetComponent<Rigidbody2D>().velocity;
        yield return new WaitForSeconds(secondsDelay);
        physBody.velocity = veloCopy;
        transform.position = Vector3.Slerp(transform.position,posCopy,copyLerp);
    }

    void Start()
    {
        physBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        StartCoroutine(CopyInput());
        //horizontal = Input.GetAxisRaw("Horizontal");
        grounded = Physics2D.OverlapCapsule(transform.position + Vector3.up * 0.75f, new Vector2(.8f, 2.8f), CapsuleDirection2D.Vertical, 0, layers);

        if(grounded == true)
        {
            physBody.velocity = new Vector2(Mathf.Lerp(physBody.velocity.x,0,moveLerp*Time.deltaTime*60),physBody.velocity.y);
            physBody.velocity += new Vector2(horizontal * moveSpeed * Time.deltaTime,0);
        }
        else
        {
            physBody.velocity = new Vector2(Mathf.Lerp(physBody.velocity.x,0,moveLerp*Time.deltaTime*60/4),physBody.velocity.y);
            physBody.velocity += new Vector2(horizontal * airMoveSpeed * Time.deltaTime,0);
        }

        if(grounded == true && jumpDelayed)
        {
            physBody.velocity += new Vector2(0,jumpForce);
        }

        physBody.velocity = new Vector2(Mathf.Clamp(physBody.velocity.x,-maxSpeed,maxSpeed),physBody.velocity.y);

        //Anim
        if(physBody.velocity.x <-.25f)
            anim.transform.localScale = new Vector2(-0.75f,0.75f);
        if(physBody.velocity.x >.25f)
            anim.transform.localScale = new Vector2(0.75f,0.75f);

        if(grounded == false)
        {
            anim.SetInteger("State",2);
        }
        else
        {
            if(Mathf.Abs(physBody.velocity.x) > 1f)
            {
                anim.SetInteger("State",1);
                anim.SetFloat("Speed",Mathf.Abs(physBody.velocity.x));
            }
            else
            {
                anim.SetInteger("State",0);
            }
        }

        
    }
}
