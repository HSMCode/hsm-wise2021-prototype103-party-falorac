using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // variables for disguise percentage and UI Elements
    private int disguisePercentage = 0;
    private int detectionPercentage;
    public Slider disguisePercentageUI;
    public TMP_Text disguiseText;

    // Start is called before the first frame update
    void Start()
    {
        // set the disguise percentage to zero at the beginning
        disguisePercentageUI.value = disguisePercentage;
    }

    // Update is called once per frame
    void Update()
    {
        // give different hints in UI depending on disguise quality
        switch (disguisePercentage) {
            case 0:
            disguiseText.text = "Quick, get yourself a disguise!";
            break;
            case 20:
            disguiseText.text = "That's not going to be enough!";
            break;
            case 40: 
            disguiseText.text = "They might not notice you!";
            break;
            case 60:
            disguiseText.text = "You almost blend in with the crowd!";
            break;
            case 80:
            disguiseText.text = "Great, this way you're going to get far!";
            break;
            case 100:
            disguiseText.text = "Amazing, no one will detect you!";
            break;
        }
    }

    private void OnTriggerStay(Collider other) {
        // condition to hold space when colliding with a disguise
        if (other.tag == "Disguise" && Input.GetKey(KeyCode.Space)) {
            // add disguise's quality value to disguise percentage (and UI)
            disguisePercentage += other.GetComponent<DisguiseController>().disguiseQuality;

            // don't overstep range of 0 to 100 percent
            if (disguisePercentage < 100) {
                disguisePercentageUI.value = disguisePercentage;
            } else {
                disguisePercentage = 100;
                disguisePercentageUI.value = disguisePercentage;
            }

            // add +1 to current disguise's counter in GameOver-Script
            switch (other.GetComponent<DisguiseController>().disguiseQuality) {
                case 20:
                gameObject.GetComponent<GameOver>().countDisguises[0]++;
                break;
                case 30:
                gameObject.GetComponent<GameOver>().countDisguises[1]++;
                break;
                case 40:
                gameObject.GetComponent<GameOver>().countDisguises[2]++;
                break;
                case 50:
                gameObject.GetComponent<GameOver>().countDisguises[3]++;
                break;
            }

            // destroy disguise because it got picked up
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
         if (other.tag == "Enemy") {
            // set the detectionPercentage depending on value of enemy type
            detectionPercentage = other.GetComponent<EnemyController>().detectionAbility;

            // check if player's disguisePercentage is high enough to counter detectionAbility of enemy
            // if yes, subtract detectionPercentage value from disguisePercentage
            if(disguisePercentage >= detectionPercentage) {
                disguisePercentage -= detectionPercentage;
                disguisePercentageUI.value = disguisePercentage;
            } 
            // if no, end game
            else {
                disguisePercentage = 0;
                disguisePercentageUI.value = disguisePercentage;
                gameObject.GetComponent<GameOver>().EndGame(false);
            }
        }
    }
}
