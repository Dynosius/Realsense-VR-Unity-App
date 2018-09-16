using System;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    private TcpSocket clientSocket;
    #region gameObjects
    public Transform head;
    public Transform ShoulderSpine;
    public Transform LeftShoulder;
    public Transform LeftElbow;
    public Transform LeftHand;
    public Transform RightShoulder;
    public Transform RightElbow;
    public Transform RightHand;
    public Transform MidSpine;
    public Transform BaseSpine;
    public Transform LeftHip;
    public Transform LeftKnee;
    public Transform LeftFoot;
    public Transform RightHip;
    public Transform RightKnee;
    public Transform RightFoot;
    public Transform LeftWrist;
    public Transform RightWrist;
    public Transform Neck;
    #endregion
    public float divisorX = 350f, divisorY = 400f, divisorZ = 300f;
    public float offsetX = 0f, offsetY = 2.3f, offsetZ = 8f;
    Vector3[] gameobjectVectors = new Vector3[19];
    private Vector3 characterVector;
    private Transform playerCharacter;
    // Use this for initialization
    void Awake()
    {
        clientSocket = new TcpSocket("127.0.0.1", 54000);
        clientSocket.MessageReceived += ClientSocket_MessageReceived;
        // Putting the character localPosition in a vector so we can manipulate its localPosition later on
        playerCharacter = GameObject.Find("PlayerCharacter").transform;
        characterVector = playerCharacter.position;
        // Initializing vectors that will be changed through coordinates we'll be receiving
        #region gameobjectVectors initialization
        gameobjectVectors[0] = Vector3.zero;    //0 - head
        gameobjectVectors[1] = Vector3.zero;     //1 - shoulderSpine
        gameobjectVectors[2] = Vector3.zero;     //2 - leftShoulder
        gameobjectVectors[3] = Vector3.zero;     //3 - leftElbow
        gameobjectVectors[4] = Vector3.zero;     //4 - leftHand
        gameobjectVectors[5] = Vector3.zero;     //5 - rightShoulder
        gameobjectVectors[6] = Vector3.zero;     //6 - rightElbow
        gameobjectVectors[7] = Vector3.zero;     //7 - rightHand
        gameobjectVectors[8] = Vector3.zero;     //8 - midSpine
        gameobjectVectors[9] = Vector3.zero;     //9 - baseSpine
        gameobjectVectors[10] = Vector3.zero;     //10 - leftHip
        gameobjectVectors[11] = Vector3.zero;     //11 - leftKnee
        gameobjectVectors[12] = Vector3.zero;     //12 - leftFoot
        gameobjectVectors[13] = Vector3.zero;     //13 - rightHip
        gameobjectVectors[14] = Vector3.zero;     //14 - rightKnee
        gameobjectVectors[15] = Vector3.zero;     //15 - rightFoot
        gameobjectVectors[16] = Vector3.zero;     //16 - leftWrist
        gameobjectVectors[17] = Vector3.zero;     //17 - rightWrist
        gameobjectVectors[18] = Vector3.zero;     //18 - neck
        #endregion

        // For using VR as Z buffer offset

        //startingPosX = characterVector.x;
        //startingPosZ = characterVector.z;
    }

    private void ClientSocket_MessageReceived(string message, long counter)
    {
        ReformatMessage(message);
        //Debug.Log("Message is: " + message);
        //Debug.Log(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        head.localPosition = gameobjectVectors[0];
        ShoulderSpine.localPosition = gameobjectVectors[1];
        LeftShoulder.localPosition = gameobjectVectors[2];
        LeftElbow.localPosition = gameobjectVectors[3];
        LeftHand.localPosition = gameobjectVectors[4];
        RightShoulder.localPosition = gameobjectVectors[5];
        RightElbow.localPosition = gameobjectVectors[6];
        RightHand.localPosition = gameobjectVectors[7];
        MidSpine.localPosition = gameobjectVectors[8];
        BaseSpine.localPosition = gameobjectVectors[9];
        LeftHip.localPosition = gameobjectVectors[10];
        LeftKnee.localPosition = gameobjectVectors[11];
        LeftFoot.localPosition = gameobjectVectors[12];
        RightHip.localPosition = gameobjectVectors[13];
        RightKnee.localPosition = gameobjectVectors[14];
        RightFoot.localPosition = gameobjectVectors[15];
        LeftWrist.localPosition = gameobjectVectors[16];
        RightWrist.localPosition = gameobjectVectors[17];
        Neck.localPosition = gameobjectVectors[18];
        // Move the character where the cubes are at
        characterVector.x = BaseSpine.position.x;
        characterVector.z = BaseSpine.position.z;
        playerCharacter.position = characterVector;
    }

    private void ReformatMessage(string message)
    {
        float x, y, z;
        string[] perBodyparts = message.Split(' ');
        string[][] wholeMessage = new string[perBodyparts.Length - 1][];
        for (int i = 0; i < perBodyparts.Length - 1; i++)   // Splitting message into parts, and changing vector coordinates
        {
            wholeMessage[i] = perBodyparts[i].Split(';');

            x = float.Parse(wholeMessage[i][1]);
            x = (x/divisorX) + offsetX;
            y = float.Parse(wholeMessage[i][2]);
            y = (y/divisorY) + offsetY;
            z = float.Parse(wholeMessage[i][3]);
            z = -(z/divisorZ) + offsetZ;
            //Debug.Log(wholeMessage[i][0] + ";" + x + ";" + y + ";" + z + ";");

            gameobjectVectors[i].x = x;
            gameobjectVectors[i].y = y;
            gameobjectVectors[i].z = z;

    }
}
}
