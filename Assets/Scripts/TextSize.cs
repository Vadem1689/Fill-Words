using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TextSize : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private WordGame wordGame;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI text;
    private int size;

    public void Size(float volume)
    {
        size = Mathf.RoundToInt(volume);
        gridGenerator.gridSize = size;

        text.text = "Size: " + volume;
    }

    public void startGeneration()
    {
        //gameManager.StartCoroutine(gameManager.RunGameFlow(levelNumber));
    }

    public void ResetSize()
    {
        //gridGenerator.gridSize = 4;
        StopAllCoroutines();
        wordGame.clear();
        gridGenerator.wordsToPlace.Clear();
        // ������� ������������
        if (gridGenerator.TryGetComponent(out GridVisualizer gridVisualizer))
        {
            gridVisualizer.ClearGrid();
        }
        Debug.Log("����� ������� ��������.");
        //gameManager.StartCoroutine(gameManager.RunGameFlow());
        //gridGenerator.InitializeGrid();
    }
}