using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
  private float originalX;
  private float maxOffset = 5.0f;
  private float enemyPatroltime = 2.0f;
  private int moveRight = -1;
  private Vector2 velocity;

  private Rigidbody2D enemyBody;

  void Start()
  {
      enemyBody = GetComponent<Rigidbody2D>();
      // get the starting position
      originalX = transform.position.x;
      ComputeVelocity();
      
  }
  void ComputeVelocity(){
      velocity = new Vector2((moveRight)*maxOffset / enemyPatroltime, 0);
      //Debug.Log(velocity) ; 
  }
  void MoveGomba(){
      enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
  }

    void FixedUpdate(){

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX)< maxOffset) {
            MoveGomba() ; 
        } else {
            moveRight *= -1  ; 
            ComputeVelocity() ; 
            MoveGomba() ; 
        }
        
    }
}


