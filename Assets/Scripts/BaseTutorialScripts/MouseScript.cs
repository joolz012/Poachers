using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseScript : MonoBehaviour
{
    public DialogScript dialogScript;

    public LayerMask mask;
    public string targetTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseRaycast();
    }
    void MouseRaycast()
    {
        //Debug.Log(currentMoney);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mask))
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (hit.collider.gameObject.CompareTag(targetTag))
                {
                    Debug.Log("Weapon");
                    GameObject clickedObject = hit.collider.gameObject;
                    EnableCanvas(clickedObject);
                    dialogScript.StartCoroutine(dialogScript.Countdown());
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void EnableCanvas(GameObject gameObject)
    {
        Transform canvasTransform = gameObject.transform.Find("BestiaryCanvas");

        if (canvasTransform != null)
        {
            canvasTransform.gameObject.SetActive(true);
            // Debug.Log("Enabled canvas for object: " + parentObject.name);
        }
        else
        {
            Debug.LogWarning("Canvas not found on object: " + gameObject.name);
        }
    }
}
