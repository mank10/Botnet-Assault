using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientComputer : MonoBehaviour
{
    [SerializeField]private LineRenderer lineConnection;
    [SerializeField]private Transform drawPoint;

    private Transform[] serverPositions;
    private int currentServerPosition;

    [SerializeField] private GameObject goodPacket;
    [SerializeField] private GameObject virusPacket;

    [SerializeField] private GameObject packetToSend = null;
    private bool sendPacket = false;
    private Vector3 defaultPacketPosition;

    public bool isDisabled = false;
    public float sendPacketDelay = 3;

    private bool resetOnce = false;

    private void Awake()
    {
        serverPositions = new Transform[3];
        serverPositions[0] = GameObject.Find("ServerPoint").transform;
        serverPositions[1] = GameObject.Find("ServerPoint (1)").transform;
        serverPositions[2] = GameObject.Find("ServerPoint (2)").transform;
        currentServerPosition = GameManager.Instance.serverPositionNumber;
        lineConnection.SetPosition(0, drawPoint.position);
        lineConnection.SetPosition(1, serverPositions[currentServerPosition].position);
    }


    void Start()
    {
        defaultPacketPosition = goodPacket.transform.position;
    }


    void Update()
    {
        if (!GameUIScript.Instance.isGameStarted) return;

        if (GameUIScript.Instance.isGameOver)
        {
            if (!resetOnce)
                ResetTheGame();
            return;
        }
        else resetOnce = false;

        if(!isDisabled)
        {
            if(sendPacket && packetToSend != null)
            {
                packetToSend.transform.Translate((serverPositions[currentServerPosition].position - defaultPacketPosition) * Time.deltaTime * 0.5f ,Space.World);
            }
            else if(!sendPacket && packetToSend == null)
            {
                StartCoroutine("SendPacketTimer");
                //Debug.Log("Started Timer");
            }
        }
        else if(isDisabled)
        {
            lineConnection.startColor = Color.grey;
            lineConnection.endColor = Color.grey;
            packetToSend = null;
            sendPacket = false;
            ResetPacketPositions();
            StartCoroutine("DisableTimer");
        }
    }

    private void SendPacket()
    {  
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
        sendPacket = true;
    }

    IEnumerator SendPacketTimer()
    {
        packetToSend = Random.Range(0, 2) == 0 ? goodPacket : virusPacket;
        yield return new WaitForSeconds(sendPacketDelay);
        SendPacket();
    }

    IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(1);
        ResetLineConnections();
        isDisabled = false;
    }

    public void PacketReached()
    {
        //Is called by the server after a good or virus packet reached to it and now it has to reset all the parameters to start the clients again.
        sendPacket = false;
        packetToSend = null;
        ResetLineConnections();
        ResetPacketPositions();
    }

    void ResetLineConnections()
    {
        //Helps us reset the color of the line connection back to black that means the connection is active again.
        lineConnection.startColor = Color.black;
        lineConnection.endColor = Color.black;
    }

    void ResetPacketPositions()
    {
        //Resets the position of the packets back to the default position.
        goodPacket.transform.position = defaultPacketPosition;
        virusPacket.transform.position = defaultPacketPosition;
    }

    void ResetTheGame()
    {
        goodPacket.SetActive(false);
        virusPacket.SetActive(false);
        packetToSend = null;
        sendPacket = false;
        ResetLineConnections();
        ResetPacketPositions();
        resetOnce = true;
    }
}
