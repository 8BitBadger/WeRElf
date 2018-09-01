using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector]
    public Vector2 target;

    private void Start()
    {
        Vector3 dir = new Vector3(target.x, target.y, 0) - transform.position;
        float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    private void Update()
    {
        Vector3 dir = new Vector3(target.x, target.y, 0) - transform.position;
        dir = dir.normalized * Time.deltaTime * 5f;
        transform.position += dir; 

        if (Vector2.Distance(target, transform.position) < 1f)
        {
            Destroy(gameObject);
        }
    }
}
