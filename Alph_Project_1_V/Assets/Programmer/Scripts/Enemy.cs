using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]float EnemyMoveSpeedValue;

    [SerializeField] Transform Waypoint, Waypoint1;

    [SerializeField] GameObject AttackHitBox;

    int health;

    float EnemyPatrolSpeed;

    Animator animator;

    enum EnemyState {
        Idle,
        Patrol,
        Dash,
        Attack,
    }
    private EnemyState enemyState;

    void Start()
    {
        AttackHitBox.SetActive(false);
        animator = GetComponent<Animator>();
        health = 100;
        //enemyState = EnemyState.Patrol;
        EnemyPatrolSpeed = EnemyMoveSpeedValue;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            enemyState = EnemyState.Attack;
        }
        if (Input.GetMouseButtonUp(0))
        {
            enemyState = EnemyState.Idle;
        }
        //Switch case for switching between states
        switch (enemyState)
        {
            case EnemyState.Idle:
                {
                    Idle();
                    break;
                }

            case EnemyState.Patrol:
                {
                    patrol();
                    //animator.SetBool("Running", true);
                    break;
                }
            case EnemyState.Dash:
                {
                    Dash();
                    break;
                }
            case EnemyState.Attack:
                {
                    Attack();
                    AttackAnimation();
                    break;
                }

            default : Debug.Log("Wrong state");
                break;
        }

    }

   

    void Heal(int Amt)
    {
        health += Amt;
    }

    void TakeDamage(int dmg)
    {
        health -= dmg;
    }
    private void Idle()
    {

    }
    void patrol()
    {
        //Fix this, It'd not working!
        this.transform.Translate(Vector2.right * EnemyPatrolSpeed * Time.deltaTime);
        if(this.transform.position.x >= Waypoint1.transform.position.x)
        {
            EnemyPatrolSpeed = -EnemyPatrolSpeed;
        }
        else if(this.transform.position.x <= Waypoint.transform.position.x)
        {
            EnemyPatrolSpeed = EnemyMoveSpeedValue;
        }
    }
    private void Dash()
    {

    }
    private void Attack()
    {

    }
    private void AttackAnimation()
    {
        int index = UnityEngine.Random.Range(1, 6);
        animator.Play("Enemy_attack"+ index);
        StartCoroutine("DoAttack");
    }
    IEnumerator DoAttack()
    {
        AttackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        AttackHitBox.SetActive(false);
    }
}

/*
     bool movingRight = true;

    [SerializeField] float EnemyWalkSpeed;
    [SerializeField] float Distance;

    [SerializeField] Transform GroundDetection;
    transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo =
            Physics2D.Raycast(groundDetect.position, Vector2.down, distance);
        if(groundInfo.collider == false)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
 */

