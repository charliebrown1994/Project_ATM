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

    public UserData(string targetID, string targetName, string targetPW)
    {
        userID = targetID;
        userName = targetName;
        userPW = targetPW;
        balance = 50000;
        cash = 100000;
    }
}
