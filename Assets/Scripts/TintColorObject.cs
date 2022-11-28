using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TintColorObject : MonoBehaviour
{
    private MeshRenderer[] originMeshRenderers;
    private Color[] originColors;
    private int len;
    private bool isOriginalColor;
    private Color tintColor;
    void Start()
    {
        isOriginalColor = true;
        originMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        len = originMeshRenderers.Length;
        originColors = new Color[originMeshRenderers.Length];
        for (int i = 0; i < len; i++)
        {
            //Color c = originMeshRenderers[i].material.color;
            //originColors[i] = new Color(c.r, c.g, c.b, c.a);

            // C#: structs are value types, while classes are reference types
            originColors[i] = originMeshRenderers[i].material.color; 
        }
    }

    public void TintColor(Color c)
    {
        if (tintColor == c && isOriginalColor == false)
        {
            //Debug.Log("Ignore tint color " + c.ToString());
            return;
        }
        isOriginalColor = false;
        tintColor = c;
        for (int i = 0; i < len; i++)
        {
            originMeshRenderers[i].material.color = c;
        }
    }

    public void ResetColor()
    {
        if (isOriginalColor)
        {
            //Debug.Log("Ignore reset color ");
            return;
        }

        isOriginalColor = true;
        for (int i = 0; i < len; i++)
        {
            originMeshRenderers[i].material.color = originColors[i];
        }
    }
}
