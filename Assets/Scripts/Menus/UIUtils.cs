using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UtilLib.Scripts;
using TMPro;

public class UIUtils : MonoBehaviour
{
    GameObject game_interface;
    GameObject victory;
    GameObject pause;
    AudioSource musicPlayer;
    bool paused;
    public float timeFinished = 0f;
    TextMeshProUGUI victoryMessage;
    
    void Start()
    {
        StateController.Set("currentLevel", SceneManager.GetActiveScene().name);
        game_interface = transform.Find("Interface").gameObject;
        victory = transform.Find("Victory").gameObject;
        victoryMessage = transform.Find("Victory/Message").GetComponent<TextMeshProUGUI>();
        pause = transform.Find("Pause").gameObject;
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.volume = StateController.Get<float>("Music", 0.5f)*0.2f;
        
        EnableInterface();
    }
    
    public void DisableAll() {
        game_interface.SetActive(false);
        victory.SetActive(false);
        pause.SetActive(false);
    }
    
    public void EnableInterface() {
        DisableAll();
        game_interface.SetActive(true);
    }
    
    public void EnableVictory() {
        DisableAll();
        victory.SetActive(true);
        victoryMessage.text = "You Win!";
        timeFinished = Time.timeSinceLevelLoad;
        
        if (timeFinished < StateController.Get<float>($"bestTime_{StateController.Get<string>("currentLevel")}", float.MaxValue)) {
            StateController.Set($"bestTime_{StateController.Get<string>("currentLevel")}", timeFinished);
            victoryMessage.text = "New Highscore!";

        }
        StateController.Set("highestLevel", SceneManager.GetActiveScene().name);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PauseTime(bool should_pause) {
        if (should_pause) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }

    public void TogglePaused() {
        if(victory.activeSelf) {
            return;
        }
        // Check if pause menu is active
        if(pause.activeSelf) {
            pause.SetActive(false);
            EnableInterface();
            PauseTime(false);
            musicPlayer.volume = StateController.Get<float>("Music", 0.5f)*0.2f;
            Cursor.lockState = CursorLockMode.Locked;
		    Cursor.visible = false;
        } else {
            pause.SetActive(true);
            game_interface.SetActive(false);
            PauseTime(true);
            musicPlayer.volume = StateController.Get<float>("Music", 0.5f)*0.05f;
            Cursor.lockState = CursorLockMode.None;
		    Cursor.visible = true;
        }
    }
}