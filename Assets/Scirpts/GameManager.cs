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

    private UserData _userData;
    public UserData userData
    {
        get { return _userData; }
        set { _userData = value; }
    }

    private void Awake()
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
}
