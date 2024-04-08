using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public GameObject[] enemyGameobjects;
    public GameObject gate;
    string tagToCheck = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there are any GameObjects with the specified tag in the hierarchy
        bool objectsWithTagExist = CheckForTag();

        // Output the result
        if (objectsWithTagExist)
        {
            gate.SetActive(false);
            Debug.Log("There are GameObjects with tag '" + tagToCheck + "' in the hierarchy.");
        }
        else
        {
            gate.SetActive(true);
            Debug.Log("There are no GameObjects with tag '" + tagToCheck + "' in the hierarchy.");
        }
    }

    private bool CheckForTag()
    {
        // Find all GameObjects with the specified tag in the hierarchy
        enemyGameobjects = GameObject.FindGameObjectsWithTag(tagToCheck);

        // Return true if any GameObjects with the specified tag are found, otherwise return false
        return enemyGameobjects.Length > 0;
    }
}
