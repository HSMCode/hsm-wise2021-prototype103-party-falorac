using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Disguise boolean and UI Element
    private bool isDisguised = false;
    public TMP_Text disguiseText;

    // Start is called before the first frame update
    void Start()
    {
        disguiseText.text = "Not Disguised";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Disguise" && Input.GetKey(KeyCode.Space)) {
            Destroy(other);
            isDisguised = true;
            disguiseText.text = "Disguised";
        }
        else if (other.tag == "Enemy") {
            if(isDisguised) {
                isDisguised = false;
                disguiseText.text = "Not Disguised";
            } else {
                gameObject.GetComponent<GameOver>().EndGame();
            }
        }
    }
}
