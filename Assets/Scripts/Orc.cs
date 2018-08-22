using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    //The stats of the orc
    private int health;
    public int damage = 1;
    public int attackInterval;
    float lastAttackTime = 0;
    public int attackRange;

    //If the orc fires an arrow
    public bool fireArrow;

    //The object for the arrow
    public GameObject arrow_go;
    public GameObject attack_image;

    Transform target;

    public int Health
    {
        get{return health;}
    }

    //We want him to walk towards the mother tree but if he is atacked then he will attack the tree that is attacking him.

    public void SetTarget(Transform _transform)
    {
        target = _transform;
    }

    public void TakeDamage()
    {
        health -= 1;
    }

    void Attack()
    {
        target.GetComponent<Tree>().TakeDamage();
    }

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("MotherTree").transform;
        }
        else
        {
            if (Vector2.Distance(transform.position, target.position) > attackRange)
            {
                Vector2 dir = target.position - transform.position;
                dir = dir.normalized * Time.deltaTime * 0.5f;
                transform.position = new Vector2(transform.position.x + dir.x, transform.position.y + dir.y);  
            }
            else
            {
                if ((Time.time - lastAttackTime) > attackInterval)
                {
                    lastAttackTime = Time.time;
                    if (fireArrow)
                    {
                        FireArrow();
                    }
                    else
                    {
                        AttackEffect();
                    }
                    Attack();
                }
            }
        }
        //Now to attack the tree hahaha
    }

    void FireArrow()
    {
        GameObject arrow = Instantiate(arrow_go, transform.position, Quaternion.Euler(target.position));
        arrow.GetComponent<Arrow>().target = target.position;
    }

    void AttackEffect()
    {

    }
}
