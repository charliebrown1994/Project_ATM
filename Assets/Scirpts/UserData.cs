using System.Collections;
using System.Collections.Generic;
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
