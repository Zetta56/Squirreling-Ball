using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UtilLib.Scripts;
using TMPro;

public class PlayMenuController : MonoBehaviour
{
    public GameObject LevelPrefab;
    TextMeshProUGUI NameField;
    TextMeshProUGUI BestTimeField;

    // Start is called before the first frame update
    void Start()
    {
        int level = 0;
        // we should definitely find a way to not hardcode these
        for(float y = 125; y>-76; y-=150){
            for(float x = -250; x<251; x+=125){
                string path = SceneUtility.GetScenePathByBuildIndex(level + 3);
                if (path == "") break;
                string LevelName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
                Vector3 pos = new Vector3(x, y, 0);
                GameObject NewLevel = Instantiate(LevelPrefab, pos, Quaternion.identity);
                NewLevel.transform.SetParent(this.transform, false);

                NewLevel.GetComponent<LevelPrefabController>().m_name = LevelName;

                NameField = NewLevel.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                NameField.text = LevelName;

                BestTimeField = NewLevel.transform.Find("BestTime").GetComponent<TextMeshProUGUI>();
                float bestTime = StateController.Get<float>($"bestTime_{LevelName}", -1f);
                BestTimeField.text = bestTime >= 0 ? Utils.StringifyTime(bestTime) : "-";

                level++;
            }
        }
        
    }
}
