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

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        modsHandler = gameObject.GetComponent<PlayerModsHandler>();
    }

    public void FixedUpdate()
    {
        rb.AddForce(movementVector * _movementSpeed); //make player move
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotateVector.x, 0, rotateVector.z), 0.5f);
    }

    void OnMove(InputValue movementValue)
    {
        //set movement direction to input
        movementVector = new Vector3(movementValue.Get<Vector2>().x, 0, movementValue.Get<Vector2>().y);
    }

    void OnRotate(InputValue rotateValue)
    {
        rotateVector = new Vector3(rotateValue.Get<Vector2>().x * 45, 0, rotateValue.Get<Vector2>().y * 45);
    }


    public void OnFire()
    {
        modsHandler.FireWeapon();
    }
}
