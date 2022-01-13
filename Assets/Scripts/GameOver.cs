using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // internal countdown until end of level is reached and goal to reach
    private float counter = 30f;
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

        // wait for 2 seconds
        yield return new WaitForSeconds(2);

        // instanciate goal aka the end of the level to walk towards player
        GameObject goalInstance = Instantiate(goal, spawnPoint.transform.position, Quaternion.identity, spawnPoint.gameObject.transform);

        // wait for 5 seconds
        yield return new WaitForSeconds(5);

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

        // enable different UI content depending on winning or losing
        if (won) {
            // UI text
            title.text = "Win (100%)";
            description.text = "You made it! Lorem ipsum dolor sit amet, asdlfjwpaoeirfjaösifjaöliwerjföowsef";
        } else {
            // calculate how far the player got this time
            float howFarDidIGet = (1 - (countDown/counter)) * 100;

            // UI text
            title.text = "Game Over (" + howFarDidIGet + "%)";
            description.text = "Oh no, you got caught! This way, they will never notice you :( \n" +
            "Maybe you should try to find better disguises next time.";
        }

        // activate GameOver UI
        gameOverUI.SetActive(true);
    }

    public void ReloadScene() {
        // unpause and reload scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart successful.");
    }
}
