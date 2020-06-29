
using UnityEngine;

public class EnemyStat : Stat
{
    private int damage;

    private void Awake()
    {
        isAlive = true;
        health = 100;
        damage = 2;
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