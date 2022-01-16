using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // internal countdown until end of level is reached and goal to reach
    private float counter = 5f;
    private float countDown;
    public GameObject goal;
    
    // count times the player collected disguises
    public int[] countDisguises = new int[4];

    // set game over UI elements and status (win or lose)
    public GameObject gameOverUI;
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text[] disguisesCounted = new TMP_Text[4];
    public Button restartButton;
    public AudioClip winningClip;
    public AudioClip losingClip;
    public bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        // set number of counted disguises to zero
        for (int i = 0; i < countDisguises.Length; i++) {
            countDisguises[i] = 0;
        }
        // deactivate game over ui and add listener for restart button
        gameOverUI.SetActive(false);
        restartButton.onClick.AddListener(ReloadScene);

        // start Coroutine that countes down until hero reaches end of level
        StartCoroutine(TimeUntilEndOfLevel());
    }

    // Update is called once per frame
    void Update()
    {
        // reload game when pressing R
        if (Input.GetKeyDown(KeyCode.R)) {
            ReloadScene();
        }
    }

    private IEnumerator TimeUntilEndOfLevel() {
        // start countdown from set time until end of level
        countDown = counter;
        while(countDown > 0) {
            yield return new WaitForSeconds(1);
            countDown--;
        }

        // cancel spawning of new disguises and enemies after countdown ends
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        spawnPoint.GetComponent<Spawning>().StopInvoke();

        // wait 4 seconds
        yield return new WaitForSeconds(4);

        // instanciate goal aka the end of the level to walk towards player
        GameObject goalInstance = Instantiate(goal, spawnPoint.transform.position, Quaternion.identity, spawnPoint.gameObject.transform);

        // wait for 8 seconds
        yield return new WaitForSeconds(8);

        // end the game
        EndGame(true);
    }

    public void EndGame(bool won) {
        // pause game
        Time.timeScale = 0f;

        // sync collected disguises counter with UI
        for (int i = 0; i < disguisesCounted.Length; i++) {
            disguisesCounted[i].text = "x" + countDisguises[i];
        }

        // assign variable for AudioClip that gets chosen based on winning/losing
        AudioClip gameOverSound;

        // enable different UI content depending on winning or losing
        if (won) {
            // UI text
            title.text = "Win (100%)";
            description.text = "You made it! Well ... she didn't look appalled but I don't know if she even noticed you? \n" + 
            "You could always try to work on your dance routine and risk some bolder moves - that'll get her attention, I'm sure of it!";
            // audio clip
            gameOverSound = winningClip;
        } else {
            // calculate how far the player got this time
            int howFarDidIGet = Mathf.RoundToInt((1 - (countDown/counter)) * 100);
            // make sure that player does not get 100 percent when he's close to the finish line and dies
            if (howFarDidIGet == 100) {
                howFarDidIGet = 99;
            }

            // UI text
            title.text = "Game Over (" + howFarDidIGet + "%)";
            description.text = "Well, I suppose you got in, so that's something? \n" + 
            "About your darling: I think I saw her cuddling with a PURPLE fellow, so you might wanna hurry up. \n" + 
            "Also, try to look more inconspicuous next time!";
            // audio clip
            gameOverSound = losingClip;
        }

        // activate GameOver UI
        gameOverUI.SetActive(true);
        // switch from music to win/lose sound
        GameObject.Find("crystalBall").GetComponentInChildren<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameOverSound);
    }

    public void ReloadScene() {
        // unpause and reload scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart successful.");
    }
}
