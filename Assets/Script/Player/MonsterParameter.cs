using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParameter : MonoBehaviour
{
    private int hp;
    private int damage;

    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        hp = 50;
        damage = 20;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            isAlive = false;

        if (!isAlive)
            Destroy(gameObject);
    }

    public void getDamage(int dmg)
    {
        hp -= dmg;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<Player>().Player_Hit_Damage(damage);
    }

}
