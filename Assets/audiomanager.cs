using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource SFX;

    [SerializeField] public AudioClip bg;
    [SerializeField] public AudioClip celestialStrike;
    [SerializeField] public AudioClip enemyAttack;
    [SerializeField] public AudioClip sword1;
    [SerializeField] public AudioClip sword2;

    [SerializeField] public AudioClip playerRun;
    [SerializeField] public AudioClip playerJump;
    [SerializeField] public AudioClip playerDeath;
    [SerializeField] public AudioClip enemyDeath;

    // Start is called before the first frame update
    void Start()
    {
        bgMusic.clip = bg;
        bgMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        SFX.Stop();
    }

}
