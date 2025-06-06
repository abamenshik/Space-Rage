using System.Collections;
using UnityEngine;

namespace SpaceRage
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float jumpForce = 10f;
        [SerializeField, Min(1f)] private float jumpWhenHoldForce = 12f;
        [SerializeField, Min(1f)] private float doubleJumpForce = 15f;
        [SerializeField, Min(1f)] private float rotationSpeedX;
        [SerializeField, Min(1f)] private float rotationSpeedY;
        [SerializeField, Min(0f)/*, Range(0f, 1f)*/] private float brakeIntensivity = 0.98f;
        [SerializeField, Min(0f)] private float checkGroundRadius = 1f;
        [SerializeField] private LayerMask whatIsGround;

        private Rigidbody _rigidbody;
        private bool doubleJumpIsUnlocked;
        private bool isHolding;
        private bool IsTouchingGround => Physics.CheckSphere(
            transform.position, checkGroundRadius, whatIsGround);

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            YusamCommon.YuCoApplicationHelper.HideCursor();
        }
        private void Update()
        {
            var isTouchingGround = IsTouchingGround;

            if (isTouchingGround)
                doubleJumpIsUnlocked = true;

            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Brake());
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTouchingGround)
                {
                    if (isHolding)
                    {
                        JumpStrengthen();
                    }
                    else
                    {
                        JumpCommon();
                    }
                }
                else if (doubleJumpIsUnlocked)
                {
                    JumpDouble();
                }
            }
            var xRotation = Input.GetAxis("Mouse X") * rotationSpeedX * Time.deltaTime;
            var yRotation = Input.GetAxis("Mouse Y") * rotationSpeedY * Time.deltaTime;

            transform.Rotate(-yRotation, xRotation, 0, Space.Self);
        }
        private IEnumerator Brake()
        {
            isHolding = true;
            while (Input.GetMouseButton(0))
            {
                if (!IsTouchingGround)
                {
                    isHolding = false;

                    yield return null;
                    continue;
                }

                var vel = _rigidbody.linearVelocity;
                var max = Mathf.Max(0, vel.magnitude - brakeIntensivity);
                vel = Vector3.ClampMagnitude(vel, max);
                _rigidbody.linearVelocity = vel;

                yield return null;
            }
            isHolding = false;
        }

        private void JumpStrengthen()
        {
            Jump(jumpWhenHoldForce);
        }
        private void JumpDouble()
        {
            doubleJumpIsUnlocked = false;

            Jump(doubleJumpForce);
        }
        private void JumpCommon()
        {
            Jump(jumpForce);
        }
        private void Jump(float force)
        {
            print   (force);
            //var direction = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
            var direction = transform.forward;
            _rigidbody.linearVelocity = direction.normalized * force;

            Debug.DrawLine(transform.position, transform.position + direction, Color.yellow);
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, checkGroundRadius);
        }
#endif
    }
}
