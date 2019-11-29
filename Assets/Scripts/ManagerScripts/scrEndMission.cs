using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrEndMission : MonoBehaviour
{
    public scrHighScore scriptHighScore;
    public bool canInteract = false;

    // Start is called before the first frame update
    void Start()
    {
        scriptHighScore = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrHighScore>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canInteract == false)
        {
            return;
        }
        else
        {
            if (Input.GetKey(KeyCode.E))
            {
                scriptHighScore.GatherResults();
                scriptHighScore.CompareResults();
                SceneManager.LoadScene("MainMenuScene");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "testDummy")
        {
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "testDummy")
        {
            canInteract = false;
        }
    }
}
