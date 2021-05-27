using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScript : MonoBehaviour
{

    /*
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }*/

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("GoodPacket"))
        {
            Debug.Log("GoodPacket collided");
        }
        else if (obj.gameObject.CompareTag("Virus"))
        {
            Debug.Log("Virus collided");
        }
    }
}
