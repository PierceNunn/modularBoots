using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour, CanDie
{
    [SerializeField] protected Enemy enemyStats;
    [SerializeField] protected Transform target;
    [SerializeField] private GameObject boost;

    protected bool isAggro;
    
    // Start is called before the first frame update
    public void Start()
    {
        isAggro = false;
        target = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(AggroCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Debug.Log("enemy died");
        Instantiate(boost, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public IEnumerator AggroCheck()
    {
        while (true)
        {
            if(Vector3.Distance(this.transform.position, target.position) < enemyStats.aggroDistance)
            {
                if (isAggro == false)
                {
                    isAggro = true;
                    StartCoroutine(Attack());
                }  
            }
            else
            {
                isAggro = false;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public virtual IEnumerator Attack()
    {
        yield return new WaitForSeconds(enemyStats.attackInterval);
    }
}
