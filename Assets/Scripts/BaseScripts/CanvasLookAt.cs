using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    public Transform camPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(camPos);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camPos);
    }
}
