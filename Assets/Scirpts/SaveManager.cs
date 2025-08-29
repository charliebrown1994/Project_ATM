using System.IO;
using UnityEngine;

[System.Serializable]

public class SaveManager
{
    //public string path;

    //public void SavePath()
    //{
    //    path = Path.Combine(Application.persistentDataPath, "userData.json");
    //}
    public void SaveUserData(UserData data)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{data.userID}.json");
        string save = JsonUtility.ToJson(data);
        File.WriteAllText(path, save);

    }
    public UserData LoadUserData(string id)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{id}.json");

        if (File.Exists(path))
        {
            string load = File.ReadAllText(path);
            return JsonUtility.FromJson<UserData>(load);
        }
        else
        {
            //data = new UserData("", "ÀÌ½ÂÀ²", "", 50000, 100000);
            return null;
        }
    }
}
