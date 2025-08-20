using Game.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Game.Input.GameInput;
using static UnityEngine.InputSystem.InputAction;

namespace ScriptableObjects.Input
{
    [CreateAssetMenu(fileName = "Input Reader", menuName = "Input/Input Reader")]
    public class InputReader : ScriptableObject, IGameplayActions
    {
        [Header("Input Reader")]
        private GameInput _gameInput;

        [field: Header("Gameplay Input Actions")]
        public event Action<float> MoveEvent;
        public event Action JumpStartedEvent;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Gameplay.SetCallbacks(this);
            }

            EnableGameplayInput();
        }

        public void EnableGameplayInput()
        {
            _gameInput.Gameplay.Enable();
        }

        public void DisableAllInputs()
        {
            _gameInput.Gameplay.Disable();
        }

        private void OnDisable()
        {
            _gameInput.Gameplay.RemoveCallbacks(this);
            _gameInput.Gameplay.Disable();
        }

        public GameplayActions GetGamePlayActions() => _gameInput.Gameplay;

        public void OnMove(CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<float>());
        }

        public void OnJump(CallbackContext context)
        {
            if(context.phase ==InputActionPhase.Started)
                JumpStartedEvent?.Invoke();
        }
    }
}
