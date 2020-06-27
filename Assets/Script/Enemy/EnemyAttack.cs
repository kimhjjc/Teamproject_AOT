using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private AudioManager audioMaster;
    private Animator animator;
    private Transform player;
    private Transform enemy;
    
    private readonly int hashAtk = Animator.StringToHash("isAtk");
    private readonly float damping = 10.0f;

    public bool isAtk = false;
    public AudioClip atkSfx;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        audioMaster = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (isAtk) Attack();
        Quaternion rot = Quaternion.LookRotation(player.position - enemy.position);
        enemy.rotation = Quaternion.Slerp(enemy.rotation, rot, Time.deltaTime * damping);
    }

    private void Attack()
    {
        animator.SetTrigger(hashAtk);
        audioMaster.Play(audioMaster.monster, "attackToMonster");
        isAtk = false;
    }
}
