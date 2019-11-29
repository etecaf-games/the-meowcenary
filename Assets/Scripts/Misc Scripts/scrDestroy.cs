using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrDestroy : MonoBehaviour
{
    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    void Load1stMission()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}