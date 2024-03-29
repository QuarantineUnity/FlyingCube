using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
        private LineRenderer lineRenderer;


        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }


        public void ShowTrajectory(Vector3 origin, Vector3 speed)
        {

            Vector3[] points = new Vector3[10];



            lineRenderer.positionCount = points.Length;


            for (int i = 0; i < points.Length; i++)
            {
                float time = i * 0.1f;
                points[i] = origin + speed * time + Physics.gravity * time * time / 2f;

                /* if (points[i].y < -3) 
                 {
                     lineRenderer.positionCount = i + 1;
                     break;
                 }
                */
            }

            lineRenderer.SetPositions(points);

        }

        public void DeleteTrajectory()
        {
            lineRenderer.positionCount = 0;
        }
    }