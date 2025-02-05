using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, CanDie
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _dashSpeed;

    private Vector3 movementVector;
    private Vector3 rotateVector;
    private Vector3 cameraRelevantMovementVector;
    private Vector3 cameraRelevantRotateVector;
    private bool isFiring = false;
    private bool isMoving = false;

    private Rigidbody rb;
    private Collider cr;
    private PlayerModsHandler modsHandler;
    private PlayerResources pr;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cr = gameObject.GetComponent<Collider>();
        modsHandler = gameObject.GetComponent<PlayerModsHandler>();
        pr = gameObject.GetComponent<PlayerResources>();
    }

    public void FixedUpdate()
    {
        UpdateCameraRelevantVectors();
        //Debug.Log(movementVector);

        rb.AddForce(cameraRelevantMovementVector * _movementSpeed); //make player move
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraRelevantRotateVector), 0.5f);
        if (IsGrounded() && modsHandler.NoPendingCooldown)
            pr.RefillAmmo();
        if(isFiring)
            modsHandler.FireWeapon();
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
            rotateVector = new Vector3(rotateValue.Get<Vector2>().y * 45, 0, -rotateValue.Get<Vector2>().x * 45);
        else
            rotateVector = new Vector3(0, 0, 0);
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
        isFiring = fireValue.isPressed;
    }

    public void OnDash()
    {
        if(isMoving)
            rb.AddForce(movementVector * _dashSpeed, ForceMode.Impulse);
        else
            rb.AddForce(Camera.main.transform.forward * _dashSpeed, ForceMode.Impulse);
        print("Dash");
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
        Vector3 newRotationZ = rotateVector.z * cameraForward;
        Vector3 newRotationX = rotateVector.x * cameraRight;

        cameraRelevantMovementVector = newMovementX + newMovementZ;
        cameraRelevantRotateVector = newRotationX + newRotationZ;
    }

    public void Die()
    {
        Debug.Log("Player has died");
    }
}
