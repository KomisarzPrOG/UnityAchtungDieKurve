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
    }

    void Update()
    {
        if (GameManager.Instance.state != GameState.Playing) return;
        if(!headScript.isAlive) return;
        if(points.Count == 0) return;

        if (headScript.isAlive && Vector2.Distance(points[points.Count - 1], head.position) > spacing) SetPoint(head.position);
    }

    void SetPoint(Vector2 pos)
    {
        if(points.Count >= 2) edgeCollider.SetPoints(points);

        points.Add(pos);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, pos);
    }

    public void SetStartingTail()
    {
        Vector2 headPos = head.position;

        for (int i = 3; i > 0; i--)
        {
            Vector2 point = headPos - (Vector2)head.up * spacing * i;
            SetPoint(point);
        }

        SetPoint(headPos);
    }

    public void ResetTail()
    {
        points.Clear();

        lineRenderer.positionCount = 0;

        edgeCollider.points = new Vector2[] { new Vector2(99999, 99999), new Vector2(99999, 100000) };
    }
}
