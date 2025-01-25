using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Vector2 movementVector;

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
    }

    void OnMove(InputValue movementValue)
    {
        //set movement direction to input
        movementVector = new Vector3(movementValue.Get<Vector2>().x, 0, movementValue.Get<Vector2>().y);
    }


    public void OnFire()
    {
        modsHandler.FireWeapon();
    }
}
