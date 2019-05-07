using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;

    [Header("Collider")]
    [SerializeField] private float activeDelay;
    [SerializeField] private CircleCollider2D collider;

    private float dir;

    void OnEnable() {
        rb.AddForce(new Vector2(bulletSpeed * this.dir, 0));
        Invoke("ActiveCollider", activeDelay);
    }

    void SetDestroy(){
        rb.velocity = new Vector2(0, 0);
        collider.enabled = false;
        gameObject.SetActive(false);
    }

    void ActiveCollider(){
        collider.enabled = true;
    }

    public void SetDirection(bool dir){
        if(dir) this.dir = 1;
        else this.dir = -1;
    }

    public void SetTag(string name){
        gameObject.tag = name;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        SetDestroy();
    }
}
