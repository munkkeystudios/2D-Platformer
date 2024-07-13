using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class new_audio_manager : MonoBehaviour
{
    //[SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip boss_attack1;
    public AudioClip boss_attack2;
    public AudioClip boss_phase2;
    public AudioClip boss_move;

    private void Start()
    {
        //musicSource.clip = boss_music;
        //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
