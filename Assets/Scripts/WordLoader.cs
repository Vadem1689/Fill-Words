using UnityEngine;
using System.Collections.Generic;
using System.IO;

//[System.Serializable]
//public class WordList
//{
//    public List<string> words;
//}

//public interface IGetData
//{
//    public void GetJson();
    
//}

//public class GetJson : IGetData
//{
//    public string jsonFileName = "words.json";
//    static public List<string> words;
//    private bool load = false;
    
  

//    void IGetData.GetJson()
//    {
//        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

//        if (File.Exists(filePath))
//        {
//            string json = File.ReadAllText(filePath);
//            WordList wordList = JsonUtility.FromJson<WordList>(json);
//            words = wordList.words;
//        }
//        else
//        {
//            //Debug.LogError("Файл не найден: " + filePath);
//        }
//    }
//}

//public class GetJsonHTTP : IGetData
//{
//    public void GetJson()
//    {
//        throw new System.NotImplementedException();
//    }
//}


public class WordLoader : MonoBehaviour
{
    static public List<string> words;
    public IGetData getData;

    public WordLoader()
    {
        getData = new GetJson();
    }

    public void LoadWords()
    {
        getData.GetJson();


    }
}
