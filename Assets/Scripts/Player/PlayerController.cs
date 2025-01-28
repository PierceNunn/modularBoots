using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Vector3 movementVector;
    private Vector3 rotateVector;

    private Rigidbody rb;
    private PlayerModsHandler modsHandler;
    private PlayerResources pr;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        modsHandler = gameObject.GetComponent<PlayerModsHandler>();
        pr = gameObject.GetComponent<PlayerResources>();
    }

    public void FixedUpdate()
    {
        rb.AddForce(movementVector * _movementSpeed); //make player move
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotateVector.x, 0, rotateVector.z), 0.5f);

        print(IsGrounded());
        if (IsGrounded())
            pr.RefillAmmo();
    }

    void OnMove(InputValue movementValue)
    {
        //set movement direction to input
        movementVector = new Vector3(movementValue.Get<Vector2>().x, 0, movementValue.Get<Vector2>().y);
    }

    void OnRotate(InputValue rotateValue)
    {
        rotateVector = new Vector3(rotateValue.Get<Vector2>().y * 45, 0, -rotateValue.Get<Vector2>().x * 45);
    }


    public void OnFire()
    {
        modsHandler.FireWeapon();
    }

    public void OnMenu()
    {
        FindObjectOfType<GunModManagerUI>().ToggleModMenu();
    }

    public bool IsGrounded()
    {
        bool output = Physics.Raycast(transform.position, -Vector3.up, 0.8f);
        if(output)
            Debug.DrawRay(transform.position, Vector3.up, Color.green, 10f);

        return output;
    }
}
