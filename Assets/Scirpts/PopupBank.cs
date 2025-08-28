using System;
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
    [SerializeField] Button sentBtn;
    [SerializeField] Button depositBackBtn;
    [SerializeField] Button withdrawalBackBtn;
    [SerializeField] Button sentBackBtn;
    [SerializeField] GameObject atmMenu;
    [SerializeField] GameObject depositMenu;
    [SerializeField] GameObject withdrawalMenu;
    [SerializeField] GameObject sentMenu;

    [Header("AddSubAction")]
    [SerializeField] Button firstAddBtn;
    [SerializeField] Button secondAddBtn;
    [SerializeField] Button thirdAddBtn;
    [SerializeField] Button addEnterBtn;

    [SerializeField] Button firstSubBtn;
    [SerializeField] Button secondSubBtn;
    [SerializeField] Button thirdSubBtn;
    [SerializeField] Button subEnterBtn;

    [Header("InputNumber")]
    [SerializeField] TMP_InputField addInput;
    [SerializeField] TMP_InputField subInput;

    [Header("PopupPanel")]
    [SerializeField] public GameObject popupPanel;

    //[Header("SentCash")]

    public int firstAmount = 10000;
    public int secondAmount = 30000;
    public int thirdAmount = 50000;

    public int addInputAmount;
    public int subInputAmount;

    public enum BankMenuType
    {
        Deposit,    // 입금
        Withdrawal, // 출금
        Sent        // 송금
    }

    private void Start()
    {
        InitializeBtn();
    }

    public void Refresh(UserData data)
    {
        nameText.text = data.userName;
        balanceText.text = data.balance.ToString("N0");
        cashText.text = data.cash.ToString("N0");
    }

    public void EnterMenu(BankMenuType type)
    {
        atmMenu.SetActive(false);
        depositMenu.SetActive(type == BankMenuType.Deposit);
        withdrawalMenu.SetActive(type == BankMenuType.Withdrawal);
        sentMenu.SetActive(type == BankMenuType.Sent);
    }

    public void ExitMenu() 
    {
        atmMenu.SetActive(true);
        depositMenu.SetActive(false);
        withdrawalMenu.SetActive(false);
        sentMenu.SetActive(false);
    }

    public void SaveAddInputAmount(string text)
    {
        addInputAmount = int.Parse(text);
    }

    public void SaveSubInputAmount(string text)
    {
        subInputAmount = int.Parse(text);
    }

    private void Add(int amount)
    {
        GameManager.Instance.AddBtn(amount);
    }
    private void Sub(int amount)
    {
        GameManager.Instance.SubBtn(amount);
    }

    public void InitializeBtn()
    {
        depositBtn.onClick.AddListener(() => EnterMenu(BankMenuType.Deposit));
        withdrawalBtn.onClick.AddListener(() => EnterMenu(BankMenuType.Withdrawal));
        sentBtn.onClick.AddListener(() => EnterMenu(BankMenuType.Sent));
        depositBackBtn.onClick.AddListener(ExitMenu);
        withdrawalBackBtn.onClick.AddListener(ExitMenu);
        sentBackBtn.onClick.AddListener(ExitMenu);

        firstAddBtn.onClick.AddListener(() => Add(firstAmount));
        secondAddBtn.onClick.AddListener(() => Add(secondAmount));
        thirdAddBtn.onClick.AddListener(() => Add(thirdAmount));

        firstSubBtn.onClick.AddListener(() => Sub(firstAmount));
        secondSubBtn.onClick.AddListener(() => Sub(secondAmount));
        thirdSubBtn.onClick.AddListener(() => Sub(thirdAmount));

        addEnterBtn.onClick.AddListener(() => Add(addInputAmount));
        subEnterBtn.onClick.AddListener(() => Sub(subInputAmount));
        
        addInput.onEndEdit.AddListener(SaveAddInputAmount);
        subInput.onEndEdit.AddListener(SaveSubInputAmount);

    }
}
