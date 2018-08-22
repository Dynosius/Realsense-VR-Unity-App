using System;
using UnityEngine;

public class RealsenseController : MonoBehaviour
{
    private TcpSocket clientSocket;
    #region gameObjects
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public GameObject head;
    public GameObject middleBody;
    public Transform camera;
    #endregion
    private const float coordZ = -9f;
    private const float divisor = 70f;
    Vector3[] gameobjectVectors = new Vector3[8];
    private Vector3 characterVector;
    private Transform playerCharacter;
    private float startingPosZ, startingPosX;
    private float offsetZ;
    private float offsetX;
    private bool IsMessageReceived = false;
    // Use this for initialization
    void Awake()
    {
        clientSocket = new TcpSocket("127.0.0.1", 54000);
        clientSocket.MessageReceived += ClientSocket_MessageReceived;
        // Putting the character position in a vector so we can manipulate its position later on
        playerCharacter = GameObject.Find("PlayerCharacter").transform;
        characterVector = playerCharacter.position;
        // Initializing vectors that will be changed through coordinates we'll be receiving
        #region gameobjectVectors initialization
        gameobjectVectors[0] = new Vector3(5f, 0f, coordZ);     //0 - leftShoulder
        gameobjectVectors[1] = new Vector3(5f, 0f, coordZ);     //1 - rightShoulder
        gameobjectVectors[2] = new Vector3(5f, 0f, coordZ);     //2 - leftHand
        gameobjectVectors[3] = new Vector3(5f, 0f, coordZ);     //3 - rightHand
        gameobjectVectors[4] = new Vector3(5f, 0f, coordZ);     //4 - head
        gameobjectVectors[5] = new Vector3(5f, 0f, coordZ);     //5 - midBody
        #endregion

        // For using VR as Z buffer offset

        startingPosX = characterVector.x;
        startingPosZ = characterVector.z;
    }

    private void ClientSocket_MessageReceived(string message, long counter)
    {
        IsMessageReceived = true;
        ReformatMessage(message);
        Debug.Log(message);
        Debug.Log(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offsetZ = startingPosZ - camera.position.z;
        offsetX = startingPosX - camera.position.x;
        characterVector.x = camera.position.x;
        characterVector.z = camera.position.z - .3f;
        playerCharacter.position = Vector3.Lerp(playerCharacter.position, characterVector, Time.deltaTime * 5f);
        if (IsMessageReceived)
        {

            leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, gameobjectVectors[2], Time.deltaTime * 5f);
            rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, gameobjectVectors[3], Time.deltaTime * 5f);
        }


        // move character to where spine is on the x axis
        if (gameobjectVectors[5][0] > 0f || gameobjectVectors[5][0] < 10f)
        {
            //characterVector.x = gameobjectVectors[5][0];
            //

        }




    }

    private void ReformatMessage(string message)
    {
        float x, y;
        string[] perBodyparts = message.Split(' ');
        string[][] wholeMessage = new string[perBodyparts.Length - 1][];
        for (int i = 0; i < perBodyparts.Length - 1; i++)   // Splitting message into parts, and changing vector coordinates
        {
            wholeMessage[i] = perBodyparts[i].Split(';');
            if (i <= 5)
            {
                x = float.Parse(wholeMessage[i][1]);
                x = (x / divisor) + 1f;
                y = float.Parse(wholeMessage[i][2]);
                y = 10f - (y / divisor);
                Debug.Log(wholeMessage[i][0] + ";" + x + ";" + y + ";");

                gameobjectVectors[i].x = x;
                gameobjectVectors[i].y = y;
            }
            else
            {
                Debug.Log(wholeMessage[i][0]);
            }

        }
    }
}
