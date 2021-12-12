using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCodeBlock : MonoBehaviour
{
    public GameObject whileHeaderPrefab = null;

    public GameObject ifHeaderPrefab = null;

    public GameObject leftPrefab = null;

    public GameObject movePrefab = null;

    public GameObject rightPrefab = null;

    public GameObject locationEmpty;

    // Creates While Header Code Block at Target Location
    public void spawnWhileHeader()
    {
        Instantiate(whileHeaderPrefab,
        locationEmpty.transform.position,
        locationEmpty.transform.rotation);
    }

    // Creates While Header Code Block at Target Location
    public void spawnIfHeader()
    {
        Instantiate(ifHeaderPrefab,
        locationEmpty.transform.position,
        locationEmpty.transform.rotation);
    }

    // Creates Turn Left Code Block at Target Location
    public void spawnLeft()
    {
        Instantiate(leftPrefab,
        locationEmpty.transform.position,
        locationEmpty.transform.rotation);
    }

    // Creates Move Code Block at Target Location
    public void spawnMove()
    {
        Instantiate(movePrefab,
        locationEmpty.transform.position,
        locationEmpty.transform.rotation);
    }

    // Creates Turn Right Code Block at Target Location
    public void spawnRight()
    {
        Instantiate(rightPrefab,
        locationEmpty.transform.position,
        locationEmpty.transform.rotation);
    }
}
