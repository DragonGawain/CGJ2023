using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    GameObject Cat,
        Rabbit;

    LineRenderer lineRenderer;
    MeshCollider meshCollider;

    Mesh mesh;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void FixedUpdate()
    {
        Vector3 sp = Cat.transform.position;
        Vector3 ep = Rabbit.transform.position;
        lineRenderer.numCapVertices = 20;
        lineRenderer.numCornerVertices = 20;
        lineRenderer.SetPosition(0, sp);
        lineRenderer.SetPosition(19, ep);
        mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        meshCollider.sharedMesh = mesh;
    }
}
