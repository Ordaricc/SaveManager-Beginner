using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveManager : MonoBehaviour
{
    private static string SavesFolderPath { get { return Path.Combine(Application.persistentDataPath, "Saves"); } }

    public static void SaveData<T>(string fileName, T dataToSave)
    {
        if (!Directory.Exists(SavesFolderPath))
            Directory.CreateDirectory(SavesFolderPath);

        string filePath = Path.Combine(SavesFolderPath, fileName);
        FileStream fileStream = null;
        try
        {
            //open file and modify it
            fileStream = new FileStream(filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, dataToSave);
        }
        finally
        {
            //close file
            fileStream?.Close();
        }
    }

    public static bool LoadData<T>(string fileName, out T dataToLoad)
    {
        dataToLoad = default;
        if (!Directory.Exists(SavesFolderPath))
            return false;

        string filePath = Path.Combine(SavesFolderPath, fileName);
        if (!File.Exists(filePath))
            return false;

        bool wasDataLoadSuccessful = false;
        FileStream fileStream = null;
        try
        {
            //open file and load it
            fileStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            dataToLoad = (T)formatter.Deserialize(fileStream);
            wasDataLoadSuccessful = true;
        }
        finally
        {
            //close file
            fileStream?.Close();
        }

        return wasDataLoadSuccessful;
    }
    
    #if UNITY_EDITOR
    [MenuItem("SaveManager/Delete All Saved Data")]
    #endif
    public static void DeleteAllData()
    {
        if (Directory.Exists(SavesFolderPath))
            Directory.Delete(SavesFolderPath, true);
    }

    public static void DeleteOneFile(string fileName)
    {
        string filePath = Path.Combine(SavesFolderPath, fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}