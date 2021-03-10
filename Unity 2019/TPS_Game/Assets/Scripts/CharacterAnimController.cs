using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterAnimController : MonoBehaviour
{
    public Animator animator;
    private Character character;

    void Start()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        if (animator == null)
        {
            Debug.LogError("No valid Animator found");
            return;
        }

        animator.SetFloat("Velocity", character.GetVelocity());
        Debug.Log(character.GetVelocity());
    }
}
