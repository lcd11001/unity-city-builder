using UnityEngine;

namespace CityBuilder.Utils
{
    public class Divider : MonoBehaviour
    {
        private const int DASH_LENGTH = 8;
        public string NodeName;

        private void OnValidate()
        {
            string dash = new string('-', DASH_LENGTH);
            gameObject.name = $"{dash} {NodeName} {dash}";
            transform.position = Vector3.zero;
        }
    }
}