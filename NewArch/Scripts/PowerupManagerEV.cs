using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    ORANGEMUSHROOM = 0,
    BLUEMUSHROOM = 1
}
public class PowerupManagerEV : MonoBehaviour
{
  public IntVariable marioJumpSpeed;
  public IntVariable marioMaxSpeed;
  public PowerupInventory powerupInventory;
  public List<GameObject> powerupIcons;

  void Start()
  {
      if (!powerupInventory.gameStarted)
      {
          powerupInventory.gameStarted = true;
          powerupInventory.Setup(powerupIcons.Count);
          resetPowerup();
      }
      else
      {
          // re-render the contents of the powerup from the previous time
          for (int i = 0; i < powerupInventory.Items.Count; i++)
          {
              Powerup p = powerupInventory.Get(i);
              if (p != null)
              {
                  AddPowerupUI(i, p.powerupTexture);
              }
          }
      }
  }
    
  public void resetPowerup()
  {
      for (int i = 0; i < powerupIcons.Count; i++)
      {
          powerupIcons[i].SetActive(false);
      }
  }
    
  void AddPowerupUI(int index, Texture t)
  {
      powerupIcons[index].GetComponent<RawImage>().texture = t;
      powerupIcons[index].SetActive(true);
  }

  public void AddPowerup(Powerup p)
  {
      powerupInventory.Add(p, (int)p.index);
      AddPowerupUI((int)p.index, p.powerupTexture);
  }

  public void AttemptConsumePowerup(KeyCode k) {
      if (k==KeyCode.J){
          // check if inventory is empty 
        Powerup p = powerupInventory.Get(0);
           //if not empty cast powerup 
        if (p != null)
        {
           int duration = p.duration ; 
           List<int> powerups = p.Utilise() ; 
           StartCoroutine(castPowerup(powerups, duration));
           // remove powerup
           powerupInventory.Remove(0) ; 
        } 
      }
      if (k==KeyCode.K){

        Powerup p = powerupInventory.Get(1);
        if (p != null)
        {
           int duration = p.duration ; 
           List<int> powerups = p.Utilise() ; 
           StartCoroutine(castPowerup(powerups, duration));
           // remove powerup
           powerupInventory.Remove(1) ; 
        } 
      }
  }

  IEnumerator castPowerup(List<int> powerups, int duration){
      marioMaxSpeed.ApplyChange(powerups[0]) ; 
      marioJumpSpeed.ApplyChange(powerups[1]) ; 
      Debug.Log("powers increased") ; 
      yield  return  new  WaitForSeconds((float)duration);
      marioMaxSpeed.ApplyChange(-powerups[0]) ; 
      marioJumpSpeed.ApplyChange(-powerups[1]) ; 
      Debug.Log("powers decreased") ; 
  }

  public void OnApplicationQuit()
  {
     // ResetValues();
  }
 
}