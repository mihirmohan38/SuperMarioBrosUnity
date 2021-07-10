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

  public GameObjectConstants gameConstants ; 

  private Rigidbody2D enemyBody;

  public Spawner spawner ; 

  void Start()
  {
      enemyBody = GetComponent<Rigidbody2D>();
      // get the starting position
      originalX = this.transform.position.x;
      ComputeVelocity();

      GameManagers.OnPlayerDeath += EnemyRejoice ; 
      
  }
  void EnemyRejoice(){
      Debug.Log("enemy killed Mario") ; 
      // enter animation 
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

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Collision detected") ; 
        if (other.gameObject.tag == "Player"){
            Debug.Log("collision wiht player") ; 
            float yOffset = (other.transform.position.y - this.transform.position.y) ; 
            if (yOffset>0.75f){
                KillSelf() ; 
            } else {
                // hurt player 
                CentralManager.centralManagerInstance.damagePlayer() ; 
            }
        }
    }

    void KillSelf(){
        CentralManager.centralManagerInstance.increaseScore() ; 
        StartCoroutine(flatten()) ; 
        Debug.Log("kill sequence ends") ; 
    }

    IEnumerator flatten() {
        Debug.Log("flatten starts") ; 
        int steps = 5 ; 
        float stepper = 1.0f/(float) steps ; 
        for (int i =  0; i  <  steps; i  ++){
			this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position  =  new  Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield  return  null;
		}
		Debug.Log("Flatten ends");
		this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
        
        Spawner.SpawnerInstance.spawnNewEnemy() ; 
		yield  break;
    }
}


