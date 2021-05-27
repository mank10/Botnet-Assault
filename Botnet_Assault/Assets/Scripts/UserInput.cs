using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touchInput = Input.GetTouch(0);
            Debug.Log("Touch Input");

            Ray camRay = mainCamera.ScreenPointToRay(touchInput.position);
            RaycastHit hitInfo;

            if(Physics.Raycast(camRay, out hitInfo))
            {
                if(hitInfo.transform.CompareTag("GoodPacket"))
                {
                    //Do Something...
                    Debug.Log("GoodPacket");
                }
                else if(hitInfo.transform.CompareTag("Virus"))
                {
                    //Do Something...
                    Debug.Log("Virus");
                }
            }
        }

    }
}
