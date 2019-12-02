using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGerenciadorSons : MonoBehaviour
{
    //sound effect list:
    //[0]:gatoDamage
    //[1]:catMorte
    //[2]:gatoPutasso
    //[3]:dogGrowling
    //[4]:dogMorte
    //[5]:UISound
    //[6]:healthPickup
    //[7]:pointsPickup
    //[8]:ammoGet
    //[9]:menuBGM
    //[10]:inGameBGM

    public AudioSource[] playerSoundEffects;
    public AudioSource[] enemySoundEffects;
    public AudioSource[] miscSoundEffects;
    public AudioSource[] bgmSoundEffects;

    // Start is called before the first frame update
    void Start()
    {
        playerSoundEffects = gameObject.GetComponents<AudioSource>();
        enemySoundEffects = gameObject.GetComponents<AudioSource>();
        miscSoundEffects = gameObject.GetComponents<AudioSource>();
        bgmSoundEffects = gameObject.GetComponents<AudioSource>();
        bgmSoundEffects[9].Play();
    }

    // Update is called once per frame

    public void playTheSoundPlayer(int index)
    {
        playerSoundEffects[index].Play();
    }
    public void playTheSoundEnemy(int index)
    {
        enemySoundEffects[index].Play();
    }
    public void playTheSoundMisc(int index)
    {
        miscSoundEffects[index].Play();
    }
    public void playTheSoundBGM(int index)
    {
        bgmSoundEffects[index].Play();
    }
}
