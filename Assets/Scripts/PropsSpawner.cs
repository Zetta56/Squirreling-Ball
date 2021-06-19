using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PropsSpawner : MonoBehaviour
{

    public GameObject table;
    public GameObject cabinet;
    public GameObject pizza;

    public BoxCollider bc;

    Vector3 cubeSize;
    Vector3 cubeCenter;

    // Start is called before the first frame update
    void Start()
    {
        // List<GameObject> prefabs = new List<GameObject>();
        // prefabs.Add(table);
        // prefabs.Add(cabinet);
        // prefabs.Add(pizza);

        GameObject[] prefabs = { pizza, pizza, table, cabinet};
        Transform cubeTrans = bc.GetComponent<Transform>();
        cubeCenter = cubeTrans.position;    

        // Multiply by scale because it does affect the size of the collider
        cubeSize.x = cubeTrans.localScale.x * bc.size.x;
        cubeSize.y = cubeTrans.localScale.y * bc.size.y;
        cubeSize.z = cubeTrans.localScale.z * bc.size.z;
        
        for(int i=0; i<Random.Range(350, 400); i++){
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject go = Instantiate(prefabs[randomIndex], GetRandomPosition(), Quaternion.identity);
            go.transform.parent = this.transform;
        }
    }

    private Vector3 GetRandomPosition()
     {
         // You can also take off half the bounds of the thing you want in the box, so it doesn't extend outside.
         // Right now, the center of the prefab could be right on the extents of the box
         Vector3 randomPosition = new Vector3(Random.Range(-cubeSize.x / 2, cubeSize.x / 2), Random.Range(-cubeSize.y / 2, cubeSize.y / 2), Random.Range(-cubeSize.z / 2, cubeSize.z / 2));
 
         return cubeCenter + randomPosition;
     }
}
