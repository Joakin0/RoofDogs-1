using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public enum MovementState { Walking, Running, Parkouring, Climbing }

    //[RequireComponent(typeof(ThirdPersonController))]

    public float jumpForce = 20;
    public float climbingSpeed;

    public LayerMask layerMask;
    public bool grounded;

    public bool showDebug = true;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool limitMovement = false;
    [HideInInspector] public bool stopMotion = false;
    [HideInInspector] public float velLimit = 0;
    [HideInInspector] public float curSpeed = 6f;

    //private ThirdPersonController controller;
    private MovementState currentState;
    private Animator anim;
    private Vector3 velocity;
    private float smoothSpeed;

    public delegate void OnLandedDelegate();
    public delegate void OnFallDelegate();
    public event OnLandedDelegate OnLanded;
    public event OnFallDelegate OnFall;

    [Header("Movement Settings")]
    public float walkSpeed;
    public float JogSpeed;
    public float RunSpeed;
    public float fallForce;

    [Header("Feet IK")]
    public bool enableFeetIK = true;

    [SerializeField] private float heightFromGroundRaycast = 0.7f;
    [Range(0, 2f)] [SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private float pelvisOffset = 0f;

    [Range(0, 1f)] [SerializeField] private float pelvisUpDownSpeed = 0.25f;
    [Range(0, 1f)] [SerializeField] private float feetToIKPositionSpeed = 0.25f;

    public string leftFootAnim = "LeftFootCurve";
    public string rightFootAnim = "RightFootCurve";

    private Vector3 leftFootPosition, leftFootIKPosition, rightFootPosition, rightFootIKPosition;
    private Quaternion leftFootIKRotation, rightFootIKRotation;
    private float lastPelvisPositionY, lastLeftFootPosition, lastRightFootPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //groundcheck
        grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.4f, layerMask);
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}
