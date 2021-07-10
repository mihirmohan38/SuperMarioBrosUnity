using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject debris ; 
    public GameObject coin ; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")){
            // another way could be to just destroy the whole fucking thing 
             
            Debug.Log("where the heck are you") ; 
            for (int x =  0; x<5; x++){
			Instantiate(debris, transform.position, Quaternion.identity);
		}
            // debris.SetActive(true) ; 
            // coin.SetActive(true) ; 
            gameObject.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().enabled  =  false;
            gameObject.transform.parent.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled  =  false;
            gameObject.transform.parent.gameObject.transform.GetChild(1).GetComponent<EdgeCollider2D>().enabled  =  false;
            GetComponent<AudioSource>().Play() ; 
        }
    }
}
