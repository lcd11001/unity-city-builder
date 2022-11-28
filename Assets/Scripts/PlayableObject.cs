using System;
using UnityEngine;

public class PlayableObject : MonoBehaviour
{
    public bool Placed { get; private set; }
    public Vector3Int Size { get; private set; }
    private Vector3[] Vertices;

    private void GetColliderVerticesPositionLocal()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        if (b != null)
        {
            Vertices = new Vector3[4];
            Vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
            Vertices[1] = b.center + new Vector3(+b.size.x, -b.size.y, -b.size.z) * 0.5f;
            Vertices[2] = b.center + new Vector3(+b.size.x, -b.size.y, +b.size.z) * 0.5f;
            Vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, +b.size.z) * 0.5f;
        }
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];
        for (int i=0; i<vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildingSystem.current.GetWorldToCell(worldPos);
            Debug.Log($"vertices[{i}] = {vertices[i].ToString()}");
        }


        Size = new Vector3Int(Math.Abs((vertices[0] - vertices[1]).x), Math.Abs((vertices[0] - vertices[3]).y), 1);
        Debug.Log("Size " + Size.ToString());
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Vertices[0]);
    }

    private void Awake()
    {
        GetColliderVerticesPositionLocal();
        CalculateSizeInCells();
    }

    public virtual void Place()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(drag);

        Placed = true;

        // invoke events of placement
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 90, 0));
        Size = new Vector3Int(Size.y, Size.x, 1);

        Vector3[] vertices = new Vector3[Vertices.Length];
        for (int i=0; i<vertices.Length; i++)
        {
            vertices[i] = Vertices[(i + 1) % Vertices.Length];
        }

        Vertices = vertices;
    }
}
