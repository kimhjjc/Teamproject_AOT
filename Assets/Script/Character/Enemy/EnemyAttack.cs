using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform player;
    private Transform enemy;
    private EnemyStat enemyStat;

    private AudioManager audioMaster;
    private Animator animator;

    private readonly int hashAtk = Animator.StringToHash("isAtk");
    private readonly float damping = 10.0f;
    
    public bool isAtk = false;
    public AudioClip atkSfx;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = GetComponent<Transform>();
        enemyStat = GetComponent<EnemyStat>();
        animator = GetComponent<Animator>();
        audioMaster = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (enemyStat.isAlive == false) return;
        if (isAtk) Attack();
    }

    private void Attack()
    {
        animator.SetTrigger(hashAtk);
        audioMaster.Play(audioMaster.monster, "attackToMonster");
        isAtk = false;
    }
}
