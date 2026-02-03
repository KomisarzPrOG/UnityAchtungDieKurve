using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField] Head headScript;
    [SerializeField] Transform head;
    [SerializeField] float spacing = 0.1f;

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] EdgeCollider2D edgeCollider;

    List<Vector2> points = new List<Vector2>();


    void Start()
    {
        edgeCollider.points = new Vector2[] { new Vector2(99999, 99999), new Vector2(99999, 100000) };

        SetPoint();
    }

    void Update()
    {
        if (headScript.isAlive && Vector2.Distance(points[points.Count - 1], head.position) > spacing) SetPoint();
    }

    void SetPoint()
    {
        if(points.Count >= 2) edgeCollider.SetPoints(points);

        points.Add(head.position);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, head.position);
    }
}
