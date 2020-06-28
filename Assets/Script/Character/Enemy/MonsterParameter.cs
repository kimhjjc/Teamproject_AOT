using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParameter : Stat
{
    private int damage;
    
    void Start()
    {
        isAlive = true;
        health = 50;
        damage = 20;
    }
    
    void Update()
    {
        Dead();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("[Trigger-MonsterParameter]" + other.gameObject.name);
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
