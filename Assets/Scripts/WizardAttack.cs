using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator animator;
    private WizardMovement wizardMovement;
    private float cooldownTimer = Mathf.Infinity;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        wizardMovement = GetComponent<WizardMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
       if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && wizardMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("isAttacking");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<WizardProjectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
