using UnityEngine;
using UnityEngine.InputSystem;

public class TaxiController : MonoBehaviour
{
    [Tooltip("Speed of movement, in meters per second")]
    [SerializeField] float speed = 10f;

    [SerializeField]
    InputAction move = new InputAction(
        type: InputActionType.Value, expectedControlType: nameof(Vector2));

    void OnEnable()
    {
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }

    void Update()
    {
        Vector2 moveDirection = move.ReadValue<Vector2>();
        Vector3 movementVector = new Vector3(moveDirection.x, moveDirection.y, 0) * speed * Time.deltaTime;
        transform.position += movementVector;
    }
}
