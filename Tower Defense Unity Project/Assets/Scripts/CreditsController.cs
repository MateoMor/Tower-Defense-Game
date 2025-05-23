using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    
    public string mainMenuScene = "MainMenu"; 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadMainMenu();
        }
    }
    
    public void LoadMainMenu()
    {
        
        if (Application.CanStreamedLevelBeLoaded(mainMenuScene))
        {
            SceneManager.LoadScene(mainMenuScene);
        }
        else
        {
            Debug.LogError($"No se encontró la escena: {mainMenuScene}. " +
                          "Verifica el nombre y que esté agregada en Build Settings.");
        }
    }
}