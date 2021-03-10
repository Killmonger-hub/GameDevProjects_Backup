using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float forwardInput;
    private float rightInput;

    private Vector3 velocity;

    public CameraController cameraController;
    public CharacterMovement characterMovement;


    public void AddMovementInput(float forward, float right)
    {
        forwardInput = forward;
        rightInput = right;

        Vector3 camFwd = cameraController.transform.forward;
        Vector3 camRight = cameraController.transform.right;

        Vector3 translation = forward * cameraController.transform.forward;
        translation += right * cameraController.transform.right;
        translation.y = 0;

        if (translation.magnitude > 0)
        {
            velocity = translation;
        }
        else
        {
            velocity = Vector3.zero;
        }
        characterMovement.Velocity = translation;
    }

    public float GetVelocity()
    {
        return characterMovement.Velocity.magnitude;
    }

    public void ToggleRun()
    {
        if (characterMovement.GetMovementMode() != MovementMode.Running)
        {
            characterMovement.SetMovementMode(MovementMode.Running);
        }
        else
        {
            characterMovement.SetMovementMode(MovementMode.Walking);
        }
    }
}
