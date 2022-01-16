using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ManageScenes : MonoBehaviour
{
    public Button startButton;

    
    // Start is called before the first frame update
    void Start()
    {
        // add listener for play button
        startButton.onClick.AddListener(PlayGame);
    }

    public void PlayGame() {
        SceneManager.LoadScene("PartyGame");
        Debug.Log("Play Game");
    }
}
