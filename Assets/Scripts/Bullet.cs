using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;
    private float dir;

    void OnEnable() {
        rb.AddForce(new Vector2(bulletSpeed * this.dir, 0));
        Invoke("DestroyTime", 3f);
    }

    void DestroyTime(){
        rb.velocity = new Vector2(0, 0);
        gameObject.SetActive(false);
    }

    public void SetDirection(bool dir){
        print(dir);
        if(dir) this.dir = 1;
        else this.dir = -1;
    }
}
