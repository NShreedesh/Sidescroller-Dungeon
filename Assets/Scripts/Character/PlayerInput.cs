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
        }

        private void OnMoveInput(float moveInput)
        {
            playerMovement.SetMoveInput(moveInput);
        }

        private void OnDisable()
        {
            inputReader.MoveEvent -= OnMoveInput;
        }
    }
}
