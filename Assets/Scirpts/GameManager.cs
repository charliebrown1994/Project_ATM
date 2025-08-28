using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    // ���ξ ����ϴ� ����
    public static GameManager Instance
    // �ܺο��� ����ϴ� ����
    {
        get 
        {
            if (_instance == null) // ���� �ν��Ͻ��� ���ٸ�
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
                // ���ο� GameObject�� ���� GameManager�� ���Դϴ�.
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
        // ���� �ν��Ͻ��� ������ �ڽ�(this)�� �ν��Ͻ��� ����
        if (_instance == null)
        {
            _instance = this;
            // �� ��ȯ �� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �̹� �ν��Ͻ��� ������ �ڽ��� �ı� �� �ߺ� ���� ����
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
