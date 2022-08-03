using System.Linq;
using UnityEngine;

/// <summary>
/// Script to path which is measured.
/// </summary>
public class MeasuredObject : MonoBehaviour
{
    /// <summary>
    /// List of all checkpoints that belongs to this object.
    /// </summary>
    public Checkpoint[] checkpoints;

    /// <summary>
    /// Is this object waiting to be measured, or is measured right now/was already measured in the past?
    /// </summary>
    private bool beforeMeasurement = true;

    /// <summary>
    /// If the object is visible and active, this is the current task that uses this object.
    /// </summary>
    private Episode currentEpisode = null;

    /// <summary>
    /// Reference to current segment that is active.
    /// </summary>
    public Collider CurrentSegment { get; set; }
    
    /// <summary>
    /// How many round user completed.
    /// </summary>
    public double RoundCompleted
    {
        get
        {
            return completedRounds + ((double)completedCheckpoints / checkpoints.Length);
        }
    }

    public Canvas info;

    private int completedRounds = 0;

    private int completedCheckpoints = 0;

    private void Start()
    {
        Hide();
    }


    /// <summary>
    /// Initialize and show object to the user.
    /// </summary>
    /// <param name="episode"></param>
    public void Show(Episode episode)
    {
        ResetChechpoints();
        
        currentEpisode = episode;
        beforeMeasurement = true;
        completedRounds = 0;
        completedCheckpoints = 0;
        gameObject.SetActive(true);
        // put the object to the center and correct height so it is right in front of the user
        gameObject.transform.position = new Vector3(0.0f, SensorimotorAdaptation.UserHeight - 0.3f, 0.5f);
        info.gameObject.SetActive(true);
    }

    /// <summary>
    /// Make this object invisible.
    /// </summary>
    public void Hide()
    {
        currentEpisode = null;
        gameObject.SetActive(false);
    }



    /// <summary>
    /// Checks if the checkpoint can be mark as completed, i.e. all previous
    /// checkpoints have to be mark as completed too.
    /// </summary>
    /// <param name="currentCheckpoint">Checkpoint which is currently proccesed.</param>
    /// <returns></returns>
    public bool CheckPoint(Checkpoint currentCheckpoint)
    {
        if (currentCheckpoint == checkpoints.First())
        {
            return CheckFirstPoint();
        }

        var inOrder = CheckPointsOrder(currentCheckpoint);
        if (inOrder)
        {
            // if user crosses last checkpoint, make the first one uncompleted
            // so he/she knows to continue.
            if (currentCheckpoint == checkpoints.Last())
            {
                checkpoints.First().MakeUncompleted();
            }
            completedCheckpoints++;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Check first point on the path.
    /// </summary>
    /// <returns>True or false whether this point can be labeled as completed.</returns>
    private bool CheckFirstPoint()
    {
        if (!beforeMeasurement)
        {
            // if point is ok, start new round.
            if (CheckPointsOrder())
            {
                ResetChechpoints();
                completedRounds++;
                completedCheckpoints = 0;
                return true;
            }
            return false;
        }

        // start the episode
        beforeMeasurement = false;
        currentEpisode.Start();
        info.gameObject.SetActive(false);
        return true;
    }


    /// <summary>
    /// Check if all checkpoint before currentCheckpoint are crossed.
    /// </summary>
    /// <param name="currentCheckpoint">Current checkpoint which needs to be checked.
    /// If null, check all point except firts one.</param>
    /// <returns></returns>
    private bool CheckPointsOrder(Checkpoint currentCheckpoint = null)
    {
        int i = 0;
        for (; i < checkpoints.Length; i++)
        {
            // skip first if it is null
            if (i == 0 && currentCheckpoint == null)
            {
                continue;
            }
            var checkpoint = checkpoints[i];

            if (currentCheckpoint == checkpoint)
            {
                // it must be either last checkpoint on path or the next one must be uncompleted.
                return (i == checkpoints.Length - 1) || (!checkpoints[i + 1].completed);
            }
            else
            {
                // all checkpoint before must be completed
                if (!checkpoint.completed)
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    /// <summary>
    /// Resets all checkpoints to default value.
    /// </summary>
    private void ResetChechpoints()
    {
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.MakeUncompleted();
        }
    }
}
