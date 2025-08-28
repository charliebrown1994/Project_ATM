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
        saveManager.SavePath();
        saveManager.LoadUserData(userData);
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
}
