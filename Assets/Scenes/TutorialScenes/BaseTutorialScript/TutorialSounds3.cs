using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSounds3 : MonoBehaviour
{
    public DefendDialogScript defendDialogScript;
    private AudioSource audioSource;
    public AudioClip[] rockyClips;
    public AudioClip[] bubblesClips;
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
            if (defendDialogScript.textComponent.text != defendDialogScript.lines[defendDialogScript.index])
            {
                audioSource.Stop();
                if (indexSounds == 0)
                {
                    audioSource.PlayOneShot(bubblesClips[0]);
                }
                else if (indexSounds == 1)
                {
                    audioSource.PlayOneShot(rockyClips[0]);
                }
                else if (indexSounds == 2)
                {
                    audioSource.PlayOneShot(thorfinClips[0]);
                }
                else if (indexSounds == 3)
                {
                    audioSource.PlayOneShot(rockyClips[1]);
                }
                else if (indexSounds == 4)
                {
                    audioSource.PlayOneShot(thorfinClips[1]);
                }
                else if (indexSounds == 5)
                {
                    audioSource.PlayOneShot(thorfinClips[2]);
                }
                else if (indexSounds == 6)
                {
                    audioSource.PlayOneShot(rockyClips[2]);
                }
                else if (indexSounds == 7)
                {
                    audioSource.PlayOneShot(thorfinClips[3]);
                }
                else if (indexSounds == 8)
                {
                    audioSource.PlayOneShot(thorfinClips[4]);
                }
                else if (indexSounds == 9)
                {
                    audioSource.PlayOneShot(thorfinClips[5]);
                }
                else if (indexSounds == 10)
                {
                    audioSource.PlayOneShot(rockyClips[3]);
                }
                else if (indexSounds == 11)
                {
                    audioSource.PlayOneShot(thorfinClips[6]);
                }
                else if (indexSounds == 12)
                {
                    audioSource.PlayOneShot(thorfinClips[7]);
                }
                else if (indexSounds == 13)
                {
                    audioSource.PlayOneShot(thorfinClips[8]);
                }
                else if (indexSounds == 14)
                {
                }
                else if (indexSounds == 15)
                {
                    audioSource.PlayOneShot(thorfinClips[9]);
                }
                else if (indexSounds == 16)
                {
                    audioSource.PlayOneShot(bubblesClips[1]);
                }
                else if (indexSounds == 17)
                {
                    audioSource.PlayOneShot(rockyClips[4]);
                }
                else if (indexSounds == 18)
                {
                    audioSource.PlayOneShot(bubblesClips[2]);
                }
                else if (indexSounds == 19)
                {
                    audioSource.PlayOneShot(thorfinClips[10]);
                }
                isPlaying = true;
            }

        }
    }
}
