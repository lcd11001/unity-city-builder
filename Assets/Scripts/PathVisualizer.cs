using System;
using System.Collections;
using System.Collections.Generic;
using CityBuilder.AI;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathVisualizer : MonoBehaviour
{
    LineRenderer lineRenderer;
    AiAgent currentAgent;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public void ShowPath(List<Vector3> path, AiAgent agent, Color color)
    {
        ResetPath();

        lineRenderer.positionCount = path.Count;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        for (int i=0; i<path.Count; i++)
        {
            Vector3 position = new Vector3(path[i].x, agent.transform.position.y, path[i].z);
            lineRenderer.SetPosition(i, position);
        }

        currentAgent = agent;
        currentAgent.OnDeath += ResetPath;
    }

    public void ResetPath()
    {
        lineRenderer.positionCount = 0;

        if (currentAgent != null)
        {
            currentAgent.OnDeath -= ResetPath;
        }

        currentAgent = null;
    }
}
