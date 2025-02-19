using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : EnemyBehavior
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float attackDamage;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    new void Start()
    {
        rb = GetComponent<Rigidbody>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAggro)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void JumpAtTarget()
    {
        Debug.Log("jumped");
        Vector3 jumpDirection = (target.transform.position - transform.position);
        jumpDirection.y += 5;
        jumpDirection.Normalize();
        rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(attackDamage);
        }
    }

    public override IEnumerator Attack()
    {
        while (isAggro)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, target.transform.position - transform.position, out hit,
                attackDistance);
            Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
            if(hit.collider != null && hit.collider.tag == "Player")
            {
                Debug.Log("raycast hit");
                rb.velocity = Vector3.zero;
                JumpAtTarget();
            }

            yield return new WaitForSeconds(EnemyStats.attackInterval);
        }
    }
}
