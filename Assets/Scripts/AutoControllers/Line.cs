using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    GameObject Cat,
        Rabbit,
        stringNodesParent;

    LineRenderer lineRenderer;

    List<Transform> stringNodes = new List<Transform>();

    [SerializeField, Range(0f, 20f)]
    float segmentLength = 1f;

    [SerializeField, Range(0f, 20f)]
    float pullSpeed = 0.5f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        foreach (Transform node in stringNodesParent.transform)
        {
            stringNodes.Add(node);
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void FixedUpdate()
    {
        // Determine the positions of the nodes on the line
        

        // standard nodes
        for (int i = 0; i < 8; i++)
        {
            if (
                Vector3.Distance(stringNodes[i].position, stringNodes[i + 1].position)
                > segmentLength
            )
            {
                stringNodes[i].position = Vector3.MoveTowards(
                    stringNodes[i].position,
                    stringNodes[i + 1].position,
                    pullSpeed
                );
            }
        }

        // Backwards pass
        for (int i = 8; i > 0; i--)
        {
            if (
                Vector3.Distance(stringNodes[i].position, stringNodes[i - 1].position)
                > segmentLength
            )
            {
                stringNodes[i].position = Vector3.MoveTowards(
                    stringNodes[i].position,
                    stringNodes[i - 1].position,
                    pullSpeed
                );
            }
        }

        // backwards pass singularity - cat
        if (Vector3.Distance(Cat.transform.position, stringNodes[0].position) > segmentLength)
        {
            stringNodes[0].position = Vector3.MoveTowards(
                stringNodes[0].position,
                Cat.transform.position,
                pullSpeed
            );
        }

        // first node singularity -> cat
        // We do this one second so that the cat has priority on pulling the string, rather than the string have priority on pulling the cat
        if (Vector3.Distance(Cat.transform.position, stringNodes[0].position) > segmentLength)
        {
            Cat.transform.position = Vector3.MoveTowards(
                Cat.transform.position,
                stringNodes[0].position,
                pullSpeed
            );
        }

        // last node singularity -> rabbit
        if (Vector3.Distance(Rabbit.transform.position, stringNodes[8].position) > segmentLength)
        {
            Rabbit.transform.position = Vector3.MoveTowards(
                Rabbit.transform.position,
                stringNodes[8].position,
                pullSpeed
            );
        }
        // draw the line
        lineRenderer.SetPosition(0, Cat.transform.position);
        for (int i = 0; i < 9; i++)
        {
            lineRenderer.SetPosition(i + 1, stringNodes[i].position);
        }
        lineRenderer.SetPosition(10, Rabbit.transform.position);
    }
}
