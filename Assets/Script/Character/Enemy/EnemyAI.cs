using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
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
    private Animator animPlayer;

    private Transform enemy;
    private Animator animator;
    private EnemyStat enemyStat;

    public float attackDis = 2.0f;
    public float traceDis = 8.0f;

    private MoveAgent moveAgent;
    private EnemyAttack enemyAtk;

    private readonly int hashMove = Animator.StringToHash("isMove");
    private readonly int hashSpeed = Animator.StringToHash("speed");
    private readonly int hashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animPlayer = player.Find("RPG-Character").GetComponent<Animator>();
        enemy = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        enemyAtk = GetComponent<EnemyAttack>();
        animator = GetComponent<Animator>();
    } 

    private void OnEnable()
    {
        enemyStat = GetComponent<EnemyStat>();
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        while(enemyStat.isAlive)
        {
            yield return new WaitForSeconds(0.3f);
            switch(state)
            {
                case State.PATROL:
                    enemyAtk.isAtk = false;
                    moveAgent.patrolling = true;
                    moveAgent.tracing = false;
                    animator.SetBool(hashMove, true);
                    break;
                case State.TRACE:
                    enemyAtk.isAtk = false;
                    moveAgent.tracing = true;
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
                    animator.SetTrigger(hashDie);
                    enemyStat.Dead();
                    break;
            }
        }
    }

    IEnumerator CheckState()
    {
        while(enemyStat.isAlive)
        {
            if (state == State.DIE) yield break;

            float dis = Vector3.Distance(player.position, enemy.position);

            if (dis <= attackDis) state = State.ATTACK;
            else if (dis <= traceDis) state = State.TRACE;
            else state = State.PATROL;

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Update()
    {
        animator.SetFloat(hashSpeed, moveAgent.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Item"))
        {
            if (animPlayer.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                Debug.Log("[Trigger-MonsterAI]" + this.gameObject.name);
                state = State.DIE;
            }
        }
    }
}
