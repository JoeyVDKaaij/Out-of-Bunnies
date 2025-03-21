using UnityEngine;

public class NoteMovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Tooltip("Change the direction that the note is headed")]
    private AxisDirection axisDirection;
    [SerializeField, Tooltip("Set the speed.")]
    private float movementSpeed;

    private void FixedUpdate()
    {
        MoveNote();
    }
    
    /* Move the Note at a speed set in the inspector. */
    private void MoveNote()
    {
        Vector3 moveDirection = Vector3.one;

        switch (axisDirection)
        {
            case AxisDirection.X:
                moveDirection = new Vector3(1, 0, 0);
                break;
            case AxisDirection.Y:
                moveDirection = new Vector3(0, 1, 0);
                break;
            case AxisDirection.Z:
                moveDirection = new Vector3(0, 0, 1);
                break;
        }
        
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }
}
