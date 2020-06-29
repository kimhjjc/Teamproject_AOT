/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private PlayerStats playerstats;

    private GameObject[] heartContainers;
    private Image[] heartFills;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        playerstats = GameObject.FindObjectOfType<PlayerStats>();
        // Should I use lists? Maybe :)
        heartContainers = new GameObject[(int)playerstats.MaxTotalHealth];
        heartFills = new Image[(int)playerstats.MaxTotalHealth];

        playerstats.onHealthChangedCallback += UpdateHeartsHUD;
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < playerstats.MaxHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < playerstats.Health)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (playerstats.Health % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(playerstats.Health);
            heartFills[lastPos].fillAmount = playerstats.Health % 1;
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < playerstats.MaxTotalHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
