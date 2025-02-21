using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public WordLoader wordLoader;
    public WordGame wordGame;
    public GridGenerator gridGenerator;
    public GridVisualizer gridVisualizer;

    void Start()
    {
        //StartCoroutine(RunGameFlow());
    }

    public IEnumerator RunGameFlow(int levelNumber)
    {
        Debug.Log("�������� ����...");
        wordLoader.LoadWords();

        if (WordLoader.words == null || WordLoader.words.Count == 0)
        {
            Debug.LogError("������ ���� ����! ��������� JSON-����.");
            yield break;
        }

        while (true)
        {
            Debug.Log("������ ���� ��� ���������� �����...");

            gridGenerator.wordsToPlace = new List<string>(WordLoader.words);

            Debug.Log("��������� �����...");
            bool success = false;
            yield return StartCoroutine(gridGenerator.GenerateGridCoroutine(levelNumber, 5, result => success = result));

            if (success)
            {
                Debug.Log("����� ������� �������������!");
                break;
            }
            else
            {
                Debug.LogWarning("�� ������� ������������� �����. ��������� �������...");

                yield return StartCoroutine(gridGenerator.GenerateGridCoroutine(levelNumber, 5, result => success = result));
            }
        }

        Debug.Log("������������...");
        gridVisualizer.gridGenerator = gridGenerator;
        yield return StartCoroutine(gridVisualizer.VisualizeGridCoroutine());

        Debug.Log("���� ������� ��������!");
    }
}