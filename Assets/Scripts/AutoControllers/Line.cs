using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    GameObject cat,
        rabbit,
        stringNodesParent;

    LineRenderer lineRenderer;

    List<Transform> stringNodes = new List<Transform>();

    [SerializeField, Range(0f, 20f)]
    float segmentLength = 1f;

    [SerializeField, Range(0f, 20f)]
    float pullSpeed = 0.5f;

    bool isCatMoving = false;
    bool isRabbitMoving = false;

    const int timerReset = 5;
    int timerRab = 0;
    int timerCat = 0;

    bool centerFlipFlop = false;

    GameObject Cat,
        Rabbit;
    bool swapped = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        foreach (Transform node in stringNodesParent.transform)
        {
            stringNodes.Add(node);
        }
        Cat = cat;
        Rabbit = rabbit;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void FixedUpdate()
    {
        if (isRabbitMoving && !isCatMoving)
            rabbitMove();
        else if (isCatMoving && !isRabbitMoving)
            catMove();
        else if (isCatMoving && isRabbitMoving)
            bothMove();

        // draw the line
        lineRenderer.SetPosition(0, Cat.transform.position);
        for (int i = 0; i < 21; i++)
        {
            lineRenderer.SetPosition(i + 1, stringNodes[i].position);
        }
        lineRenderer.SetPosition(22, Rabbit.transform.position);
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
        for (int i = 1; i <= 20; i++)
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
        if (Vector3.Distance(Rabbit.transform.position, stringNodes[20].position) > segmentLength)
        {
            Rabbit.transform.position = Vector3.MoveTowards(
                Rabbit.transform.position,
                stringNodes[20].position,
                pullSpeed
            );
        }

        // backwards rabbit pass -> rabbit to node 20
        if (Vector3.Distance(Rabbit.transform.position, stringNodes[20].position) > segmentLength)
        {
            stringNodes[20].position = Vector3.MoveTowards(
                stringNodes[20].position,
                Rabbit.transform.position,
                pullSpeed * 2
            );
        }
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
        // PUT FULL BACKWARDS PASS (not just node 20)
    }

    public void rabbitMove()
    {
        // first node singularity (node 19 to rabbit)
        if (Vector3.Distance(stringNodes[20].position, Rabbit.transform.position) > segmentLength)
        {
            stringNodes[20].position = Vector3.MoveTowards(
                stringNodes[20].position,
                Rabbit.transform.position,
                pullSpeed
            );
        }

        // rabbit to cat
        for (int i = 19; i >= 0; i--)
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
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
        // PUT FULL BACKWARDS PASS (not just node 0)
    }

    public void bothMove()
    {
        // Super singularity, special case node 10 (center node)
        // Both neighbor nodes
        if (
            Vector3.Distance(stringNodes[10].position, stringNodes[11].position) > segmentLength
            && Vector3.Distance(stringNodes[10].position, stringNodes[9].position) > segmentLength
        )
        {
            timerRab = timerReset;
            timerCat = timerReset;

            for (int i = 11; i <= 20; i++)
            {
                if (
                    Vector3.Distance(stringNodes[i].position, stringNodes[i - 1].position)
                    > segmentLength
                )
                {
                    stringNodes[i].position = Vector3.MoveTowards(
                        stringNodes[i].position,
                        stringNodes[i - 1].position,
                        pullSpeed * 2
                    );
                }
            }

            for (int i = 9; i >= 0; i--)
            {
                if (
                    Vector3.Distance(stringNodes[i].position, stringNodes[i + 1].position)
                    > segmentLength
                )
                {
                    stringNodes[i].position = Vector3.MoveTowards(
                        stringNodes[i].position,
                        stringNodes[i + 1].position,
                        pullSpeed * 2
                    );
                }
            }

            // stringNodes[11].position = Vector3.MoveTowards(
            //     stringNodes[11].position,
            //     stringNodes[10].position,
            //     pullSpeed
            // );

            // stringNodes[9].position = Vector3.MoveTowards(
            //     stringNodes[9].position,
            //     stringNodes[10].position,
            //     pullSpeed
            // );

            if (centerFlipFlop)
            {
                stringNodes[10].position = new Vector3(
                    Vector3
                        .MoveTowards(
                            stringNodes[10].position,
                            stringNodes[11].position,
                            pullSpeed / 2
                        )
                        .x,
                    stringNodes[10].position.y,
                    stringNodes[10].position.z
                );
            }
            else
            {
                stringNodes[10].position = new Vector3(
                    Vector3
                        .MoveTowards(
                            stringNodes[10].position,
                            stringNodes[9].position,
                            pullSpeed / 2
                        )
                        .x,
                    stringNodes[10].position.y,
                    stringNodes[10].position.z
                );
            }
            centerFlipFlop = !centerFlipFlop;
        }

        // node + 1
        if (Vector3.Distance(stringNodes[10].position, stringNodes[11].position) > segmentLength)
        {
            if (timerRab <= 0)
            {
                stringNodes[10].position = Vector3.MoveTowards(
                    stringNodes[10].position,
                    stringNodes[11].position,
                    pullSpeed
                );
            }
            timerRab--;
        }
        else
        {
            timerRab = timerReset;
        }

        // node - 1
        if (Vector3.Distance(stringNodes[10].position, stringNodes[9].position) > segmentLength)
        {
            if (timerCat <= 0)
            {
                stringNodes[10].position = Vector3.MoveTowards(
                    stringNodes[10].position,
                    stringNodes[9].position,
                    pullSpeed
                );
            }
            timerCat--;
        }
        else
        {
            timerCat = timerReset;
        }
        // CAT

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
        for (int i = 1; i <= 9; i++)
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

        // RABBIT

        // first node singularity (node 20 to rabbit)
        if (Vector3.Distance(stringNodes[20].position, Rabbit.transform.position) > segmentLength)
        {
            stringNodes[20].position = Vector3.MoveTowards(
                stringNodes[20].position,
                Rabbit.transform.position,
                pullSpeed
            );
        }

        // rabbit to cat
        for (int i = 19; i >= 11; i--)
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

        // character singularities

        // Cat singularity -> cat to node 0
        if (Vector3.Distance(Cat.transform.position, stringNodes[0].position) > segmentLength)
        {
            Cat.transform.position = Vector3.MoveTowards(
                Cat.transform.position,
                stringNodes[0].position,
                pullSpeed
            );
        }

        // Rabbit singularity -> rabbit to node 20
        if (Vector3.Distance(Rabbit.transform.position, stringNodes[20].position) > segmentLength)
        {
            Rabbit.transform.position = Vector3.MoveTowards(
                Rabbit.transform.position,
                stringNodes[20].position,
                pullSpeed
            );
        }

        // Backwards passes to ensure string doesn't break

        // Backwards cat pass -> node 0 to cat
        if (Vector3.Distance(Cat.transform.position, stringNodes[0].position) > segmentLength)
        {
            stringNodes[0].position = Vector3.MoveTowards(
                stringNodes[0].position,
                Cat.transform.position,
                pullSpeed * 2
            );
        }

        // backwards rabbit pass -> rabbit to node 20
        if (Vector3.Distance(Rabbit.transform.position, stringNodes[20].position) > segmentLength)
        {
            stringNodes[20].position = Vector3.MoveTowards(
                stringNodes[20].position,
                Rabbit.transform.position,
                pullSpeed * 2
            );
        }
    }

    public void setRabbitMoving(bool val)
    {
        isRabbitMoving = val;
    }

    public void setCatMoving(bool val)
    {
        isCatMoving = val;
    }

    public void swapPlaces()
    {
        swapped = !swapped;
        if (swapped)
        {
            Cat = rabbit;
            Rabbit = cat;
        }
        else
        {
            Cat = cat;
            Rabbit = rabbit;
        }
    }
}
