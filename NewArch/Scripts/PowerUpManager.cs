using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    // Start is called before the first frame update
                
    public  List<GameObject> powerupIcons;
	private  List<ConsumableInterface> powerups;

	// Start is called before the first frame update
	void  Start()
	{
		powerups  =  new  List<ConsumableInterface>();
		for (int i =  0; i<powerupIcons.Count; i++){
			powerupIcons[i].SetActive(false);
			powerups.Add(null);
		}
	}        

    public  void  addPowerup(Texture texture, int index, ConsumableInterface i){
        Debug.Log("adding powerup");
        if (index  <  powerupIcons.Count){
            powerupIcons[index].GetComponent<RawImage>().texture  =  texture;
            powerupIcons[index].SetActive(true);
            powerups[index] =  i;
        }
        for (int j=0 ; j<2 ; j++){
            Debug.Log(powerups[j]) ;
            
        } 
}

    public  void  removePowerup(int index){
        if (index  <  powerupIcons.Count){
        powerupIcons[index].SetActive(false);
        powerups[index] =  null;
        }
    }

    void  cast(int i, GameObject p){
	if (powerups[i] !=  null){
        Debug.Log("here consuming") ; 
        Debug.Log(powerups[i]) ; 
		powerups[i].consumedBy(p); // interface method
		removePowerup(i);
	}
}

    public  void  consumePowerup(KeyCode k, GameObject player){
        switch(k){
            case  KeyCode.J:
                cast(0, player);
                break;
            case  KeyCode.K:
                cast(1, player);
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
