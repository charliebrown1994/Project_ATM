# 개인 프로젝트 : Project_ATM

이번 주 개인과제 "구현하지 못하면 나갈 수 없는 방"에서 ATM만들기를 선택해 구현해 보았습니다.

## 참고 이미지 :

https://github.com/user-attachments/assets/8869e825-fe12-4ae0-8b1b-750fd64b6f6d

##

### 필수 기능 :  (구현 완료)
<details>
<summary>펼쳐보기</summary> 
  
1. STEP 1. ATM 화면 구성 (PopupBank) ✅
    
2. STEP 2. 유저 데이터 제작 ✅
    
3. STEP 3. 게임 매니저 제작 ✅

4. STEP 4. 데이터와 UI 연동 ✅

5. STEP 5. 입금 UI 제작 ✅
    
6. STEP 6. 출금 UI 제작 ✅
    
7. STEP 7. 입출금 창 이동 ✅
    
8. STEP 8. 입금 기능 만들기 ✅

9. STEP 9. 출금 기능 만들기 ✅

10. STEP 10. 저장 및 로드 기능 만들기 ✅
  
</details>

##

## 구현 설명 :

<details>
<summary>스크립트 :</summary> 
  
1. 스크립트는 4개를 만들어서 각자의 역활을 정하고 주요 지능은 GameManager에서 제어할 수 있게 했습니다.
2. 데이터만 관리하는 UserData
```csharp
using UnityEngine;

[System.Serializable]

public class UserData
{
    [Header("UserInfo")]
    [SerializeField] public string userID;
    [SerializeField] public string userName;
    [SerializeField] public string userPW;
    [SerializeField] public int balance;
    [SerializeField] public int cash;

    public UserData(string targetID, string targetName, string targetPW, int targetBalance, int targetCash)
    {
        userID = targetID;
        userName = targetName;
        userPW = targetPW;
        balance = targetBalance;
        cash = targetCash;
    }
}
```
3. 저장을 담당하는 SaveManager
```csharp
using System.IO;
using UnityEngine;

[System.Serializable]

public class SaveManager
{
    public string path;

    public void SavePath()
    {
        path = Path.Combine(Application.persistentDataPath, "userData.json");
    }
    public void SaveUserData(UserData data)
    {
        string save = JsonUtility.ToJson(data);
        File.WriteAllText(path, save);

    }
    public void LoadUserData(ref UserData data)
    {
        if (File.Exists(path))
        {
            string load = File.ReadAllText(path);
            data = JsonUtility.FromJson<UserData>(load);
        }
        else
        {
            data = new UserData("", "이승율", "", 50000, 100000);
        }
    }
}
```
4. 주요기능을 담당하는 GameManager와 UI를 담당하는 PopupBank
```csharp
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    // 내부어서 사용하는 변수
    public static GameManager Instance
    // 외부에서 사용하는 변수
    {
        get 
        {
            if (_instance == null) // 만약 인스턴스가 없다면
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
                // 새로운 GameObject를 만들어서 GameManager를 붙입니다.
            }
            return _instance;
        }
    }

    [SerializeField] private UserData _userData;
    public UserData userData
    {
        get { return _userData; }
    }

    public SaveManager saveManager;

    public PopupBank popupBank;

    private void Awake()
    {
        InitializeSingleton();
        saveManager = new SaveManager();
        saveManager.SavePath();
        saveManager.LoadUserData(ref _userData);
        popupBank.Refresh(userData);
    }

    private void InitializeSingleton()
    {
        // 만약 인스턴스가 없으면 자신(this)을 인스턴스로 지정
        if (_instance == null)
        {
            _instance = this;
            // 씬 전환 시 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 인스턴스가 있으면 자신을 파괴 → 중복 생성 방지
            Destroy(gameObject);
        }
    }

    public void AddBtn(int amount)
    {
        if (userData.cash >= amount)
        {
            userData.balance += amount;
            userData.cash -= amount;

            saveManager.SaveUserData(userData);
            popupBank.Refresh(userData);
        }
        else
        {
            popupBank.popupPanel.SetActive(true);
        }
    }

    public void SubBtn(int amount)
    {
        if (userData.balance >= amount)
        {
            userData.balance -= amount;
            userData.cash += amount;

            saveManager.SaveUserData(userData);
            popupBank.Refresh(userData);
        }
        else
        {
            popupBank.popupPanel.SetActive(true);
        }
    }
}

```
```csharp
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

```
  
</details>


## 감사합니다!


