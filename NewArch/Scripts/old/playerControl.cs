using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events ; 


// issues 
// flippin mario only through wasd not arrow and the flips aren't instantaneos

public class playerControl : MonoBehaviour
{   

   
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator ; 
    private AudioSource marioAudioSource ; 
    private bool faceRightState = true;


    private bool isDead = false ; 
    public GameObjectConstants constants ;  
    private float moveHorizontal ; 
    //public float constants.playerMaxSpeed = constants.playerMaxJumpSpeed; 
    private bool onGroundState = true;

    public CustomPowerupCastEvent onPowerupCast ; 
    //public float constants.playerMaxJumpSpeed = GameObjectCo ; 


    // setting up the counting system 

   
  




  // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Platform")) {
          onGroundState = true;
          marioAnimator.SetBool("onGround", onGroundState) ; 
         
            
          }

        if (col.gameObject.CompareTag("Obstacle")&& Mathf.Abs(marioBody.velocity.y)<0.01f) {
          onGroundState = true;
          marioAnimator.SetBool("onGround", onGroundState) ; 
          
          
          
          }
    }

    // called before start to set up what ever required
    void Awake()
    {

        marioUpSpeed.SetValue(constants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(constants.playerMaxSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
       
      Application.targetFrameRate =  30;
      marioBody = GetComponent<Rigidbody2D>();
      marioSprite = GetComponent<SpriteRenderer>();
      marioAnimator = GetComponent<Animator>() ;
      marioAudioSource = GetComponent<AudioSource>() ;
      marioAnimator.SetBool("isDead", isDead) ;  

    }
   
    public void PlayerDiesSequence()
    {
        isDead = true;
        marioAnimator.SetBool("isDead", isDead);
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector3.up * 50, ForceMode2D.Impulse);
        marioBody.gravityScale = 5;
        StartCoroutine(dead());
    }
    
    IEnumerator dead()
    {
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
        //Time.timeScale = 0.0f ; 
        // trigger the reset function 
    }   
   void FixedUpdate()

    {
      if (!isDead){

      // dynamic rigidbody
        moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement * marioMaxSpeed.Value);
      }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
          // stop
          marioBody.velocity = Vector2.zero;
      }

        if (Input.GetKeyDown("space") && onGroundState){
          marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
          onGroundState = false;
          marioAnimator.SetBool("onGround", onGroundState) ; 
      }


      }

    
      }

    // void OnTriggerEnter2D(Collider2D other){
      // if (other.gameObject.CompareTag("Enemy")) {
        // Debug.Log("Collided with Gomba! ") ; 
        // Time.timeScale = 0.0f ; 
        // Time.timeScale = 0.0f ; 
        // 
      // }
    // }
// 
    // Update is called once per frame
    void Update()
    {

      // flipping of mario 
      if (Input.GetKeyDown("a") && faceRightState){
        faceRightState = false ;
        marioSprite.flipX = true ; 
        
        // new 

          if(Mathf.Abs(marioBody.velocity.x)>1.0){
            marioAnimator.SetTrigger("onSkid") ; 
        }
         
      }

      if (Input.GetKeyDown("d") && !faceRightState){
        faceRightState = true ; 
        marioSprite.flipX = false ; 


// new 
          if(Mathf.Abs(marioBody.velocity.x)>1.0){
            marioAnimator.SetTrigger("onSkid") ; 
          }
      }

      marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x)) ; 
    
    // counting 

      if (Input.GetKeyDown("j")){
        //CentralManager.centralManagerInstance.consumePowerup(KeyCode.J,this.gameObject);
        onPowerupCast.Invoke(KeyCode.J) ; 

      }

      if (Input.GetKeyDown("k")){
        //CentralManager.centralManagerInstance.consumePowerup(KeyCode.K,this.gameObject);         
        onPowerupCast.Invoke(KeyCode.K) ; 
      }

      // adding the restart 
       if (Input.GetKeyDown(KeyCode.R))
         {
             Application.LoadLevel(0);
         }
    }

    void PlayJumpSound(){
      marioAudioSource.PlayOneShot(marioAudioSource.clip) ; 

    }
}
