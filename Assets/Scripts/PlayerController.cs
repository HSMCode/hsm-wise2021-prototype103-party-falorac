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

    // variable to check if there is a disguise around player
    private bool canPickUp = false;
    private GameObject currentPickUp;

    // Start is called before the first frame update
    void Start()
    {
        // set the disguise percentage to zero at the beginning
        disguisePercentageUI.value = disguisePercentage;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.Space)) {
            // add disguise's quality value to disguise percentage (and UI)
            disguisePercentage += currentPickUp.GetComponent<DisguiseController>().disguiseQuality;

            // don't overstep range of 0 to 100 percent
            if (disguisePercentage < 100) {
                disguisePercentageUI.value = disguisePercentage;
            } else {
                disguisePercentage = 100;
                disguisePercentageUI.value = disguisePercentage;
            }

            // add +1 to current disguise's counter in GameOver-Script
            switch (currentPickUp.GetComponent<DisguiseController>().disguiseQuality) {
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

            // send message to disguise -> emit particles (number)
            currentPickUp.SendMessage("OnPickupEmitParticles", 5);

            // destroy disguise and reset because it got picked up
            Destroy(currentPickUp);
            canPickUp = false;
        }

        // give different hints in UI depending on disguise quality
        if (disguisePercentage > 80) {
            disguiseText.text = "Excellent";
        } else if (disguisePercentage > 50) {
            disguiseText.text = "Good";
        } else if (disguisePercentage > 20) {
            disguiseText.text = "Acceptable";
        } else {
            disguiseText.text = "Shabby";
        }
    }

    // alternate dance routine when player gets to the end
    public void SwitchDanceRoutine() {
        AudioSource wooSound = gameObject.GetComponent<AudioSource>();
        wooSound.PlayOneShot(wooSound.clip);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("EndPose");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Disguise") {
            canPickUp = true;
            currentPickUp = other.gameObject;
        }
        else if (other.tag == "Enemy") {
            // set the detectionPercentage depending on value of enemy type
            detectionPercentage = other.GetComponent<EnemyController>().detectionAbility;

            // check if player's disguisePercentage is high enough to counter detectionAbility of enemy
            // if yes, subtract detectionPercentage value from disguisePercentage
            if(disguisePercentage >= detectionPercentage) {
                disguisePercentage -= detectionPercentage;
                disguisePercentageUI.value = disguisePercentage;

                // make enemy react (a little bit) and play heartbeat
                other.SendMessage("ShowAnger");
            } 
            // if no, end game
            else {
                disguisePercentage = 0;
                disguisePercentageUI.value = disguisePercentage;
                gameObject.GetComponent<GameOver>().EndGame(false);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Disguise") {
            canPickUp = false;
        }
    }
}
