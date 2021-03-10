using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementMode { Walking, Running, Crouching, Proning, Swimming, Sprinting }

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    public Transform t_mesh;
    public float maxSpeed = 10;

    private Rigidbody rb;

    private MovementMode movementMode;

    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // transform.Translate(velocity.normalized * maxSpeed);
        rb.velocity = new Vector3(velocity.normalized.x * maxSpeed, rb.velocity.y, velocity.normalized.z * maxSpeed);

        if (velocity.magnitude > 0)
        {
            t_mesh.rotation = Quaternion.LookRotation(velocity);
        }
    }

    public Vector3 Velocity { get => rb.velocity; set => velocity = value; }

    public void SetMovementMode(MovementMode mode)
    {
        movementMode = mode;
        switch (mode)
        {
            case MovementMode.Walking:
                {
                    maxSpeed = 5;
                    break;
                }
            case MovementMode.Running:
                {
                    maxSpeed = 10;
                    break;
                }
            case MovementMode.Crouching:
                {
                    maxSpeed = 4;
                    break;
                }
            case MovementMode.Proning:
                {
                    maxSpeed = 2;
                    break;
                }
            case MovementMode.Swimming:
                {
                    maxSpeed = 5;
                    break;
                }
            case MovementMode.Sprinting:
                {
                    maxSpeed = 14;
                    break;
                }
        }
    }

    public MovementMode GetMovementMode()
    {
        return movementMode;
    }
}