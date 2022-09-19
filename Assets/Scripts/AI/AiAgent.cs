using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CityBuilder.AI
{
    [RequireComponent(typeof(Animator))]
    public class AiAgent : MonoBehaviour, IAiBehaviour
    {
        public event OnDeathHandler OnDeath;
        public Vector3 Position => transform.position;
        Animator animator;

        public float speed = 0.2f;
        public float rotationSpeed = 10f;

        List<Vector3> pathToGo = new List<Vector3>();
        bool moveFlag = false;
        int index = 0;
        Vector3 endPosition;

        public Color pathColor;
        PathVisualizer pathVisualizer;


        private void Start()
        {
            pathVisualizer = FindObjectOfType<PathVisualizer>();
            pathColor = UnityEngine.Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
        }

        public void ShowPath()
        {
            pathVisualizer.ShowPath(pathToGo, this, pathColor);
        }

        public void Initialize(List<Vector3Int> path)
        {
            Initialize(path.Select(pointInt => (Vector3)pointInt).ToList());
        }

        public void Initialize(List<Vector3> path)
        {
            Debug.Assert(path.Count > 0, $"invalid path with length = {path.Count}");
            pathToGo = path;
            index = 1;
            moveFlag = true;
            endPosition = pathToGo[index];
            animator = GetComponent<Animator>();
            animator.SetTrigger("Walk");
        }

        private void Update()
        {
            if (moveFlag)
            {
                PerformMovement();
            }
        }

        private void PerformMovement()
        {
            if (pathToGo.Count > index)
            {
                float distanceToGo = MoveTheAgent();
                if (distanceToGo < 0.05f)
                {
                    index++;
                    if (index >= pathToGo.Count)
                    {
                        moveFlag = false;
                        Destroy(gameObject);
                        return;
                    }

                    endPosition = pathToGo[index];
                }
            }
        }

        private float MoveTheAgent()
        {
            float step = speed * Time.deltaTime;
            Vector3 targetPosition = new Vector3(endPosition.x, transform.position.y, endPosition.z); // don't change AI y position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            var lookDirection = targetPosition - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);

            return Vector3.Distance(transform.position, targetPosition);
        }

        private void OnDestroy()
        {
            OnDeath?.Invoke();
        }
    }
}
