using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject machineStat;
    public GameObject moneyText; 

    private static MenuController instance;
    private int currentMoney = 0;

    void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        menuPanel.SetActive(false);
        machineStat.SetActive(false);
        UpdateMoneyDisplay();
    }

    public static void AddMoney(int amount)
    {
        if (instance == null) return;

        instance.currentMoney += amount;
        instance.UpdateMoneyDisplay();
    }

    private void UpdateMoneyDisplay()
    {
        moneyText.GetComponent<Text>().text = currentMoney.ToString();
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
    }

    public void OpenMachineStat()
    {
        machineStat.SetActive(true);
    }

    public void CloseMachineStat()
    {
        machineStat.SetActive(false);
    }
}