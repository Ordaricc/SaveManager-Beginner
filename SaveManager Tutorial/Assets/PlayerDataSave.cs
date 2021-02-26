using UnityEngine;

public class PlayerDataSave : MonoBehaviour
{
    private const string playerStatsFilePath = "playerStatsFilePath";
    [SerializeField] private PlayerStats playerstats;

    public void SavePlayerStats()
    {
        SaveManager.SaveData(playerStatsFilePath, playerstats);
    }

    public void LoadPlayerStats()
    {
        if (SaveManager.LoadData(playerStatsFilePath, out PlayerStats dataLoaded))
            playerstats = dataLoaded;
    }
}

[System.Serializable]
public class PlayerStats
{
    public string playerName;
    public int health;
}