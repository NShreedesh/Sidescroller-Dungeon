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
        [SerializeField]
        private float accelarationSpeed = 100;
        [SerializeField]
        private float deccelarationSpeed = 50;
        private float _curentMoveSpeed;

        [Header("Inputs")]
        private float _moveInput;

        private void Update()
        {
            float deltaValue = Mathf.Sign(_curentMoveSpeed) != Mathf.Sign(_moveInput) ? deccelarationSpeed : accelarationSpeed;
            if(_moveInput == 0) deltaValue = deccelarationSpeed;
            _curentMoveSpeed = Mathf.MoveTowards(_curentMoveSpeed, _moveInput * moveSpeed, deltaValue * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 velocity = rb.linearVelocity;
            float xMovement = _curentMoveSpeed * Time.fixedDeltaTime;
            velocity.x = xMovement;
            rb.linearVelocity = velocity;   
        }

        public void SetMoveInput(float moveInput) => _moveInput = moveInput;
    }
}
