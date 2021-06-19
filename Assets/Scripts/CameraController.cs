using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilLib.Scripts;

public class CameraController : MonoBehaviour
{
    public float zoomSensitivity = 0.1f;
    public float mouseSensitivity = 0.01f;
    public int CameraYRotation = 0;
    float zoom = 3f;
    float mouseX = 0;
    float mouseY = 0;
    GameObject player;
    Camera mycam;
    Vector3 offset;

    void Start()
    {
        player = GameObject.Find("Player"); // Gets the GameObject named "Player" in hierarchy
        mycam = GetComponent<Camera>();
        offset = new Vector3(0, 2f, -zoom);
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 144;
        QualitySettings.vSyncCount = 1;
    }

    void Update() {
      mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
      mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity;
      LimitRotation();
      // Possible Vertical Rotation: (-90, 90) or 180 degrees total 
      // Possible Horizontal Rotation: (-180, 180) or 360 degrees total
      transform.rotation = Quaternion.Euler(new Vector4(-mouseY * 180f, mouseX * 360f + CameraYRotation, 
        this.transform.rotation.z));

      // Zooming
      if(zoom - Input.mouseScrollDelta.y * zoomSensitivity > 0 && 
         zoom - Input.mouseScrollDelta.y * zoomSensitivity < 10
      ) {
        zoom -= Input.mouseScrollDelta.y * zoomSensitivity;
      }
      
      // Positioning camera behind player
      offset.x = -transform.forward.x * zoom;
      offset.z = -transform.forward.z * zoom; 
      LimitPosition();
      transform.position = player.transform.position + offset;
    }

    void LimitRotation() {
      if (mouseX < -0.5f) { 
        mouseX = 0.5f; 
      } else if (mouseX > 0.5f) { 
        mouseX = -0.5f; 
      }
      if (mouseY < -0.5f) { 
        mouseY = -0.5f; 
      } else if (mouseY > 0.5f) { 
        mouseY = 0.5f; 
      }
    }

    void LimitPosition() {
      int i = 0;
      while (i < 1000 && Physics.Raycast(player.transform.position, transform.position - player.transform.position, offset.magnitude+0.1f, 1 << 3)) {
        offset = Vector3.MoveTowards(offset, new Vector3(0, offset.y, 0), .1f);
        transform.position = player.transform.position + offset;
        i++;
        }
    }

    // public float mouseSensitivity = 5f;
    // public float zoomSensitivity = 0.1f;
    //
    // float zoom = 2f;
    // GameObject player;
    // float rotateX;
    // float rotateY;
    // Vector3 rotation;
    // Vector3 offset;
    //
    // // Start is called before the first frame update
    // void Start()
    // {
    //     rotation = new Vector3(0, 10, 0);
    //     offset = new Vector3(zoom, 2f, zoom);
    //     // Gets the GameObject named "Player" in current scene
    //     player = GameObject.Find("Player");
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     // Camera zoom
    //     if(zoom - Input.mouseScrollDelta.y * zoomSensitivity > 0 &&
    //        zoom - Input.mouseScrollDelta.y * zoomSensitivity < 10
    //     ) {
    //         zoom -= Input.mouseScrollDelta.y * zoomSensitivity;
    //     }
    //
    //     // Puts camera behind cube
    //     offset.x = -transform.forward.x * zoom;
    //     offset.z = -transform.forward.z * zoom;
    //     this.transform.position = player.transform.position + offset;
    //
    //     // Rotating camera
    //     rotateX = Input.GetAxis("Mouse Y");
    //     rotateY = Input.GetAxis("Mouse X");
    //     rotation = new Vector3(-rotateX, rotateY, 0); // Negative rotation is clockwise around axis
    //     this.transform.eulerAngles += rotation * mouseSensitivity; // Rotates around world axis
    // }
}
