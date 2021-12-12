using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Something entered my collider!");
    }

    private void OnTriggerExit(Collider other)
    {
        print("Something exited my collider!");
    }
}
