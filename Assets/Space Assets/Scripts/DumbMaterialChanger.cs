using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbMaterialChanger : MonoBehaviour
{
    public GameObject block;

    public Material inactiveMaterial;

    public Material activeMaterial;

    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = block.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void activate()
    {
        mesh.material = activeMaterial;
    }

    public void deactivate()
    {
        mesh.material = inactiveMaterial;
    }
}
