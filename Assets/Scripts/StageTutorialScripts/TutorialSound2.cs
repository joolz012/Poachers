using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSounds2 : MonoBehaviour
{
    public TutorialDialog tutorialDialog;
    private AudioSource audioSource;
    public AudioClip[] rockyClips;
    public AudioClip[] thorfinClips;
    public bool isPlaying = true;

    public int indexSounds;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    IEnumerator PlayDelayedAudio(float delay, AudioClip clip)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
        yield break;
    }
    // Update is called once per frame
    void Update()
    {
        //indexSounds = dialogScript.index;
        if (/*Input.GetMouseButtonDown(0) || */!isPlaying)
        {
            if (tutorialDialog.textComponent.text != tutorialDialog.lines[tutorialDialog.index])
            {
                audioSource.Stop();
                if (indexSounds == 0)
                {
                }
                else if (indexSounds == 1)
                {
                    audioSource.PlayOneShot(rockyClips[0]);
                }
                else if (indexSounds == 2)
                {
                }
                else if (indexSounds == 3)
                {
                    audioSource.PlayOneShot(rockyClips[1]);
                }
                else if (indexSounds == 4)
                {
                }
                else if (indexSounds == 5)
                {
                    audioSource.PlayOneShot(rockyClips[2]);
                }
                else if (indexSounds == 6)
                {

                }
                else if (indexSounds == 7)
                {
                }
                else if (indexSounds == 8)
                {
                    audioSource.PlayOneShot(rockyClips[3]);
                }
                else if (indexSounds == 9)
                {

                }
                else if (indexSounds == 10)
                {
                    audioSource.PlayOneShot(rockyClips[4]);
                }
                else if (indexSounds == 11)
                {
                }
                else if (indexSounds == 12)
                {

                }                
                isPlaying = true;
            }

        }
    }
}
