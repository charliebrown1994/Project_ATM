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

    //[Header("SighInInfo")]

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

    public void InitializeBtn()
    {
        sighUpBtn.onClick.AddListener(ClickSighUpBtn);
    }
}
