using System;
using System.Collections;
using Components.Colliders;
using UnityEngine;

namespace SpaceRage
{
    public class PlayerController : MonoBehaviour
    {
        public event Action<bool> OnBrakingStateChange;
        public event Action<bool> OnKickStateChange;

        [Header("Jumps")]
        [SerializeField, Min(1f)] private float jumpForce = 10f;
        [SerializeField, Min(1f)] private float jumpWhenHoldForce = 12f;
        [SerializeField, Min(1f)] private float doubleJumpForce = 15f;

        [Header("Kick")]
        [SerializeField] private Cooldown kickCd;
        [SerializeField, Min(0f)] private float jumpAfterKickForce;

        [Header("Grab")]
        [SerializeField, Min(0f)] private float rotationSpeedWhenSmoothBraking;
        [SerializeField, Min(0f)] private float rotationTime = 0.5f;
        [SerializeField, Min(0f)] private float speedOverSmoothBraking;
        [SerializeField, Min(0f)] private float brakeIntensivity = 0.98f;

        [Header("Other")]
        [SerializeField, Min(1f)] private float rotationSpeedX;
        [SerializeField, Min(1f)] private float rotationSpeedY;
        [SerializeField] private RayCaster collisionWithGround;
        [SerializeField, Min(0f)] private float checkGroundRadius = 1f;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private GameObject armsPrefab;

        private Rigidbody _rigidbody;
        private bool doubleJumpIsUnlocked;
        private bool isHolding;
        private bool isGrounded;
        private bool isSmoothBraking;
        private GameObject prewArms;

        private float Speed => _rigidbody.linearVelocity.magnitude;
        private bool IsTouchingGround => Physics.CheckSphere(
            transform.position, checkGroundRadius, whatIsGround);

        private void Awake()
        {
            // TODO: to entry point
            Application.targetFrameRate = 60;

            _rigidbody = GetComponent<Rigidbody>();

            collisionWithGround.Origin = transform;
        }
        private void Start()
        {
            YusamCommon.YuCoApplicationHelper.HideCursor();
        }
        private void Update()
        {
            var isTouchingGround = IsTouchingGround;

            if (isHolding)
            {
                isGrounded = true;
                doubleJumpIsUnlocked = true;
            }

            GrabCheck();
            JumpCheck();
            CamRotate();
        }
        private void GrabCheck()
        {
            if (Input.GetMouseButton(0))
            {
                isHolding = true;
                OnBrakingStateChange?.Invoke(true);

                var isGrabing = collisionWithGround.TryRayCast(transform.forward, out var hit);
                if (isGrabing && !isSmoothBraking)
                {
                    if (Speed < speedOverSmoothBraking)
                    {
                        _rigidbody.linearVelocity = Vector3.zero;
                    }
                    else
                    {
                        this.hit = hit;
                        StartCoroutine(SmoothBrake(hit));
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                isHolding = false;
                OnBrakingStateChange?.Invoke(false);
            }
        }
        private void JumpCheck()
        {
            if (!Input.GetKeyDown(KeyCode.Space))
                return;

            if (isGrounded)
            {
                if (isHolding)
                {
                    JumpStrengthen();
                }
                else
                {
                    JumpCommon();
                }
                isGrounded = false;
            }
            else if (doubleJumpIsUnlocked)
            {
                JumpDouble();

                StartCoroutine(LateKickEnd());
            }
        }
        private void CamRotate()
        {
            var xRotation = Input.GetAxis("Mouse X") * rotationSpeedX * Time.deltaTime;
            var yRotation = Input.GetAxis("Mouse Y") * rotationSpeedY * Time.deltaTime;

            transform.Rotate(-yRotation, xRotation, 0, Space.Self);
        }

        private IEnumerator LateKickEnd()
        {
            OnKickStateChange?.Invoke(true);

            kickCd.Reset();

            while (!kickCd.IsReady)
            {
                if (collisionWithGround.TryRayCast(transform.forward, out var hit))
                {
                    Debug.Log(hit.collider.name);

                    Jump(jumpAfterKickForce, -transform.forward);

                    break;
                }

                yield return null;
            }

            OnKickStateChange?.Invoke(false);
        }
        private IEnumerator SmoothBrake(RaycastHit hit)
        {
            var grabPoint = hit.point;

            // заспавнить руки в точке

            if (prewArms)
            {
                Destroy(prewArms);
            }
            var armsRotation = Quaternion.LookRotation(hit.normal);
            prewArms = Instantiate(armsPrefab, grabPoint, armsRotation, DynamicSpawn.Parent);

            isSmoothBraking = true;
            var time = Time.time;

            while (Input.GetMouseButton(0))
            {
                if (Time.time - time < rotationTime)
                {
                    var targetRotation = Quaternion.LookRotation(transform.position.To(grabPoint));
                    var rotation = Quaternion.Lerp(transform.rotation, targetRotation,
                        rotationSpeedWhenSmoothBraking * Time.deltaTime);

                    _rigidbody.rotation = rotation;
                }

                //if (!IsTouchingGround)
                //{
                //    yield return null;
                //    continue;
                //}

                var vel = _rigidbody.linearVelocity;
                var max = Mathf.Max(0, vel.magnitude - brakeIntensivity * Time.deltaTime);
                vel = Vector3.ClampMagnitude(vel, max);
                _rigidbody.linearVelocity = vel;

                yield return null;
            }
            isSmoothBraking = false;
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
            Jump(force, transform.forward);
        }
        private void Jump(float force, Vector3 direction)
        {
            _rigidbody.linearVelocity = direction.normalized * force;
        }


        private RaycastHit hit;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (collisionWithGround.TryRayCast(transform.forward, out var hit))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position.AddToY(-.5f), hit.point);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position.AddToY(-.5f), transform.position + transform.forward * collisionWithGround.distance);
            }

            UnityEditor.Handles.SphereHandleCap(0, hit.point, Quaternion.identity, 1, EventType.Repaint);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, checkGroundRadius);
        }
#endif
    }
}
