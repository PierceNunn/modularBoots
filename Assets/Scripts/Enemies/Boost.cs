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
        other.GetComponent<Rigidbody>().AddForce(new Vector3(0, boostForce, 0), ForceMode.Impulse);
        pr = other.GetComponent<PlayerResources>();
        pr.RefillAmmo();
        Destroy(gameObject);
    }
}
