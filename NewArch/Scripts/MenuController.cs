using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour 
{
    // Start is called before the first frame update

    void Awake(){
        Time.timeScale = 0.0f ; 
    }

    public void startButtonClicked(){
        foreach (Transform eachChild in transform){
            if (eachChild.name != "Score" && eachChild.name!="Powerups" && eachChild.name!="PowerupSlot1" && eachChild.name!="PowerupSlot2"){
                Debug.Log("Child found. Name : " + eachChild.name) ; 
                eachChild.gameObject.SetActive(false) ; 
                Time.timeScale = 1.0f ; 
            }
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
