using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLifeController : MonoBehaviour
{
    [SerializeField] private float _totalLifetime;

    public void Start()
    {
        if(_totalLifetime > 0f)
        {
            StartCoroutine(DeleteAfterLifetime());
        }
    }

    IEnumerator DeleteAfterLifetime()
    {
        yield return new WaitForSeconds(_totalLifetime);
        Destroy(gameObject);
    }
}
