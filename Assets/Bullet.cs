using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;

    void OnEnable() {
        rb.AddForce(new Vector2(bulletSpeed, 0));
        Invoke("DestroyTime", 3f);
    }

    void DestroyTime(){
        rb.velocity = new Vector2(0, 0);
        gameObject.SetActive(false);
    }
}
