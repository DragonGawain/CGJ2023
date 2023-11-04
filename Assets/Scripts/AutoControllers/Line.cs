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


        // center to rabbit
        // for (int i = 8; i < 19; i++)
        // {
        //     if (
        //         Vector3.Distance(stringNodes[i].position, stringNodes[i + 1].position)
        //         > segmentLength
        //     )
        //     {
        //         stringNodes[i].position = Vector3.MoveTowards(
        //             stringNodes[i].position,
        //             stringNodes[i + 1].position,
        //             pullSpeed
        //         );
        //     }
        // }

        // center to cat
        // for (int i = 10; i > 0; i--)
        // {
        //     if (
        //         Vector3.Distance(stringNodes[i].position, stringNodes[i - 1].position)
        //         > segmentLength
        //     )
        //     {
        //         stringNodes[i].position = Vector3.MoveTowards(
        //             stringNodes[i].position,
        //             stringNodes[i - 1].position,
        //             pullSpeed
        //         );
        //     }
        // }

        // Backwards passes
        // if (Vector3.Distance(stringNodes[45].position, stringNodes[44].position) > segmentLength)
        // {
        //     stringNodes[45].position = Vector3.MoveTowards(
        //         stringNodes[45].position,
        //         stringNodes[44].position,
        //         pullSpeed
        //     );
        // }

        // if (Vector3.Distance(stringNodes[0].position, stringNodes[1].position) > segmentLength)
        // {
        //     stringNodes[0].position = Vector3.MoveTowards(
        //         stringNodes[0].position,
        //         stringNodes[1].position,
        //         pullSpeed
        //     );
        // }

        // backwards pass singularity -> cat
        // if (Vector3.Distance(Cat.transform.position, stringNodes[0].position) > segmentLength)
        // {
        //     stringNodes[0].position = Vector3.MoveTowards(
        //         stringNodes[0].position,
        //         Cat.transform.position,
        //         pullSpeed
        //     );
        // }

        // first node singularity -> cat
        // We do this one second so that the cat has priority on pulling the string, rather than the string have priority on pulling the cat
        // if (Vector3.Distance(Cat.transform.position, stringNodes[0].position) > segmentLength)
        // {
        //     Cat.transform.position = Vector3.MoveTowards(
        //         Cat.transform.position,
        //         stringNodes[0].position,
        //         pullSpeed
        //     );
        // }

        // backwards pass singularity -> rabbit
        // if (Vector3.Distance(Rabbit.transform.position, stringNodes[19].position) > segmentLength)
        // {
        //     stringNodes[19].position = Vector3.MoveTowards(
        //         stringNodes[19].position,
        //         Rabbit.transform.position,
        //         pullSpeed
        //     );
        // }

        // last node singularity -> rabbit
        // if (Vector3.Distance(Rabbit.transform.position, stringNodes[19].position) > segmentLength)
        // {
        //     Rabbit.transform.position = Vector3.MoveTowards(
        //         Rabbit.transform.position,
        //         stringNodes[19].position,
        //         pullSpeed
        //     );
        // }

        // draw the line
        lineRenderer.SetPosition(0, Cat.transform.position);
        for (int i = 0; i < 20; i++)
        {
            lineRenderer.SetPosition(i + 1, stringNodes[i].position);
        }
        lineRenderer.SetPosition(21, Rabbit.transform.position);
    }

    public void catMove()
    {
        // first node singularity (node 0 to cat)
        if (Vector3.Distance(stringNodes[0].position, Cat.transform.position) > segmentLength)
        {
            stringNodes[0].position = Vector3.MoveTowards(
                stringNodes[0].position,
                Cat.transform.position,
                pullSpeed
            );
        }

        // rabbit to cat
        for (int i = 1; i <= 19; i++)
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

        // Rabbit singularity -> rabbit to node 19
        if (Vector3.Distance(Rabbit.transform.position, stringNodes[19].position) > segmentLength)
        {
            Rabbit.transform.position = Vector3.MoveTowards(
                Rabbit.transform.position,
                stringNodes[19].position,
                pullSpeed
            );
        }

        // backwards rabbit pass -> rabbit to node 19
        if (Vector3.Distance(Rabbit.transform.position, stringNodes[19].position) > segmentLength)
        {
            Rabbit.transform.position = Vector3.MoveTowards(
                stringNodes[19].position,
                Rabbit.transform.position,
                pullSpeed * 2
            );
        }
    }

    public void rabbitMove()
    {
        // first node singularity (node 19 to rabbit)
        if (Vector3.Distance(stringNodes[19].position, Rabbit.transform.position) > segmentLength)
        {
            stringNodes[19].position = Vector3.MoveTowards(
                stringNodes[19].position,
                Rabbit.transform.position,
                pullSpeed
            );
        }

        // rabbit to cat
        for (int i = 18; i >= 0; i--)
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

        // Cat singularity -> cat to node 0
        if (Vector3.Distance(Cat.transform.position, stringNodes[0].position) > segmentLength)
        {
            Cat.transform.position = Vector3.MoveTowards(
                Cat.transform.position,
                stringNodes[0].position,
                pullSpeed
            );
        }

        // Backwards cat pass -> node 0 to cat
        if (Vector3.Distance(Cat.transform.position, stringNodes[0].position) > segmentLength)
        {
            stringNodes[0].position = Vector3.MoveTowards(
                stringNodes[0].position,
                Cat.transform.position,
                pullSpeed * 2
            );
        }
    }
}
