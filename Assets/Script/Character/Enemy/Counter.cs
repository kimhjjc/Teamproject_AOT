using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private int count;
    public int killcount
    {
        get { return transform.childCount - count; }
    }

    public GameObject countText;
    public GameObject destroyCountText;

    private void Start()
    {
        count = transform.childCount;
        SetCount(countText, count);
    }

    private void Update()
    {
        if (count != transform.childCount) UpdateCount();
    }

    public void SetCount(GameObject go, int count)
    {
        go.transform.Find("count").GetComponent<Text>().text = count.ToString();
    }

    public void UpdateCount()
    {
        SetCount(countText, --count);
        SetCount(destroyCountText, transform.childCount - count);
    }
}
