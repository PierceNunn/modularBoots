using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, CanDie
{
    [Header ("Movement Variables")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _airMovementSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _maxGroundSpeed;
    [SerializeField] private float _stompDownwardForce;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashDuration;

    [Header("References")]
    [SerializeField] private ParticleSystem StompParticles;
    [SerializeField] private ParticleSystem DashParticles;
    [SerializeField] private GameObject PlayerModel;
    [SerializeField] private Transform CameraTransform;

    private Vector3 movementVector;
    private Vector3 rotateVector;
    private Vector3 pointVector;
    private Vector3 cameraRelevantMovementVector;
    private Vector3 cameraRelevantRotateVector;
    private float currentDashCooldown = 0f;
    private bool isFiring = false;
    private bool isMoving = false;
    private bool isDashing = false;

    private Rigidbody rb;
    private Collider cr;
    private PlayerModsHandler modsHandler;
    private PlayerResources pr;

    public float CurrentDashCooldown { get => currentDashCooldown; set => currentDashCooldown = value; }
    public bool IsDashing { get => isDashing; set => isDashing = value; }

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cr = gameObject.GetComponent<Collider>();
        modsHandler = gameObject.GetComponent<PlayerModsHandler>();
        pr = gameObject.GetComponent<PlayerResources>();

        if (_dashDuration > _dashCooldown)
            Debug.LogWarning("Dash duration is longer than cooldown. dashing will not be flagged as complete properly.");
        DashParticles.Stop();
    }

    public void FixedUpdate()
    {
        UpdateCameraRelevantVectors();
        //Debug.Log(movementVector);

        if(isMoving && IsGrounded())
            transform.forward = Vector3.Slerp(transform.forward, cameraRelevantMovementVector, Time.deltaTime * 10);
        
        if(!IsGrounded())
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotateVector), 0.5f);

        if (IsGrounded() && modsHandler.NoPendingCooldown && pr.CurrentAmmo != pr.MaxAmmo)
            pr.RefillAmmo();
        if(isFiring)
            modsHandler.FireWeapon();

        rb.useGravity = !IsDashing;

        //Not sure where else I can turn off the stomp particles so it's going in Fixed Update
        if (IsGrounded())
        {
            rb.AddForce(cameraRelevantMovementVector * _movementSpeed); //make player move
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, _maxGroundSpeed);
            if(StompParticles.isPlaying)
                StopStompParticles();
        }
        else
        {
            rb.AddForce(cameraRelevantMovementVector * _airMovementSpeed);
        }
    }

    public void StopStompParticles()
    {
        StompParticles.Stop();
    }

    public void PlayStompParticles()
    {
        StompParticles.Play();
    }
    void OnPoint(InputValue pointValue)
    {
        pointVector = pointValue.Get<Vector2>();
    }

    void OnMove(InputValue movementValue)
    {
        //set movement direction to input
        movementVector = new Vector3(movementValue.Get<Vector2>().x, 0, movementValue.Get<Vector2>().y);

        isMoving = movementVector.magnitude > 0;
        print(movementVector.magnitude);
    }

    void OnRotate(InputValue rotateValue)
    {
        if (!IsGrounded())
        {
            rotateVector = new Vector3(rotateValue.Get<Vector2>().y * 45, transform.rotation.eulerAngles.y,
                -rotateValue.Get<Vector2>().x * 45);
            Debug.Log(rotateVector);
        }
        else
            rotateVector = transform.rotation.eulerAngles; //new Vector3(0, 0, 0);

        //Debug.Log(rotateVector);
    }

    void OnJump(InputValue rotateValue)
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * _jumpSpeed);
            AudioManager.Instance.PlaySFX("Jump");
        }
    }


    public void OnFire(InputValue fireValue)
    {
        IsDashing = false;
        isFiring = fireValue.isPressed;
        StopStompParticles();
    }

    public void OnDash()
    {
        if(CurrentDashCooldown == 0f)
        {
            AudioManager.Instance.PlaySFX("Dash");
            DashParticles.Play();
            Invoke("StopDashParticles", 1);
            StopStompParticles();
            rb.velocity = Vector3.zero;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (isMoving)
                rb.AddForce(cameraRelevantMovementVector * _dashSpeed, ForceMode.Impulse);
            else
                rb.AddForce(Camera.main.transform.forward * _dashSpeed, ForceMode.Impulse);
            StartCoroutine(DashCooldownCounter());
        }
        else
        {
            print("can't dash, still on cooldown");
        }
        
    }

    void StopDashParticles()
    {
        DashParticles.Stop();
    }

    public void OnStomp()
    {
        if(!IsGrounded())
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * _stompDownwardForce, ForceMode.Impulse);          
            PlayStompParticles();
        }
    }

    public void OnMenu()
    {
        FindObjectOfType<GunModManagerUI>().ToggleModMenu();
    }

    public bool IsGrounded()
    {
        bool output = Physics.Raycast(transform.position, -Vector3.up, cr.bounds.extents.y+ 0.1f);
        if(output)
            Debug.DrawRay(transform.position, Vector3.up, Color.green, 10f);
        return output;
    }

    private void UpdateCameraRelevantVectors()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 newMovementZ = movementVector.z * cameraForward;
        Vector3 newMovementX = movementVector.x * cameraRight;
        Vector3 newRotationZ = rotateVector.z * transform.forward.normalized;
        Vector3 newRotationX = rotateVector.x * transform.right.normalized;

        cameraRelevantMovementVector = newMovementX + newMovementZ;
        cameraRelevantRotateVector = newRotationX + newRotationZ;
    }

    public void Die()
    {
        Debug.Log("Player has died");
        FindObjectOfType<DeathScreen>().ShowDeathScreen();
    }

    IEnumerator DashCooldownCounter()
    {
        CurrentDashCooldown = _dashCooldown;
        IsDashing = true;
        while(CurrentDashCooldown >= 0f)
        {
            CurrentDashCooldown -= Time.deltaTime;
            if(_dashCooldown - currentDashCooldown > _dashDuration)
                IsDashing = false;
            yield return null;
        }
        CurrentDashCooldown = 0f;
    }
}
