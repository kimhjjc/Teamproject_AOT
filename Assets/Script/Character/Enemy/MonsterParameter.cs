using UnityEngine;

public class MonsterParameter : MonoBehaviour
{
    private PlayerStats playerstats;
    private int damage;

    private void Start()
    {
        playerstats = GameObject.FindObjectOfType<PlayerStats>();
    }
    void Update()
    {
        playerstats.Dead();
        if(Input.GetKeyUp(KeyCode.O))
            playerstats.TakeDamage(damage);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("[Trigger-MonsterParameter]" + other.gameObject.name + "=" + damage);
            //PlayerStats.Instance.TakeDamage(damage);
        }
    }
}
