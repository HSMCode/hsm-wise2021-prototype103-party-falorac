using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // internal countdown until end of level is reached and goal to reach
    private float counter = 10f;
    public GameObject goal;

    // set game over UI elements
    public GameObject gameOverUI;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        // deactivate game over ui and add listener for restart button
        gameOverUI.SetActive(false);
        restartButton.onClick.AddListener(ReloadScene);

        // start Coroutine that countes down until hero reaches end of level
        StartCoroutine(timeUntilEndOfLevel());
    }

    // Update is called once per frame
    void Update()
    {
        // active for now, delete later
        if (Input.GetKeyDown(KeyCode.R)) {
            ReloadScene();
        }    
    }

    private IEnumerator timeUntilEndOfLevel() {
        float countDown = counter;
        while(countDown > 0) {
            yield return new WaitForSeconds(1);
            countDown--;
            Debug.Log(countDown);
        }
        // gameObject.GetComponent<Spawning>().SendMessage("CancelInvoke");
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        GameObject goalInstance = Instantiate(goal, spawnPoint.transform.position, Quaternion.identity, spawnPoint.gameObject.transform);
        //goalInstance.transform.Translate(Vector3.forward * Time.deltaTime * 5);        

        yield return new WaitForSeconds(5);
        EndGame();
    }

    public void EndGame() {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void ReloadScene() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart successful.");
    }
}
