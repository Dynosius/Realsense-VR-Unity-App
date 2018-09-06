using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayBouncyBall()
    {
        CleanupAndLoad(1);
    }

    public void PlayPainting()
    {
        CleanupAndLoad(2);
    }

    public void PlayDestruction()
    {
        CleanupAndLoad(3);
    }
    
    public void PlayCupDemo()
    {
        CleanupAndLoad(4);
    }

    public void QuitGame()
    {
        TcpSocket.CloseSocket();
        Application.Quit();
    }

    private void CleanupAndLoad(int n)
    {
        TcpSocket.CloseSocket();
        Time.timeScale = 1f;
        Pause.GameIsPaused = false;
        SceneManager.LoadScene(n);
    }

}
