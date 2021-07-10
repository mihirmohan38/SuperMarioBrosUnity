using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusroomOrangeController : MonoBehaviour
{
    // Start is called before the first frame update
  //private float originalX;
  //private float maxOffset = 5.0f;
  //private float enemyPatroltime = 2.0f;
  //private int moveRight = -1;
  private Vector2 velocity;
  public float speed = 4.0f ; 
  private float direction = 1.0f ; 
  private float moving = 1.0f ; 

  private Rigidbody2D mushroomBody;

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Obstacle")){
            if(direction==1.0f){
                direction=-1.0f ; 
            }else if (direction==-1.0f){
                direction=1.0f ; 
            }
            ComputeVelocity() ; 
        }

        if (col.gameObject.CompareTag("Player")){
            moving = 0.0f ; 
            ComputeVelocity() ; 
        }
    }

    void moveMushroom(){
        mushroomBody.MovePosition(mushroomBody.position + velocity * Time.fixedDeltaTime);
    }
    void Start()
    {
      mushroomBody = GetComponent<Rigidbody2D>();
      mushroomBody.AddForce(Vector2.up * 20, ForceMode2D.Impulse) ; 
      // get the starting position
      //originalX = transform.position.x;
      ComputeVelocity() ; 
      
        
    }

    // Update is called once per frame
    void Update()
    {
        moveMushroom() ; 
    }

    void ComputeVelocity(){
        //velocity = new Vector2((moveRight)*maxOffset / enemyPatroltime, 0);
        velocity = new Vector2((moving*speed*direction),0) ; 

    }

    void OnBecameInvisible(){
        Debug.Log("here") ; 
        Destroy(gameObject) ; 

    }
}
