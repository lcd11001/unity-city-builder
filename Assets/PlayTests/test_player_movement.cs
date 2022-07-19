using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnitTestDemo;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEditor;
using System;

public class test_player_movement
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator with_positive_vertical_input_moves_forward()
    {
        GameObject go = new GameObject("Player");
        Player player = go.AddComponent<Player>();

        // Modify SerializeField property        
        var so = new SerializedObject(player);
        so.FindProperty("moveSpeed").floatValue = 1f;
        so.ApplyModifiedProperties();
        
        // Assume we press up arrow
        player.PlayerInput = Substitute.For<IInput>();
        player.PlayerInput.Vertical.Returns(1f);
        

        // display of player
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(go.transform);
        cube.transform.localPosition = Vector3.zero;
        
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(1f);

        Assert.IsTrue(player.transform.position.z > 0f);
        Assert.AreEqual(player.transform.position.x, 0f);
        Assert.AreEqual(player.transform.position.y, 0f);
    }
}
