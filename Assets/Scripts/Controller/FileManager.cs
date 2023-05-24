using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

            Debug.Log("File written successfully.");
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
    public static void SavePlayerData(string fileName, Player player)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"speed, {player.speed}");
                writer.WriteLine($"maxHealth, {player.maxHealth}");
                writer.WriteLine($"bonusdame, {player.bonusdame}");
                writer.WriteLine($"healthBarValue, {player.healthBar.value}");
            }

            Debug.Log("Player data saved successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving player data: " + ex.Message);
        }
    }
}
