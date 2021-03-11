using UnityEngine;

public class PlayerDataSave : MonoBehaviour
{
    private const string playerStatsFilePath = "playerStatsFilePath";
    [SerializeField] private PlayerStats playerstats;

    private const string playerTransformFilePath = "playerTransformFilePath";
    [SerializeField] private PlayerTransformData playerTransformData;

    public void SavePlayerStats()
    {
        SaveManager.SaveData(playerStatsFilePath, playerstats);
    }

    public void LoadPlayerStats()
    {
        if (SaveManager.LoadData(playerStatsFilePath, out PlayerStats dataLoaded))
            playerstats = dataLoaded;
    }

    public void SavePlayerTransform()
    {
        playerTransformData.position[0] = transform.position.x;
        playerTransformData.position[1] = transform.position.y;
        playerTransformData.position[2] = transform.position.z;

        Vector3 playerRotation = transform.rotation.eulerAngles;
        playerTransformData.rotation[0] = playerRotation.x;
        playerTransformData.rotation[1] = playerRotation.y;
        playerTransformData.rotation[2] = playerRotation.z;

        playerTransformData.scale[0] = transform.localScale.x;
        playerTransformData.scale[1] = transform.localScale.y;
        playerTransformData.scale[2] = transform.localScale.z;

        SaveManager.SaveData(playerTransformFilePath, playerTransformData);
    }

    public void LoadPlayerTransform()
    {
        if (SaveManager.LoadData(playerTransformFilePath, out PlayerTransformData dataLoaded))
            playerTransformData = dataLoaded;

        Vector3 newPosition;
        newPosition.x = playerTransformData.position[0];
        newPosition.y = playerTransformData.position[1];
        newPosition.z = playerTransformData.position[2];
        transform.position = newPosition;
        
        Vector3 playerRotation;
        playerRotation.x = playerTransformData.rotation[0];
        playerRotation.y = playerTransformData.rotation[1];
        playerRotation.z = playerTransformData.rotation[2];
        transform.rotation = Quaternion.Euler(playerRotation);

        Vector3 playerScale;
        playerScale.x = playerTransformData.scale[0];
        playerScale.y = playerTransformData.scale[1];
        playerScale.z = playerTransformData.scale[2];
        transform.localScale = playerScale;
    }
}

[System.Serializable]
public class PlayerStats
{
    public string playerName;
    public int health;
}

[System.Serializable]
public class PlayerTransformData
{
    public float[] position = new float[3];
    public float[] rotation = new float[3];
    public float[] scale = new float[3];
}