using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
   public void PlayButton(){
    SceneManager.LoadScene("Level_1");
    Time.timeScale = 1;

   }

   public void ExitButton(){
    Application.Quit();
   }
}
