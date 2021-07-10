using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// issues 
// flippin mario only through wasd not arrow and the flips aren't instantaneos

public class playerControl : MonoBehaviour
{   

    public float speed;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator ; 
    private AudioSource marioAudioSource ; 
    private bool faceRightState = true;
  
    private float moveHorizontal ; 
    public float maxSpeed = 10 ; 
    private bool onGroundState = true;
    public float upSpeed = 15 ; 


    // setting up the counting system 

    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;




  // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Platform")) {
          onGroundState = true;
          marioAnimator.SetBool("onGround", onGroundState) ; 
          countScoreState = false ; 
          scoreText.text = "Score: " + score.ToString() ; 
          
          
          }

        if (col.gameObject.CompareTag("Obstacle")&& Mathf.Abs(marioBody.velocity.y)<0.01f) {
          onGroundState = true;
          marioAnimator.SetBool("onGround", onGroundState) ; 
          
          
          
          }
    }

    // called before start to set up what ever required
    void Awake()
    {

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
    }

    void FixedUpdate()
    {


    
      // dynamic rigidbody
        moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
      }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
          // stop
          marioBody.velocity = Vector2.zero;
      }

        if (Input.GetKeyDown("space") && onGroundState){
          marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
          onGroundState = false;
          marioAnimator.SetBool("onGround", onGroundState) ; 
          countScoreState = true;
      }

      }

    void OnTriggerEnter2D(Collider2D other){
      if (other.gameObject.CompareTag("Enemy")) {
        Debug.Log("Collided with Gomba! ") ; 
        Time.timeScale = 0.0f ; 
        
      }
    }

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
      if (!onGroundState && countScoreState)
      {
        
          if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
          {
              Debug.Log("here") ; 
              countScoreState = false;
              score++;
              Debug.Log(score);
          }
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
