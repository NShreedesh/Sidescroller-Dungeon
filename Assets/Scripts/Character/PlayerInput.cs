using ScriptableObjects.Input;
using UnityEngine;

namespace Dungeon.Character
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private PlayerMovement playerMovement;

        [Header("Input")]
        [SerializeField]
        private InputReader inputReader;

        private void OnEnable()
        {
            inputReader.MoveEvent += OnMoveInput;
            inputReader.JumpStartedEvent += OnJumpStartedInput;
            inputReader.CrouchStartedEvent += OnCrouchStartedInput;
            inputReader.CrouchCanceledEvent += OnCrouchCanceledInput;
        }

        private void OnMoveInput(float moveInput)
        {
            playerMovement.SetMoveInput(moveInput);
        }

        private void OnJumpStartedInput()
        {
            playerMovement.Jump();
        }

        private void OnCrouchStartedInput()
        {
            playerMovement.Crouch();
        }

        private void OnCrouchCanceledInput()
        {
            playerMovement.CrouchStand();
        }

        private void OnDisable()
        {
            inputReader.MoveEvent -= OnMoveInput;
            inputReader.JumpStartedEvent -= OnJumpStartedInput;
            inputReader.CrouchStartedEvent -= OnCrouchStartedInput;
            inputReader.CrouchCanceledEvent -= OnCrouchCanceledInput;
        }
    }
}
