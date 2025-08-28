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
            data = new UserData("", "ÀÌ½ÂÀ²", "", 50000, 100000);
        }
    }
}
