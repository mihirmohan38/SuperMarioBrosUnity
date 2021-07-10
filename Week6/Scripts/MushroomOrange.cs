using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomOrange : MonoBehaviour, ConsumableInterface
{
    // Start is called before the first frame update

    public  Texture t;
    private int index = 0 ; 


    public GameObjectConstants constants ;  
	public  void  consumedBy(GameObject player){
		// give player jump boost
		constants.playerMaxSpeed *=  2;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator  removeEffect(GameObject player){
		yield  return  new  WaitForSeconds(5.0f);
		
		constants.playerMaxSpeed  /=  2;
	}

    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            // update UI
            CentralManager.centralManagerInstance.addPowerup(t, index, this);
            GetComponent<Collider2D>().enabled  =  false;
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

