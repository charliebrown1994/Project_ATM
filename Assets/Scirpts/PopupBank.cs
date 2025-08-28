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

    //[Header("SighUpInfo")]

    //[Header("SighInInfo")]

    //[Header("SentCash")]

    public int firstAmount = 10000;
    public int secondAmount = 30000;
    public int thirdAmount = 50000;

    public int addInputAmount;
    public int subInputAmount;

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
        depositBtn.onClick.AddListener(() => EnterMenu(true));
        withdrawalBtn.onClick.AddListener(() => EnterMenu(false));
        depositBackBtn.onClick.AddListener(ExitMenu);
        withdrawalBackBtn.onClick.AddListener(ExitMenu);

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
