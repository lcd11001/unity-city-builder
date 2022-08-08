using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiVertex : IEquatable<AiVertex>
    {
        public Vector3 Position { get; set; }

        public AiVertex(Vector3 pos)
        {
            Position = pos;
        }

        public bool Equals(AiVertex other)
        {
            return Vector3.SqrMagnitude(Position - other.Position) < 0.0001f;
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
