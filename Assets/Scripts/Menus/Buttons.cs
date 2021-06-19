using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UtilLib.Scripts;

public class Buttons : MonoBehaviour
{
    public void LoadNextLevel() {
        Time.timeScale = 1;
        if(SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene("Menu");
            return;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadCurrentLevel(){
        Time.timeScale = 1;
		string scene = StateController.Get<string>("currentLevel", "Tutorial");
		SceneManager.LoadScene(scene);
	}

    public void LoadMenu(){
        Time.timeScale = 1;
		SceneManager.LoadScene("Menu");
	}

    public void Quit(){
        Debug.Log("You just quit. You are officially a quitter. How does that feel?");
        Application.Quit();
    }
}
