using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBlue : MonoBehaviour, ConsumableInterface
{
    // Start is called before the first frame update
    public  Texture t;
    private int index = 1 ; 

    public GameObjectConstants constants ;  
	public  void  consumedBy(GameObject player){
		// give player jump boost

		constants.playerMaxJumpSpeed += 10;
        Debug.Log("What the heck is happening here") ; 
		StartCoroutine(removeEffect());
	}

	IEnumerator  removeEffect(){

        Debug.Log("remove effect called") ; 
		yield  return  new  WaitForSeconds(5.0f);
		constants.playerMaxJumpSpeed -=  10;

	}
    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            // update UI
            CentralManager.centralManagerInstance.addPowerup(t, index, this);
            this.gameObject.SetActive(false);
         //   GetComponent<Collider2D>().enabled  =  false;
            
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
