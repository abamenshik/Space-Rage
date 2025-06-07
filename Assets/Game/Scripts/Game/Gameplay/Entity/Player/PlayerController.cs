using System;
using System.Collections;
using UnityEngine;

namespace SpaceRage
{
    public class PlayerController : MonoBehaviour
    {
        public event Action<bool> OnBrakingStateChange;

        [Header("Jumps")]
        [SerializeField, Min(1f)] private float jumpForce = 10f;
        [SerializeField, Min(1f)] private float jumpWhenHoldForce = 12f;
        [SerializeField, Min(1f)] private float doubleJumpForce = 15f;

        [Header("Grab")]
        [SerializeField, Min(0f)] private float distanceGrab;
        [SerializeField, Min(0f)] private float rotationSpeedWhenSmoothBraking;
        [SerializeField, Min(0f)] private float rotationTime = 0.5f;
        [SerializeField, Min(0f)] private float speedOverSmoothBraking;
        [SerializeField, Min(0f)/*, Range(0f, 1f)*/] private float brakeIntensivity = 0.98f;

        [Header("Other")]
        [SerializeField, Min(1f)] private float rotationSpeedX;
        [SerializeField, Min(1f)] private float rotationSpeedY;
        [SerializeField, Min(0f)] private float checkGroundRadius = 1f;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private GameObject armsPrefab;

        private Rigidbody _rigidbody;
        private bool doubleJumpIsUnlocked;
        private bool isHolding;
        private bool isSmoothBraking;

        private float Speed => _rigidbody.linearVelocity.magnitude;
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
            print(isSmoothBraking);
            var isTouchingGround = IsTouchingGround;

            if (isTouchingGround)
                doubleJumpIsUnlocked = true;

            if (Input.GetMouseButtonUp(0))
            {
                isHolding = false;
                OnBrakingStateChange?.Invoke(false);
            }
            if (Input.GetMouseButton(0))
            {
                isHolding = true;
                OnBrakingStateChange?.Invoke(true);

                var ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out var hit, distanceGrab, whatIsGround)
                    && !isSmoothBraking)
                {
                    //print(Speed);
                    if (Speed < speedOverSmoothBraking)
                    {
                        //Debug.Log("rig 0 " + Time.time);
                        _rigidbody.linearVelocity = Vector3.zero;
                    }
                    else
                    {
                        this.hit = hit;
                        StartCoroutine(SmoothBrake(hit));
                    }
                }
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
        private GameObject prewArms;
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
            //var direction = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
            var direction = transform.forward;
            _rigidbody.linearVelocity = direction.normalized * force;

            Debug.DrawLine(transform.position, transform.position + direction, Color.yellow);
        }
        private RaycastHit hit;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.SphereHandleCap(0, hit.point, Quaternion.identity, 1, EventType.Repaint);

            Gizmos.DrawWireSphere(transform.position, checkGroundRadius);
        }
#endif
    }
}
