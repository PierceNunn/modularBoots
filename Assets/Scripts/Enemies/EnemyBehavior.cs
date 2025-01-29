using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Enemy enemyStats;
    [SerializeField] private Transform target;

    private bool isAggro;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AggroCheck()
    {
        while (true)
        {
            if(Vector3.Distance(this.transform.position, target.position) < enemyStats.AggroDistance)
            {
                isAggro = true;
            }
            else
            {
                isAggro = false;
            }
            yield return new WaitForSeconds(.25f);
        }
    }
}
