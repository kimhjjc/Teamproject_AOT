/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Stat
{
    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    private Animator animator;
    public GameObject gameover;
    private Counter counter;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        counter = GameObject.FindObjectOfType<Counter>();
        isAlive = true;
        health = 100;
    }

    void Update()
    {
        Dead();
    }

    public override void Dead()
    {
        if (health <= 0 && isAlive)
        {
            animator.Play("Death");
            isAlive = false;
            gameover.SetActive(true);
            counter.SetCount(gameover, counter.killcount);
        }
    }
}
