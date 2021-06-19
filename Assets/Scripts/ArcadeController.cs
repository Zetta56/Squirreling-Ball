using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ArcadeController : MonoBehaviour
{

    private KeyCode[] sequence = new KeyCode[]{
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A,

    };
    private int sequenceIndex = 0;
    public bool is_in = false;
    public bool is_done = false;
    private bool failed = false;
    private Coroutine lastRoutine = null;
    
    public TextMeshPro cabinetText;

    void Start(){
        cabinetText = cabinetText.GetComponent<TextMeshPro>();
        cabinetText.color = Color.white;
        cabinetText.text="Konami";
        cabinetText.fontSize=3;
    }

    void Update(){
        if(is_in && !is_done){
            if (Input.GetKeyDown(sequence[sequenceIndex])) {
                if(failed){
                    StopCoroutine (lastRoutine);
                    failed=false;
                    cabinetText.fontSize=3;
                }
                
                
                cabinetText.color = Color.green;
                cabinetText.text = '\u25A1'.ToString();//sequence[sequenceIndex].ToString();
                cabinetText.fontSize+=1.35f;
                if (++sequenceIndex == sequence.Length){
                    sequenceIndex = 0;
                    cabinetText.fontSize=3;
                    cabinetText.text = "+1 Jump";
                    is_done = true;
                }
            } else if (Input.anyKeyDown){
                cabinetText.text = "X";
                cabinetText.color = Color.red;
                cabinetText.fontSize=3;
                lastRoutine = StartCoroutine (WaitForOff());
                sequenceIndex = 0;
            } 
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.name=="Player"){
            is_in = true;
        }
        
    }
    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.name=="Player"){
            is_in = false;
        }
        
    }

    private IEnumerator WaitForOff()
    {
        failed = true;
        yield return new WaitForSeconds(1.5f);
        cabinetText.color = Color.white;
        cabinetText.text="Konami";
        cabinetText.fontSize=3;
        failed = false;
    }
}
