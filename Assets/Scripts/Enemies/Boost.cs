using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] private float boostForce;
    [SerializeField] private float healing;
    private PlayerResources pr;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            other.GetComponent<PlayerController>().StopStompParticles();
            //cancel falling momentum
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, boostForce, 0), ForceMode.Impulse);
            other.GetComponent<HealthSystem>().ReceiveHealing(healing);
            pr = other.GetComponent<PlayerResources>();
            pr.RefillAmmo();
            Destroy(gameObject);
        }

    }
}
