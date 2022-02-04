using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ManageScenes : MonoBehaviour
{
    public Button startButton;
    public Button creditsButton;
    public Button returnButton;
    public GameObject creditsUI;

    
    // Start is called before the first frame update
    void Start()
    {
        // add listener for play button
        startButton.onClick.AddListener(PlayGame);
        
        // add listener script to credits button
        creditsButton.onClick.AddListener(ShowCredits);

        // add listener script to return button
        returnButton.onClick.AddListener(HideCredits);
    }

    private void PlayGame() {
        SceneManager.LoadScene("PartyGame");
    }


    public void ShowCredits() {
        creditsUI.SetActive(true);
    }    
    public void HideCredits() {
        creditsUI.SetActive(false);
    }
}
