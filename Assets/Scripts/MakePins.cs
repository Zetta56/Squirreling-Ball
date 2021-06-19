using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePins : MonoBehaviour 
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public int rows = 3;
    public float rowSpacing = 0.5f;
    public float columnSpacing = 0.5f;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        for(int row = 0; row < rows; row++){
            for(int column = 0; column < row + 1; column++){
                float offset = row * (columnSpacing / 2);
                Vector3 pos = new Vector3(column * columnSpacing - offset, 0, row * rowSpacing);
                GameObject pin = Instantiate(myPrefab, pos + this.transform.position, Quaternion.identity);
                pin.tag = "Pin";
                pin.transform.parent = this.transform;
            }
        }

        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y*2, 0);
    }
}
