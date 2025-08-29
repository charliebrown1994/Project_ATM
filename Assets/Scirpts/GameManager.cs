using System.Collections.Generic;
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

    [SerializeField] private UserData _currentUserData;
    public UserData currentUserData
    {
        get { return _currentUserData; }
    }

    public SaveManager saveManager;

    public PopupBank popupBank;

    private void Awake()
    {
        InitializeSingleton();
        saveManager = new SaveManager();
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

    public void SighUpData(string id, string name, string pw)
    {
        
    }

    public void SighInData(string id, string pw)
    {

    }

    public void AddBtn(int amount)
    {
        if (currentUserData.cash >= amount)
        {
            currentUserData.balance += amount;
            currentUserData.cash -= amount;

            saveManager.SaveUserData(currentUserData);
            popupBank.Refresh(currentUserData);
        }
        else
        {
            popupBank.popupPanel.SetActive(true);
        }
    }

    public void SubBtn(int amount)
    {
        if (currentUserData.balance >= amount)
        {
            currentUserData.balance -= amount;
            currentUserData.cash += amount;

            saveManager.SaveUserData(currentUserData);
            popupBank.Refresh(currentUserData);
        }
        else
        {
            popupBank.popupPanel.SetActive(true);
        }
    }
}
