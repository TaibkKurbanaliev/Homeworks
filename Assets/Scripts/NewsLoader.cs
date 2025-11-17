using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class NewsLoader
{
    private string _filePath;
    private int _awaitTime = 5000;
    
    public NewsLoader(string fileName, int awaitTime)
    {
        _awaitTime = awaitTime;
        Directory.CreateDirectory(Application.streamingAssetsPath);
        _filePath = Path.Combine(Application.streamingAssetsPath, fileName);
    }

    public async Task<List<NewsItem>> LoadNewsAsync()
    {
        try
        {
            var json = await File.ReadAllTextAsync(_filePath);
            var data = JsonConvert.DeserializeObject<List<NewsItem>>(json);

            if (data == null)
                throw new InvalidDataException("Данные в json не совпадают");

            await Task.Delay(_awaitTime);
            return data;
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException("Файл не был найден!!!");
        }
        catch (JsonReaderException)
        {
            throw new JsonReaderException("Некорректный файл Json!!!");
        }       
    }
}
