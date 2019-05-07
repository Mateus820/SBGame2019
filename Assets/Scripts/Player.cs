using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isFacingRight;

    [SerializeField] protected float speed, jumpForce;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform bul;
    [SerializeField] protected ObjectPooler obp;
    
    protected bool jumpVerify;

    protected virtual void Start()
    {
        jumpVerify = false;
    }

    protected virtual void Update(){
        if(Input.GetButtonDown("Jump") && jumpVerify) rb.AddForce(new Vector2(0, jumpForce));

        if(Input.GetButtonDown("Fire1")) Shot();

        if(Input.GetKeyDown(KeyCode.RightArrow) && !isFacingRight) Flip();
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && isFacingRight) Flip();   
    }

    public virtual void Shot(){
        GameObject obj = obp.GetPooledObject();
        if(obj == null) return;
        obj.transform.position = bul.position;
        obj.GetComponent<Bullet>().SetDirection(isFacingRight);
        obj.SetActive(true);
    }

    public virtual void Flip(){
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected virtual void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed * x, rb.velocity.y);        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Ground"){
            jumpVerify = true;
        }    
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Ground"){
            jumpVerify = false;
        }    
    }
}
