using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Labb3_NET22.DataModels;
using Newtonsoft.Json;

namespace Labb3_NET22.Helpers;

public static class FileHandler
{
    private static readonly string AppDataPath =
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    static FileHandler()
    {
        try
        {
            Directory.CreateDirectory($"{AppDataPath}/Labb3");
        }
        catch (Exception e)
        {
            MessageBox.Show($"Something went wrong creating the Labb3 directory: {e}");
        }
    }


    public static List<Quiz> ReadQuizFiles()
    {
        try
        {
            List<Quiz> foundQuizFiles = new();
            var availableFiles = Directory.EnumerateFiles($"{AppDataPath}/Labb3", "*.json");

            foreach (var file in availableFiles)
            {
                var text = File.ReadAllText(file);
                var quiz = JsonConvert.DeserializeObject<Quiz>(text);

                if (quiz != null) foundQuizFiles.Add(quiz);
            }


            return foundQuizFiles.Count > 0
                ? foundQuizFiles
                : throw new Exception("No quiz files found in app folder.");
        }
        catch (Exception e)
        {
            MessageBox.Show($"Something went wrong reading quiz files: {e}");
        }

        return null;
    }

    public static Quiz? ReadQuizFile(string fileName)
    {
        try
        {
            using (var r = new StreamReader($"{AppDataPath}/Labb3/{fileName}"))
            {
                var fileData = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Quiz>(fileData);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"Something went wrong reading quiz file: {e}");
        }

        return null;
    }

    public static void SaveQuiz(Quiz quiz)
    {
        try
        {
            // Orkar inte type safety json igen (￣o￣) . z Z
            var json = JsonConvert.SerializeObject(quiz);

            File.WriteAllText($"{AppDataPath}/Labb3/{quiz.Title}.json", json);
        }
        catch (Exception e)
        {
            MessageBox.Show($"Something went wrong creating your file: {e}");
        }
    }
}