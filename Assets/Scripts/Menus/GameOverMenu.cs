using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    private Scene scene;

    public void Setup(){
        gameObject.SetActive(true);
    }
   public void RestartButton(){
    scene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(scene.name);
    Time.timeScale = 1;

   }

   public void MainMenuButton(){

   }
}
