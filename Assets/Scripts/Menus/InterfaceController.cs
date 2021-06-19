using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtilLib.Scripts;
using TMPro;

public class InterfaceController : MonoBehaviour
{
    UIUtils ui;
    TextMeshProUGUI timer;
    TextMeshProUGUI pinsLeft;
    int numPinsLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI").gameObject.GetComponent<UIUtils>();
        pinsLeft = transform.Find("PinsLeft").gameObject.GetComponent<TextMeshProUGUI>();
        pinsLeft.text = numPinsLeft.ToString() + " pins left";
        timer = transform.Find("Timer/TimerText").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = Utils.StringifyTime(Time.timeSinceLevelLoad);
    }

    public void UpdatePinsLeft(int value) {
        numPinsLeft += value;
        pinsLeft.text = numPinsLeft.ToString() + (numPinsLeft > 1 ? " pins left" : " pin left");
        if(numPinsLeft <= 0) {
            ui.EnableVictory();
        }
    }
}
