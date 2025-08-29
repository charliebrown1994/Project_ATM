using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class LogInPanel : MonoBehaviour
{
    [Header("SighUpInfo")]
    [SerializeField] TMP_InputField idText;
    [SerializeField] TMP_InputField nameText;
    [SerializeField] TMP_InputField PWText;
    [SerializeField] TMP_InputField confirmText;
    [SerializeField] Button sighUpBtn;
    [SerializeField] GameObject erroPanel;
    [SerializeField] GameObject sighUpPanel;

    [Header("SighInInfo")]
    [SerializeField] TMP_InputField logInIdText;
    [SerializeField] TMP_InputField logInPWText;
    [SerializeField] Button logInBtn;

    [Header("Menu")]
    [SerializeField] GameObject popupBank;
    [SerializeField] GameObject popupLogin;

    private void Start()
    {
        InitializeBtn();
    }

    public void ClickSighUpBtn()
    {
        if (string.IsNullOrEmpty(idText.text) || string.IsNullOrEmpty(nameText.text) || string.IsNullOrEmpty(PWText.text) || string.IsNullOrEmpty(confirmText.text))
        {
            erroPanel.SetActive(true);
        }
        else if (!string.Equals(PWText.text, confirmText.text))
        {
            erroPanel.SetActive(true);
        }
        else
        {
            GameManager.Instance.SighUpData(idText.text, nameText.text, PWText.text);
            sighUpPanel.SetActive(false);
        }
    }

    public void ClickSighInBtn()
    {
        if (string.IsNullOrEmpty(logInIdText.text) || string.IsNullOrEmpty(logInPWText.text))
        {
            erroPanel.SetActive(true);
        }
        else
        {
            UserData temp = SaveManager.LoadUserData(logInIdText.text);
            if (temp == null)
            {
                erroPanel.SetActive(true);
            }
            else if (!string.Equals(temp.userPW, logInPWText.text))
            {
                erroPanel.SetActive(true);
            }
            else
            {
                GameManager.Instance.SighInData(temp);
                popupLogin.SetActive(false);
                popupBank.SetActive(true);
            }
        }
    }

    public void InitializeBtn()
    {
        sighUpBtn.onClick.AddListener(ClickSighUpBtn);
        logInBtn.onClick.AddListener(ClickSighInBtn);
    }
}
