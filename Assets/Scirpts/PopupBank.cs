using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [Header("BaseInfo")]
    [SerializeField] TextMeshProUGUI balanceText;
    [SerializeField] TextMeshProUGUI cashText;
    [SerializeField] TextMeshProUGUI nameText;

    [Header("ButtonAction")]
    [SerializeField] Button depositBtn;
    [SerializeField] Button withdrawalBtn;
    [SerializeField] Button depositBackBtn;
    [SerializeField] Button withdrawalBackBtn;
    [SerializeField] GameObject atmMenu;
    [SerializeField] GameObject depositMenu;
    [SerializeField] GameObject withdrawalMenu;

    //[Header("SighUpInfo")]

    //[Header("SighInInfo")]

    private void Start()
    {
        OnClickBtn();
    }

    public void Refresh(UserData data)
    {
        nameText.text = data.userName;
        balanceText.text = data.balance.ToString("N0");
        cashText.text = data.cash.ToString("N0");
    }

    public void EnterMenu(bool isDeposit)
    {
        atmMenu.SetActive(false);
        
        if (isDeposit)
        {
            depositMenu.SetActive(true);
        }
        else
        {
            withdrawalMenu.SetActive(true);
        }
    }

    public void ExitMenu() 
    {
        atmMenu.SetActive(true);
        depositMenu.SetActive(false);
        withdrawalMenu.SetActive(false);
    }

    public void OnClickBtn()
    {
        depositBtn.onClick.AddListener(() => EnterMenu(true));
        withdrawalBtn.onClick.AddListener(() => EnterMenu(false));
        depositBackBtn.onClick.AddListener(ExitMenu);
        withdrawalBackBtn.onClick.AddListener(ExitMenu);
    }
}
