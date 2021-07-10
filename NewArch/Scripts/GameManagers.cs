using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagers : MonoBehaviour
{
    // Start is called before the first frame update

     public Text score ; 
	private  int playerScore =  0;
    public delegate void gameEvent() ; 
    public static event gameEvent OnPlayerDeath ; 
	
	public  void  increaseScore(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();
	}

    public void damagePlayer(){
        OnPlayerDeath() ; 
        
        Time.timeScale = 0.0f ; 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
