using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject clientPrefab;
    public Transform defaultSpawnPosition;
    public int serverPositionNumber = 0;

    private Camera mainCamera;
    private Vector3 cameraMoveOffset;

    private int lastVirusCount = 0;
    private int countDifference = 0;
    private int noOfClient = 3;
    [SerializeField]private int noOfRows = 1;

    [SerializeField]private int currentRowTotalCount = 3;   //This var keeps the track of the total clients to be spawned in a the current row.
    [SerializeField]private int currentRowSpawnClients = 3; //This var keeps the track of the clients already spawned in the current row.

    private Vector3 standardOffset;
    private Vector3 specialOffset;
    private Vector3 offset;
    int i = 1, j = 1;

    #region Singleton
    public static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = (GameManager)FindObjectOfType(typeof(GameManager));
            }

            return _Instance;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        standardOffset = new Vector3(1.3f, 0, 0);
        specialOffset = new Vector3(0.7f, 0, 0);
        cameraMoveOffset = new Vector3(0, 1, -1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (noOfClient == 12) return;

        countDifference = GameUIScript.Instance.virusTerminatedCount - lastVirusCount;
        if(countDifference == noOfClient * 3) //noOfClient * 3
        {
            if(currentRowTotalCount == currentRowSpawnClients)
            {
                currentRowTotalCount++;       //We go to the next row in the tree.
                currentRowSpawnClients = 0;
                //Increament z axis.
                defaultSpawnPosition.position += new Vector3(0, 0, -1.5f);
                i = 1; j = 1;
                //Move Camera
                mainCamera.transform.position += cameraMoveOffset * noOfRows;
                if(noOfRows != 1)
                    serverPositionNumber++;
                noOfRows++;
            }

            lastVirusCount = GameUIScript.Instance.virusTerminatedCount;

            if (currentRowTotalCount % 2 == 0)
            {
                offset = specialOffset;  // 4 clients in row.
                if (currentRowSpawnClients < currentRowTotalCount / 2)
                {
                    Instantiate(clientPrefab, defaultSpawnPosition.position - i * offset, Quaternion.identity);
                    i++;
                }
                else
                {
                    Instantiate(clientPrefab, defaultSpawnPosition.position + j * offset, Quaternion.identity);
                    j++;
                }
               
                currentRowSpawnClients++;
            }
            else
            {
                offset = standardOffset;  // 5 clients in row.
                if(Mathf.Ceil(currentRowTotalCount / 2) == currentRowSpawnClients)
                {
                    Instantiate(clientPrefab, defaultSpawnPosition.position, Quaternion.identity);
                }
                else if(currentRowSpawnClients < Mathf.Ceil(currentRowTotalCount / 2))
                {
                    Instantiate(clientPrefab, defaultSpawnPosition.position - i * offset, Quaternion.identity);
                    i++;
                }
                else if(currentRowSpawnClients > Mathf.Ceil(currentRowTotalCount / 2))
                {
                    Instantiate(clientPrefab, defaultSpawnPosition.position + j * offset, Quaternion.identity);
                    j++;
                }
                currentRowSpawnClients++;
            }
            noOfClient++;

            
        }
    }
}
