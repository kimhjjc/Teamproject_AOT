using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    public State state = State.PATROL;

    private Transform player;
    private Transform enemy;
    private Animator animator;

    public float attackDis = 2.0f;
    public float traceDis = 8.0f;

    public bool isDie = false;

    private MoveAgent moveAgent;
    private EnemyAttack enemyAtk;

    private readonly int hashMove = Animator.StringToHash("isMove");
    private readonly int hashSpeed = Animator.StringToHash("speed");
    private readonly int hashDemage = Animator.StringToHash("isDemage");

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        enemyAtk = GetComponent<EnemyAttack>();
        animator = GetComponent<Animator>();
    } 

    private void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            switch(state)
            {
                case State.PATROL:
                    enemyAtk.isAtk = false;
                    moveAgent.patrolling = true;
                    animator.SetBool(hashMove, true);
                    break;
                case State.TRACE:
                    enemyAtk.isAtk = false;
                    moveAgent.traceTarget = player.position;
                    animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    moveAgent.Stop();
                    animator.SetBool(hashMove, false);
                    if (enemyAtk.isAtk == false) enemyAtk.isAtk = true;
                    break;
                case State.DIE:
                    moveAgent.Stop();
                    animator.SetBool(hashDemage, true);
                    break;
            }
        }
    }

    IEnumerator CheckState()
    {
        while(!isDie)
        {
            if (state == State.DIE) yield break;
            float dis = Vector3.Distance(player.position, enemy.position);
            if (dis <= attackDis)
            {
                state = State.ATTACK;
            }
            else if (dis <= traceDis)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Update()
    {
        animator.SetFloat(hashSpeed, moveAgent.speed);
    }
}
