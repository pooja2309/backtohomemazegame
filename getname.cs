using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class getname : MonoBehaviour
{

   public static string playernamestr;
   public static string playerscores;
   public Text playername;
   public Text playerscore;
   void Start(){
       playername.text=playernamestr;
       playerscore.text=playerscores;
    }
    
    public void BackHome(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

}
