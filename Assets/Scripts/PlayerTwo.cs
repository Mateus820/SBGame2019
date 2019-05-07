using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : Player
{   

    protected override void Update(){
        if(Input.GetButtonDown("Jump2") && jumpVerify) rb.AddForce(new Vector2(0, jumpForce));

        if(Input.GetButtonDown("Fire2")) Shot();

        if(Input.GetKeyDown(KeyCode.D) && !isFacingRight) Flip();
        else if(Input.GetKeyDown(KeyCode.A) && isFacingRight) Flip();

        base.Update(); 
    }

    protected override void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal2");
        rb.velocity = new Vector2(speed * x, rb.velocity.y);        
    }
}
