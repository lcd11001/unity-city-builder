using System.Collections;
using UnityEngine;

public delegate void OnDeathHandler();

public interface IAiBehaviour
{
    event OnDeathHandler OnDeath;
    void ShowPath();
    Vector3 Position { get; }
}
