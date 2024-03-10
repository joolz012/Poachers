using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicBase : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip[0]);
    }
    public void DefaultBgm()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clip[0]);
    }

    public void PoacherAttacking()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clip[1]);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
