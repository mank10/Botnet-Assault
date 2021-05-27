using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientComputer : MonoBehaviour
{
    [SerializeField]private LineRenderer lineConnection;
    [SerializeField]private Transform drawPoint;
    [SerializeField]private Transform serverPos;

    [SerializeField] private GameObject goodPacket;
    [SerializeField] private GameObject virusPacket;

    private GameObject packetToSend;
    private Vector3 defaultPacketPosition;

    private void Awake()
    {
        lineConnection.SetPosition(0, drawPoint.position);
        lineConnection.SetPosition(1, serverPos.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        defaultPacketPosition = goodPacket.transform.position;
        Invoke("SendPacket", 3);
    }

    // Update is called once per frame
    void Update()
    {  
        if(packetToSend != null)
        {
            packetToSend.transform.Translate((serverPos.position - defaultPacketPosition) * Time.deltaTime * 0.5f ,Space.World);
        }
    }

    private void SendPacket()
    {
        packetToSend = Random.Range(0, 2) == 0 ? goodPacket : virusPacket;
        if (packetToSend == goodPacket)
        {
            goodPacket.SetActive(true);
            lineConnection.startColor = Color.green;
            lineConnection.endColor = Color.green;
        }
        else
        {
            virusPacket.SetActive(true);
            lineConnection.startColor = Color.red;
            lineConnection.endColor = Color.red;
        }
    }
}
