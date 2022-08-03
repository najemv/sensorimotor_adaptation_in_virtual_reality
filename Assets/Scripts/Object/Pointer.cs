using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Script for object representing cursor with which the measurement is performed.
/// </summary>
public class Pointer : MonoBehaviour
{
    /// <summary>
    /// Real position of given Controller.
    /// </summary>
    public GameObject rawInput;

    /// <summary>
    /// Is measuring active or not?
    /// </summary>
    private bool measure = false;

    /// <summary>
    /// Reference to current episode happening right now.
    /// </summary>
    private Episode currentEpisode = null;

    /// <summary>
    /// Setting whether this pointer is for right or left hand.
    /// </summary>
    public bool isRightHand = false;

    void Awake()
    {
        SensorimotorAdaptation.MeasureStart += MeasureStartHandler;
        SensorimotorAdaptation.MeasureEnd += MeasureEndHandler;
        SensorimotorAdaptation.EpisodeChanged += ËpisodeChangedHandler;
    }

    void OnDestroy()
    {
        SensorimotorAdaptation.MeasureStart -= MeasureStartHandler;
        SensorimotorAdaptation.MeasureEnd -= MeasureEndHandler;
        SensorimotorAdaptation.EpisodeChanged -= ËpisodeChangedHandler;
    }

    public void MeasureStartHandler(object sender, EventArgs e)
    {
        measure = true;
    }

    public void MeasureEndHandler(object sender, EventArgs data)
    {
        measure = false;
        currentEpisode = null;
        // hide ray
        DrawRay(Vector3.zero, Vector3.zero);
    }

    private void ËpisodeChangedHandler(object sender, EventArgs e)
    {
        currentEpisode = sender as Episode;
    }

    void Start()
    {
        InitLineRenderer();

        if ((SensorimotorAdaptation.RightHanded && isRightHand) || (!SensorimotorAdaptation.RightHanded && !isRightHand))
        {
            // ók, keep it
        }
        else
        {
            // we don't need it, destroy it
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position = TransformPosition();
        if (measure)
        {
            Measure();
        }
    }

    /// <summary>
    /// Get position from raw input from controller and apply distortions, if episode is running
    /// </summary>
    /// <returns></returns>
    private Vector3 TransformPosition()
    {
        Vector3 newPosition = rawInput.transform.position;
        if (currentEpisode != null)
        {
            foreach (var distortion in currentEpisode.Distortions)
            {
                newPosition = distortion.Transform(newPosition, currentEpisode.MeasuredObject.gameObject.transform.position);
            }

            if (!currentEpisode.MeasureIn3D)
            {
                Vector3 V = newPosition - currentEpisode.MeasuredObject.gameObject.transform.position;
                float dist = Vector3.Dot(V, Vector3.back);
                newPosition = newPosition - dist * Vector3.back;

            }
        }

        return newPosition;
    }

    /// <summary>
    /// Measure the distance betweeen center of pointer to closest point on measured object
    /// </summary>
    private void Measure()
    {
        Vector3 closestPoint = currentEpisode.MeasuredObject.CurrentSegment.GetComponent<MeshKDTree>().ClosestPointOnSurface(gameObject.transform.position);
        var distance = Vector3.Distance(gameObject.transform.position, closestPoint);

        // dirty hack to not not calculate distance when pointer is inside path mesh
        var direction = closestPoint - gameObject.transform.position;
        RaycastHit[] hits = new RaycastHit[10];
        int numOfHits = Physics.RaycastNonAlloc(new Ray(gameObject.transform.position, direction), hits, 0.2f);
        if (distance < 0.1 && numOfHits == 0)
        {
            distance = 0;
        }

        currentEpisode.AddMeasure(gameObject.transform.position, closestPoint);
        DrawRay(gameObject.transform.position, closestPoint);
    }

    private void DrawRay(Vector3 from, Vector3 to)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
    }

    private void InitLineRenderer()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.001f;
        lineRenderer.positionCount = 2;
    }
}
