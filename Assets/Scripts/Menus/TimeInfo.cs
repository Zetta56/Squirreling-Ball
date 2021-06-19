using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilLib.Scripts;
using TMPro;

public class TimeInfo : MonoBehaviour
{
    TextMeshProUGUI textField;
    UIUtils ui;

    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        ui = GameObject.Find("UI").GetComponent<UIUtils>();
    }

    // Update is called once per frame
    void Update()
    {
        float best = StateController.Get<float>($"bestTime_{StateController.Get<string>("currentLevel")}", -1f);
        string bestString = best >= 0 ? Utils.StringifyTime(best) : "-";
        textField.text = $"Best Time: {bestString}\nCurrent Time: {Utils.StringifyTime(ui.timeFinished)}";
    }
}
