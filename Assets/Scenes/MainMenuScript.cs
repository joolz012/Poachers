using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Transform lightTrans;
    public float speed;
    private bool goRight;
    // Start is called before the first frame update
    void Start()
    {
        goRight = true;
        StartCoroutine(LightMovement());
    }

    // Update is called once per frame
    void Update()
    {
        if (goRight)
        {
            lightTrans.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            lightTrans.Rotate(0, -speed * Time.deltaTime, 0);
        }
    }

    IEnumerator LightMovement()
    {
        while(true)
        {
            yield return new WaitForSeconds(15);
            Debug.Log("Left");
            goRight = false;
            yield return new WaitForSeconds(15);
            Debug.Log("Right");
            goRight = true;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("CutScene1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
