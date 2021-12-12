using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Snap : MonoBehaviour
{
    [SerializeField] private Vector3 gridSize = default;

    private bool isEntered = false;

    private XRGrabInteractable grabInteractable = null;



    private void OnTriggerEnter(Collider other)
    {
        isEntered = true;
        print("I entered a trigger");
        //this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isEntered = false;
        print("I exited a trigger");
        //this.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

    }



    //private void OnDrawGizmos()
    void Update()
    {
        //print("selectEntered: " + grabInteractable.selectEntered.ToString());
        if (isEntered)
            SnapToGrid();
    }

    private void SnapToGrid()
    {
        var position = new Vector3(
            // Position Snapping
            Mathf.Round(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
            Mathf.Round(this.transform.position.y / this.gridSize.y) * this.gridSize.y,
            Mathf.Round(this.transform.position.z / this.gridSize.z) * this.gridSize.z
            );

        this.transform.position = position;

        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        
    }
}
