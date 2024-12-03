using UI;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private SteeringWheel wheelInput;
    [SerializeField] private DriveButton driveForwardInput;
    [SerializeField] private DriveButton driveBackwardInput;

    private bool _lockInput;
    public bool LockInput => _lockInput;
    public bool IsSendingInput => wheelInput.IsDragging || driveBackwardInput.IsPressed || driveForwardInput.IsPressed;

    public Vector3 GetDriveDirection()
    {
        if (LockInput)
        {
            return Vector3.zero;
        }

        var forwardDirection = 0;

        if (driveForwardInput.IsPressed && !driveBackwardInput.IsPressed)
        {
            forwardDirection = 1;
        }
        else if (!driveForwardInput.IsPressed && driveBackwardInput.IsPressed)
        {
            forwardDirection = -1;
        }

        return new Vector3(wheelInput.GetNormalizedXPosition(), forwardDirection, 0);
    }

    public void SetInputLock(bool isLocked)
    {
        _lockInput = isLocked;
    }
}
