using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public static Currency instance;

    public Text currency1, currency2;

    public int mineralCurr1 = 0, mineralCurr2 = 0;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        currency1.text = mineralCurr1.ToString("Mineral 1 : ");
        currency2.text = mineralCurr2.ToString("Mineral 2 : ");
    }
}