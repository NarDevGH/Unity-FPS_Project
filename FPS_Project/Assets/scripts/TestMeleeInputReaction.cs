using MyDatatypes.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMeleeInputReaction : MonoBehaviour
{
    [SerializeField] private GameObject currentWeapon;
    [Space]
    [SerializeField] private PoseState currentPoseState;
    [SerializeField] private MovementState currentMovementState;


    private PoseState lastPoseState;
    private MovementState lastMovementState;
    private void Awake()
    {
        lastMovementState = currentMovementState;
        lastPoseState = currentPoseState;
    }

    private void Update()
    {
        if (lastPoseState != currentPoseState)
        {
            lastPoseState = currentPoseState;
            OnPoseStateChange();
        }
        if (lastMovementState != currentMovementState)
        {
            lastMovementState = currentMovementState;
            OnMovementStateChange();
        }
    }

    private void OnPoseStateChange()
    {
        switch (currentPoseState)
        {
            case PoseState.Standing:
                break;
            case PoseState.Crouching:
                break;
            case PoseState.Prone:
                break;
        }
    }
    private void OnMovementStateChange()
    {
        switch (currentMovementState)
        {
            case MovementState.Still:
                break;
            case MovementState.Walking:
                break;
            case MovementState.Sprinting:
                break;

        }
    }

    public void Attack()
    {
    }
    public void Inspect()
    {
    }
}
