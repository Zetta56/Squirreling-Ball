using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPrefabController : MonoBehaviour
{
    public string m_name;
    // Start is called before the first frame update
    public void PlayLevel()
    {
        SceneManager.LoadScene(m_name);
    }
}
