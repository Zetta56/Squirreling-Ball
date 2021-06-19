using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtilLib.Scripts;

public class OptionMenuController : MonoBehaviour
{
    public void ChangeOptions(Slider slider){
        string sliderName = slider.transform.parent.name;
        float mod = slider.value;
        StateController.Set(sliderName, mod);
    }

    public void Reset(){
        Debug.Log("Resetting");
        foreach(Transform section in this.transform.Find("Sections").transform)
        {
            if(section.name.EndsWith("Section")){

                foreach(Transform option in section.transform){
                    
                    Slider optionSlider = option.Find("Slider").GetComponent<Slider>();
                    optionSlider.value = 0.5f;
                    StateController.Set(option.name, 0.5f);

                }

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform section in this.transform.Find("Sections").transform)
        {
            if(section.name.EndsWith("Section")){

                foreach(Transform option in section.transform){
                    
                    Slider optionSlider = option.Find("Slider").GetComponent<Slider>();
                    optionSlider.value = StateController.Get<float>(option.name, 0.5f);

                    optionSlider.onValueChanged.AddListener(
                        delegate {
                            ChangeOptions(optionSlider);
                        }
                    );
                }

            }
        }
    }
}
