using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
public class FileManager
{
    public static void WriteToFile(string fileName, string data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(data);
            }

            Debug.Log("File written successfully : " + filePath);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error writing file: " + ex.Message);
        }
    }

    public static string ReadFromTxtFile(string fileName)
    {
        string data = string.Empty;
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    data = reader.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error reading file: " + ex.Message);
        }

        return data;
    }
    public string[][] ReadFromCSVFile(string filePath)
    {
        List<string[]> data = new List<string[]>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] row = line.Split(',');
                    data.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error reading file: " + ex.Message);
        }

        return data.ToArray();
    }
    public static void WriteToFileInSameLine(string fileName, string[] parameters)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            string line = string.Join(", ", parameters);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(line);
            }

            Debug.Log("File written successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error writing file: " + ex.Message);
        }
    }
    public static void WriteToFileInSameColumn(string fileName, string[] parameters)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in parameters)
                {
                    writer.WriteLine(item);
                }
            }

            Debug.Log("File written successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error writing file: " + ex.Message);
        }
    }
    public static void WriteToFile(string fileName, string[][] data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string[] row in data)
                {
                    string line = string.Join(", ", row);
                    writer.WriteLine(line);
                }
            }

            Debug.Log("File written successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error writing file: " + ex.Message);
        }
    }
    public static void SavePlayerData(Player player)
    {
        string fileName = "PlayerData.json";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log($"Lưu Player Data tại vị trí: {filePath}");

        try
        {
            PlayerData playerData = new PlayerData(player);
            string json = JsonConvert.SerializeObject(playerData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);

            Debug.Log("Dữ liệu người chơi đã được lưu thành công.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Lỗi khi lưu dữ liệu người chơi: " + ex.Message);
        }
    }

    public static PlayerData ReadPlayerData()
    {
        string fileName = "PlayerData.json";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        PlayerData playerData = null;
        Debug.Log($"Đoc Player Data tại vị trí : {filePath}");
        try
        {
            // Kiểm tra xem tệp tin có tồn tại không
            if (File.Exists(filePath))
            {
                // Đọc nội dung từ tệp tin
                string json = File.ReadAllText(filePath);
                playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            }
            else
            {
                Debug.Log("Tệp tin không tồn tại");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Lỗi khi đọc dữ liệu người chơi: " + ex.Message);
        }

        return playerData;
    }
}
