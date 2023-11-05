using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
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


    public static async Task<List<Quiz?>?> ReadQuizFiles()
    {
        try
        {
            List<Quiz>? foundQuizFiles = new();
            var availableFiles = Directory.EnumerateFiles($"{AppDataPath}/Labb3", "*.json");

            foreach (var file in availableFiles)
            {
                var text = await File.ReadAllTextAsync(file);
                var quiz = JsonConvert.DeserializeObject<Quiz>(text);

                if (quiz != null) foundQuizFiles.Add(quiz);
            }


            if (foundQuizFiles.Count > 0) return foundQuizFiles;
            MessageBox.Show("No quiz files found in app folder.");
            return null;
        }
        catch (Exception e)
        {
            MessageBox.Show($"Something went wrong reading quiz files: {e}");
        }

        return null;
    }

    public static async Task<Quiz?> ReadQuizFileAsync(string fileName)
    {
        try
        {
            using var r = new StreamReader($"{AppDataPath}/Labb3/{fileName}");
            var fileData = await r.ReadToEndAsync();
            return JsonConvert.DeserializeObject<Quiz>(fileData);
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

    // Chat-GPT
    public static byte[] ConvertBmpToBytes(BitmapImage bitmapImage)
    {
        using var stream = new MemoryStream();
        var encoder = new BmpBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
        encoder.Save(stream);
        return stream.ToArray();
    }

    // Chat-GPT
    public static BitmapImage ConvertBytesToBmp(byte[] imageBytes)
    {
        var bitmapImage = new BitmapImage();
        using var stream = new MemoryStream(imageBytes);
        bitmapImage.BeginInit();
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.StreamSource = stream;
        bitmapImage.EndInit();

        return bitmapImage;
    }
}