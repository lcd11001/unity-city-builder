using UnityEngine;

[CreateAssetMenu(fileName = "SO_MapConfig", menuName = "city-builder/SO_MapConfig", order = 0)]
public class SO_MapConfig : ScriptableObject
{
    public int width;
    public int height;
    public int depth;
    public Material groundMaterial;
    public Material planeMaterial;
}