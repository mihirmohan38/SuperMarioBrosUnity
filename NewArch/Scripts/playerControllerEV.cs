using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerEV : MonoBehaviour
{
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameObjectConstants gameConstants;

    // player components 
   
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator ; 
    private AudioSource marioAudioSource ; 

    private bool isDead ; 

    private bool onGroundState ; 

    private bool faceRightState ; 
    // state values 
    private bool isADKeyUp ; 
    private bool isSpacebarUp ; 
    
    // Start is called before the first frame update
    void Start()
    {
        // init values 
        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerMaxSpeed);
        force = gameConstants.playerDefaultForce;
        isDead = false ; 
        faceRightState = true ; 
        onGroundState = true ; 
        isADKeyUp = false ; 
        isSpacebarUp = false ; 
        // getting mario components 
        
        
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>() ;
        marioAudioSource = GetComponent<AudioSource>() ;
    }
    
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

    // Update is called once per frame
    void Update()
    {

      // flipping of mario 
      if (Input.GetKeyUp("a") && faceRightState){
        faceRightState = false ;
        marioSprite.flipX = true ; 
        
        isADKeyUp = true ;  
        // new 

          if(Mathf.Abs(marioBody.velocity.x)>1.0){
            marioAnimator.SetTrigger("onSkid") ; 
        }
         
      }else {
          isADKeyUp = false ; 
      }

      if (Input.GetKeyUp("space")){
          isSpacebarUp = true ; 
      } else {
          isSpacebarUp = false ; 
      }

      if (Input.GetKeyUp("d") && !faceRightState){
        faceRightState = true ; 
        marioSprite.flipX = false ; 
        isADKeyUp = true ; 

// new 
          if(Mathf.Abs(marioBody.velocity.x)>1.0){
            marioAnimator.SetTrigger("onSkid") ; 
          }
      }

      marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x)) ; 
    

    //   if (Input.GetKeyUp("j")){
    //     CentralManager.centralManagerInstance.consumePowerup(KeyCode.J,this.gameObject);
    //   }

    //   if (Input.GetKeyUp("k")){
    //     CentralManager.centralManagerInstance.consumePowerup(KeyCode.K,this.gameObject);
    //   }

      // adding the restart 
       if (Input.GetKeyDown(KeyCode.R))
         {
             Application.LoadLevel(0);
         }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            //check if a or d is pressed currently
            if (!isADKeyUp)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
            }

            if (!isSpacebarUp && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                
            }
        }
    }
}
