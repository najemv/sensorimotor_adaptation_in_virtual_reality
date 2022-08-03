using UnityEngine;

/// <summary>
/// Checkpoint which must be crossed by pointer in order to complete task.
/// </summary>
public class Checkpoint : MonoBehaviour
{
    /// <summary>
    /// Was this object crossed by pointer?
    /// </summary>
    public bool completed = false;

    /// <summary>
    /// Path segment which belongs to this checkpoint.
    /// </summary>
    public Collider segment;

    /// <summary>
    /// Reference to the object for whom this belongs to.
    /// </summary>
    public MeasuredObject measuredObject;

    void OnTriggerEnter(Collider other)
    {
        // pointer crossed the checkoint
        if (other.tag == "Pointer")
        {
            // check if this checkpoint is in order
            if (measuredObject.CheckPoint(this))
            {
                MakeCompleted();
                measuredObject.CurrentSegment = segment;
            }
        }

    }

    /// <summary>
    /// Mark this object as completed.
    /// </summary>
    public void MakeCompleted()
    {
        completed = true;
        // green color
        GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1.0f);
    }

    /// <summary>
    /// Set this checkpoint to default state.
    /// </summary>
    public void MakeUncompleted()
    {
        completed = false;
        // red color
        GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1.0f);
    }
}
