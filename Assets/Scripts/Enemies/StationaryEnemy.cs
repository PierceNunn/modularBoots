using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : EnemyBehavior
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawnPoint;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAggro)
        {
            transform.LookAt(target);
        }
    }

    public void ShootAtTarget()
    {
        //Debug.Log("shot at target");
        GameObject proj = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        //proj.GetComponent<ProjectileController>().ProjectileSpawner = gameObject;
        //proj.transform.LookAt(target);
        Debug.Log(proj.transform.forward);
        proj.GetComponent<Rigidbody>().velocity = (proj.transform.forward) * 5f;
        //proj.GetComponent<ProjectileController>().Fire();

    }

    public override IEnumerator Attack()
    {
        while (isAggro)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, target.transform.position - transform.position, out hit);
            Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
            if(hit.collider != null && hit.collider.tag == "Player")
            {
                ShootAtTarget();
            }
            
            yield return new WaitForSeconds(enemyStats.attackInterval);
        }
    
    }
}
