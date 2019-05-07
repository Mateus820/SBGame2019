using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : Player
{

    protected override void Update(){
        if(Input.GetButtonDown("Jump") && jumpVerify) rb.AddForce(new Vector2(0, jumpForce));

        if(Input.GetButtonDown("Fire1")) Shot();

        if(Input.GetKeyDown(KeyCode.RightArrow) && !isFacingRight) Flip();
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && isFacingRight) Flip();

        base.Update();
    }

    protected override void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed * x, rb.velocity.y);        
    }
}
