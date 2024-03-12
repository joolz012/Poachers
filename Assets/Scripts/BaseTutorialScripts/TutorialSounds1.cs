using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSounds1 : MonoBehaviour
{
    public DialogScript dialogScript;
    private AudioSource audioSource;
    public AudioClip[] rockyClips;
    public AudioClip[] bubblesClips;
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
            if (dialogScript.textComponent.text != dialogScript.lines[dialogScript.index])
            {
                audioSource.Stop();
                if (indexSounds == 0)
                {
                }
                else if (indexSounds == 1)
                {
                    Debug.Log("Rocky");
                    audioSource.PlayOneShot(rockyClips[0]);
                }
                else if (indexSounds == 2)
                {
                    audioSource.PlayOneShot(bubblesClips[0]);
                }
                else if (indexSounds == 3)
                {
                    Debug.Log("Rocky2");
                    audioSource.PlayOneShot(rockyClips[1]);
                }
                else if (indexSounds == 4)
                {
                    audioSource.PlayOneShot(bubblesClips[1]);
                }
                else if (indexSounds == 5)
                {
                    Debug.Log("Rocky4");
                    audioSource.PlayOneShot(rockyClips[2]);
                }
                else if (indexSounds == 6)
                {

                }
                else if (indexSounds == 7)
                {
                    audioSource.PlayOneShot(bubblesClips[2]);
                }
                else if (indexSounds == 8)
                {

                }
                else if (indexSounds == 9)
                {

                }
                else if (indexSounds == 10)
                {

                }
                else if (indexSounds == 11)
                {
                    audioSource.PlayOneShot(bubblesClips[3]);
                }
                else if (indexSounds == 12)
                {

                }
                else if (indexSounds == 13)
                {

                }
                else if (indexSounds == 14)
                {
                    audioSource.PlayOneShot(bubblesClips[4]);
                }
                else if (indexSounds == 15)
                {

                }
                else if (indexSounds == 16)
                {
                    audioSource.PlayOneShot(bubblesClips[5]);
                }
                else if (indexSounds == 17)
                {
                }
                else if (indexSounds == 18)
                {
                }
                else if (indexSounds == 19)
                {
                    audioSource.PlayOneShot(rockyClips[3]);
                }
                else if (indexSounds == 20)
                {
                }
                else if (indexSounds == 21)
                {
                    audioSource.PlayOneShot(rockyClips[4]);
                }
                else if (indexSounds == 22)
                {
                    audioSource.PlayOneShot(rockyClips[5]);
                }
                else if (indexSounds == 23)
                {
                }
                else if (indexSounds == 24)
                {
                    audioSource.PlayOneShot(rockyClips[6]);
                }
                else if (indexSounds == 25)
                {

                }
                else if (indexSounds == 26)
                {
                    audioSource.PlayOneShot(bubblesClips[6]);
                }
                else if (indexSounds == 27)
                {
                    audioSource.PlayOneShot(rockyClips[7]);
                }
                else if (indexSounds == 28)
                {

                }
                isPlaying = true;
            }
            
        }
    }
}
