using UnityEngine;

namespace Dungeon.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Componentes")]
        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private Animator anim;

        [Header("Move Data")]
        [SerializeField]
        private float moveSpeed = 300;
        [SerializeField]
        private float accelarationSpeed = 100;
        [SerializeField]
        private float deccelarationSpeed = 50;
        private float _curentMoveSpeed;

        [Header("Jump Data")]
        [SerializeField]
        private float jumpForce = 10;
        [SerializeField]
        private float jumpStartTime = 0.5f;
        [SerializeField]
        private float jumpCoolDownTime = 0.5f;
        [SerializeField]
        private Transform groundedCheckTransform;
        [SerializeField]
        private LayerMask groundedLayerMask;
        [SerializeField]
        private float distance = 0.1f;
        private bool _isGrounded;
        private bool _isJumping;

        [Header("Inputs")]
        private string _currentAnimation = "Locomotion";

        [Header("Inputs")]
        private float _moveInput;

        private void Update()
        {
            CheckGrounded();
            CalculateMoveSpeed();
            Animate();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void CalculateMoveSpeed()
        {
            float deltaValue = Mathf.Sign(_curentMoveSpeed) != Mathf.Sign(_moveInput) ? deccelarationSpeed : accelarationSpeed;
            if (_moveInput == 0) deltaValue = deccelarationSpeed;
            _curentMoveSpeed = Mathf.MoveTowards(_curentMoveSpeed, _moveInput * moveSpeed, deltaValue * Time.deltaTime);
        }

        private void Move()
        {
            Vector3 velocity = rb.linearVelocity;
            float xMovement = _curentMoveSpeed * Time.fixedDeltaTime;
            velocity.x = xMovement;
            rb.linearVelocity = velocity;
        }

        public void Jump()
        {
            if (_isJumping) return;
            if (!_isGrounded) return;
            if (!_currentAnimation.Equals("Jump Up"))
            {
                _currentAnimation = "Jump Up";
                anim.CrossFadeInFixedTime("Jump Up", .1f, 0);
            }
            Invoke(nameof(DelayJump), jumpStartTime);

            _isJumping = true;
            Invoke(nameof(ResetJump), jumpCoolDownTime);
        }

        private void DelayJump()
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private void Animate()
        {
            if(_isGrounded)
            {
                if (!_currentAnimation.Equals("Locomotion") && !_isJumping)
                {
                    _currentAnimation = "Locomotion";
                    anim.CrossFadeInFixedTime("Locomotion", .1f);
                }
                anim.SetFloat("LocomotionX", _curentMoveSpeed / (moveSpeed));
            }
        }

        private void CheckGrounded()
        {
            _isGrounded = Physics.Raycast(groundedCheckTransform.position, Vector3.down, distance, groundedLayerMask);
        }

        public void SetMoveInput(float moveInput) => _moveInput = moveInput;

        private void ResetJump()
        {
            _isJumping = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (groundedCheckTransform == null) return;
            Gizmos.DrawLine(groundedCheckTransform.position, groundedCheckTransform.position + Vector3.down * distance);
        }
#endif
    }
}
