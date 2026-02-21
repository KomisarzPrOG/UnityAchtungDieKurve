using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Head headScript;
    [SerializeField] Transform head;
    [SerializeField] GameObject tailSegmentPrefab;

    [Header("Settings")]
    [SerializeField] float spacing = 0.1f;

    private Material tailMaterial;

    private LineRenderer currentLine;
    private EdgeCollider2D currentCollider;

    private List<Vector2> points = new List<Vector2>();

    [Header("Gap Settings")]
    [SerializeField] float minDistanceBetweenGaps = 3f;
    [SerializeField] float gapLength = 30f;
    [SerializeField] float gapProbability = 0.05f;

    private float distanceSinceLastGap = 0f;
    private float currentGapDistance = 0f;
    private bool isGapActive = false;


    void Update()
    {
        if (GameManager.Instance.state != GameState.Playing) return;
        if (!headScript.isAlive) return;
        if (headScript.phaseWalk) return;

        Vector2 headPos = head.position;

        if (points.Count == 0)
        {
            AddPoint(headPos);
            return;
        }

        if (Vector2.Distance(points[points.Count - 1], headPos) > spacing)
        {
            float delta = Vector2.Distance(points[points.Count - 1], headPos);
            distanceSinceLastGap += delta;

            if (isGapActive)
            {
                currentGapDistance += delta;

                if (currentGapDistance >= gapLength)
                {
                    isGapActive = false;
                    distanceSinceLastGap = 0f;
                    StartNewSegment();
                }

                return;
            }

            if (distanceSinceLastGap >= minDistanceBetweenGaps)
            {
                if (Random.value < gapProbability)
                {
                    isGapActive = true;
                    currentGapDistance = 0f;
                    return;
                }
            }

            AddPoint(headPos);
        }
    }

    void AddPoint(Vector2 pos)
    {
        if (points.Count >= 2) currentCollider.SetPoints(points);

        points.Add(pos);

        currentLine.positionCount = points.Count;
        currentLine.SetPosition(points.Count - 1, pos);
    }

    void CreateNewSegment()
    {
        GameObject segment = Instantiate(tailSegmentPrefab, transform);

        currentLine = segment.GetComponent<LineRenderer>();
        currentCollider = segment.GetComponent<EdgeCollider2D>();

        currentCollider.points = new Vector2[] { new Vector2(99999, 99999), new Vector2(99999, 100000) };

        currentLine.material = tailMaterial;

        points = new List<Vector2>();
    }

    public void StartNewSegment()
    {
        CreateNewSegment();
    }

    public void ResetTail()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        points.Clear();
        currentLine = null;
        currentCollider = null;

        CreateNewSegment();
    }

    public void SetTail(Material material)
    {
        tailMaterial = material;

        foreach (Transform child in transform)
        {
            LineRenderer lr = child.GetComponent<LineRenderer>();
            if (lr != null)
                lr.material = material;
        }

        if (headScript != null)
            headScript.playerColor = material.color;
    }

    public void SetStartingTail()
    {
        CreateNewSegment();
            
        Vector2 headPos = head.position;

        for (int i = 3; i > 0; i--)
        {
            Vector2 point = headPos - (Vector2)head.up * spacing * i;
            AddPoint(point);
        }   

        AddPoint(headPos);
    }
}