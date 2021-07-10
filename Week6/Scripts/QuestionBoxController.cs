using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{

    public  Rigidbody2D rigidBody;
    public  SpringJoint2D springJoint;
    public  GameObject consummablePrefab; // the spawned mushroom prefab
    public  SpriteRenderer spriteRenderer;
    public  Sprite usedQuestionBox; // the sprite that indicates empty box instead of a question mark
    private bool hit =  false;

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Player") && !hit){
            hit=true ; 
            rigidBody.AddForce(new  Vector2(0, rigidBody.mass*20), ForceMode2D.Impulse);
            Instantiate(consummablePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity) ; 
            StartCoroutine(DisableBox()) ; 
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool ObjectMovedAndStopped(){
        return Mathf.Abs(rigidBody.velocity.magnitude)<0.01f ; 
    }
    IEnumerator DisableBox(){
        if(!ObjectMovedAndStopped()){
            yield return new WaitUntil(()=> ObjectMovedAndStopped()) ; 
        }

        // continues here 
        spriteRenderer.sprite = usedQuestionBox ; 
        rigidBody.bodyType  =  RigidbodyType2D.Static; 

        // reset the location 
        this.transform.localPosition  =  Vector3.zero;
	    springJoint.enabled  =  false;

    }

}
