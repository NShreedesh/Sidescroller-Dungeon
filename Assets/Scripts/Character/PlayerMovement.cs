using UnityEngine;

namespace Dungeon.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Componentes")]
        [SerializeField]
        private Rigidbody rb;

        [Header("Data")]
        [SerializeField]
        private float moveSpeed = 300;

        [Header("Inputs")]
        private float _moveInput;

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 velocity = rb.linearVelocity;
            float xMovement = _moveInput * moveSpeed * Time.fixedDeltaTime;
            velocity.x = xMovement;
            rb.linearVelocity = velocity;   
        }

        public void SetMoveInput(float moveInput) => _moveInput = moveInput;
    }
}
