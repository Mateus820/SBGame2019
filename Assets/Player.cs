using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool jumpVerify;

    void Start()
    {
        jumpVerify = false;
    }

    void Update(){
        if(Input.GetButtonDown("Jump") && jumpVerify){
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed * x, rb.velocity.y);        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Ground"){
            jumpVerify = true;
        }    
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Ground"){
            jumpVerify = false;
        }    
    }
}
