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
            //Debug.Log("Touch Input");

            Ray camRay = mainCamera.ScreenPointToRay(touchInput.position);
            RaycastHit hitInfo;

            if(Physics.Raycast(camRay, out hitInfo))
            {
                if(hitInfo.transform.CompareTag("Virus"))
                {
                    //Disable the virus packet and inform Client to disable itself and line connection.
                    hitInfo.transform.parent.GetComponentInChildren<ClientComputer>().isDisabled = true;
                    hitInfo.transform.gameObject.SetActive(false);
                    GameUIScript.Instance.UpdateVirusTerminated();
                    //Debug.Log("Virus touch");
                }
            }
        }

    }
}
