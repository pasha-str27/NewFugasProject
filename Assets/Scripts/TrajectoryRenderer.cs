using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField]
    private int trajectoryLenth = 50;
    [SerializeField]
    private float pointsAmount = 0.3f;

    private LineRenderer lineRendererComponent;

    private void Start()
    {
        lineRendererComponent = GetComponent<LineRenderer>();
    }

    internal void ShowTrajectory(Vector3 origin, Vector3 speed, float mass)
    {
        Vector3[] points = new Vector3[trajectoryLenth];
        lineRendererComponent.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * pointsAmount;

            points[i] = origin + speed * time / mass + Physics.gravity * time * time / 2f;
        }

        lineRendererComponent.SetPositions(points);
    }

    internal void ClearTrajectory()
    {
        lineRendererComponent.positionCount = 0;
    }
}

