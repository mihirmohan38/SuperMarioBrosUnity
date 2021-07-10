using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : Singleton<GameUI>  
{
 
    override  public  void  Awake(){
		base.Awake();
        Time.timeScale = 0.0f ; 
		Debug.Log("awake called");
		// other instructions...
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
