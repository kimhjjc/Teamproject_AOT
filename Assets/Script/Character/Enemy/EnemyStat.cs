
using UnityEngine;

public class EnemyStat : Stat
{
    private int damage;

    private void Awake()
    {
        isAlive = true;
        health = 1;
        damage = 20;
    }

    void Update()
    {
        if(!isAlive) Destroy(this.gameObject, 2.0f);
    }
    
    public override void Dead()
    {
        isAlive = false;
    }
}