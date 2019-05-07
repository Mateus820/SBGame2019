using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool isFacingRight;

    [Header("Life")]
    [SerializeField] protected float curLife; // 0 - 100
    [SerializeField] protected float maxLife;
    [SerializeField] protected Image UI_life;

    [Header("Physics")]
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform bulletSpawn;
    protected bool jumpVerify;

    [Header("Shot")]
    [SerializeField] protected string tagBulletName;
    [SerializeField] protected string damageTag;
    [SerializeField] protected float shotDelay;
    [SerializeField] protected bool canShot;
    [SerializeField] protected Image UI_shotTime;
    [SerializeField] protected Text UI_shotText;
    [SerializeField] protected ObjectPooler obp;

    #region Pattern Fuctions
    protected virtual void Start()
    {
        jumpVerify = false;
        curLife = maxLife;
    }

    protected virtual void Update(){

        VerifyLife();
        UpdateUI();   
    }

    protected virtual void FixedUpdate(){
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed * x, rb.velocity.y);        
    }

    public virtual void Shot(){
        
        //I stoped in here!!
        if(!canShot) return;

        GameObject obj = obp.GetPooledObject();
        if(obj == null) return;
        obj.transform.position = bulletSpawn.position;

        Bullet bul = obj.GetComponent<Bullet>();
        bul.SetDirection(isFacingRight);
        bul.SetTag(tagBulletName);

        StartCoroutine(DownTimeShot());

        obj.SetActive(true);
    }

    #endregion

    IEnumerator DownTimeShot(){

        float shotDelayTemp = shotDelay;
        //I stoped in here!!
        if(!canShot){
            while(shotDelayTemp > 0){
                canShot = true;
                yield return new WaitForSeconds(0.1f);
                shotDelayTemp -= 0.1f;
                ShotDelayUI(shotDelayTemp);
            }
        }
        canShot = false;
        print("Shot Delay: " + shotDelayTemp);
    }

    #region UI

    protected virtual void UpdateUI(){
        UI_life.fillAmount = AdjustLifeLimits(curLife);
    }

    protected virtual void ShotDelayUI(float time){
        //Please, Adjust!!!1
        UI_shotTime.fillAmount = time;
        UI_shotText.text = time.ToString();
    }

    #endregion

    #region Other Fuctions

    protected virtual void VerifyLife(){
        if(curLife <= 0){
            LostGame();
        }
    }

    protected virtual float AdjustLifeLimits(float v){
        if(v > maxLife) v = maxLife;
        else if(v < 0) v = 0;

        return v / 100;
    }

    protected virtual void Flip(){
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected virtual void LostGame(){
        SceneManager.LoadScene("GameOver");
    }

    #endregion

    #region Collider Fuctions
    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if(damageTag == other.gameObject.tag){
            if(curLife > 0) curLife -= 20;
        }
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
    #endregion
}
