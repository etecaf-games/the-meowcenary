using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrDestroy : MonoBehaviour
{
    public scrGerenciadorSons scriptGerenciaSons;

    void Awake()
    {
        scriptGerenciaSons = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrGerenciadorSons>();
    }
    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    void Load1stMission()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        scriptGerenciaSons.bgmSoundEffects[10].Stop();
    }
}