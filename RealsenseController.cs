using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RealsenseController : MonoBehaviour
{

    private TcpSocket clientSocket;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public GameObject head;
    public GameObject middleBody;
    private float coordZ = -9f;


    #region gameobjectVectors
    Vector3[] gameobjectVectors = new Vector3[6];
    #endregion


    private bool IsMessageReceived = false;
    private bool IsFirst = true;
    // Use this for initialization
    void Awake()
    {
        clientSocket = new TcpSocket("127.0.0.1", 54000);
        clientSocket.MessageReceived += ClientSocket_MessageReceived;
        clientSocket.Connect();
        // Initializing vectors that will be changed through coordinates we'll be receiving
        gameobjectVectors[0] = new Vector3(0f, 0f, coordZ);     //0 - leftShoulder
        gameobjectVectors[1] = new Vector3(0f, 0f, coordZ);     //1 - rightShoulder
        gameobjectVectors[2] = new Vector3(0f, 0f, coordZ);     //2 - leftHand
        gameobjectVectors[3] = new Vector3(0f, 0f, coordZ);     //3 - rightHand
        gameobjectVectors[4] = new Vector3(0f, 0f, coordZ);     //4 - head
        gameobjectVectors[5] = new Vector3(0f, 0f, coordZ);     //5 - midBody
    }

    private void ClientSocket_MessageReceived(string message)
    {
        IsMessageReceived = true;
        ReformatMessage(message);
        Debug.Log(message);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(IsMessageReceived == true)
        {
            leftShoulder.transform.position = gameobjectVectors[0];
            rightShoulder.transform.position = gameobjectVectors[1];
            leftHand.transform.position = gameobjectVectors[2];
            rightHand.transform.position = gameobjectVectors[3];
            head.transform.position = gameobjectVectors[4];
            middleBody.transform.position = gameobjectVectors[5];

            IsMessageReceived = false; 
        }
    }

    private void ReformatMessage(string message)
    {
        string[] perBodyparts = message.Split(' ');
        string[][] wholeMessage = new string[perBodyparts.Length - 1][];
        for (int i = 0; i < perBodyparts.Length - 1; i++)   // Splitting message into parts, and changing vector coordinates
        {
            wholeMessage[i] = perBodyparts[i].Split(';');
            gameobjectVectors[i].x = float.Parse(wholeMessage[i][1]);
            gameobjectVectors[i].y = float.Parse(wholeMessage[i][2]);
        }
    }
}
