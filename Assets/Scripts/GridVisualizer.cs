using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class GridVisualizer : MonoBehaviour
{
    public static GridVisualizer instance;

    [Header("Grid Configuration")]
    public GridLayoutGroup gridLayout;
    public GameObject SectretContainer;
    public GameObject cellPrefab;
    public GridGenerator gridGenerator;
    public Transform BridgeContainer;
    public Image BridgePrefab;

    private List<CellController> selectedCells = new List<CellController>();
    private bool isSelecting = false;
    public GameObject LoadPanel;

    private Queue<Color> recentColors = new Queue<Color>();

    void Awake()
    {
        instance = this;
        LoadPanel.SetActive(true);
    }

    public IEnumerator VisualizeGridCoroutine()
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = gridGenerator.gridSize;

        foreach (Transform child in gridLayout.transform)
        {
            Destroy(child.gameObject);
        }

        string[,] grid = gridGenerator.grid;

        for (int x = 0; x < gridGenerator.gridSize; x++)
        {
            for (int y = 0; y < gridGenerator.gridSize; y++)
            {
                GameObject cell = Instantiate(cellPrefab, gridLayout.transform);
                cell.GetComponent<CellController>().SecretPanel = SectretContainer;
                TextMeshProUGUI cellText = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (cellText != null)
                {
                    cellText.text = grid[x, y].ToString();
                }

                CellController cellController = cell.GetComponent<CellController>();
                if (cellController != null)
                {
                    cellController.SetData(x, y, grid[x, y]);
                    cellController.IsLocked = false; // ������������� ��������� ������
                }

                yield return null;
            }
        }
        LoadPanel.SetActive(false);
        Debug.Log("����� ��������������� � ������.");
    }

    public void StartSelection(CellController cell)
    {
        if (cell.IsLocked)
        {
            Debug.LogWarning("��� ������ ��� �������������.");
            return;
        }

        if (selectedCells.Count > 0 && !IsAdjacent(cell, selectedCells[selectedCells.Count - 1]))
        {
            ResetSelection(); // ���������� �����, ���� ������ �� �������� ��������
        }

        isSelecting = true;
        AddCellToSelection(cell);
    }

    public void ContinueSelection(CellController cell)
    {
        if (isSelecting && !cell.IsLocked && IsAdjacent(cell, selectedCells[selectedCells.Count - 1]))
        {
            AddCellToSelection(cell);
        }
    }

    public int GetRemainingWordsCount()
    {
        return gridGenerator.wordsToPlace.Count;
    }

    public void ShowHint()
    {
        if (gridGenerator.wordsToPlace.Count == 0)
        {
            Debug.LogWarning("Нет оставшихся слов для подсказки.");
            return;
        }

        string hintWord = gridGenerator.wordsToPlace[Random.Range(0, gridGenerator.wordsToPlace.Count)];
        List<CellController> hintCells = FindWordCells(hintWord);

        if (hintCells.Count > 0)
        {
            AnimateHint(hintCells);
        }
    }

    private List<CellController> FindWordCells(string word)
    {
        List<CellController> wordCells = new List<CellController>();

        foreach (Transform cellObj in gridLayout.transform)
        {
            CellController cell = cellObj.GetComponent<CellController>();
            if (cell != null && word.StartsWith(cell.letter))
            {
                List<CellController> tempCells = new List<CellController> { cell };
                int index = 1;

                for (int i = 1; i < word.Length; i++)
                {
                    int nextX = cell.x + i;
                    int nextY = cell.y;

                    CellController nextCell = FindCellAt(nextX, nextY);
                    if (nextCell != null && nextCell.letter == word[index].ToString())
                    {
                        tempCells.Add(nextCell);
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (index == word.Length)
                {
                    return tempCells;
                }
            }
        }
        return wordCells;
    }

    private void AnimateHint(List<CellController> cells)
    {
        foreach (var cell in cells)
        {
            cell.PlayAnimation();

        }
    }


    private CellController FindCellAt(int x, int y)
    {
        foreach (Transform cellObj in gridLayout.transform)
        {
            CellController cell = cellObj.GetComponent<CellController>();
            if (cell != null && cell.x == x && cell.y == y)
            {
                return cell;
            }
        }
        return null;
    }




    public void EndSelection()
    {
        isSelecting = false;

        string word = string.Concat(selectedCells.ConvertAll(c => c.letter));
        if (gridGenerator.wordsToPlace.Contains(word))
        {
            Debug.Log("���������� ����� �������!");

            // ��������� ��������� ���� � ��������� �������
            ApplyRandomColorToSelection();

            // ��������� ��������� ������
            LockSelectedCells();

            selectedCells.Clear();
        }
        else
        {
            Debug.LogWarning("������� ������������ �����.");
            ResetSelection();
        }
    }

    public bool IsSelecting()
    {
        return isSelecting;
    }

    private void AddCellToSelection(CellController cell)
    {
        if (!selectedCells.Contains(cell))
        {
            selectedCells.Add(cell);
            cell.HighlightCell(Color.yellow); // �������� ������ ����� ������
        }
    }

    public void ResetSelection()
    {
        foreach (var cell in selectedCells)
        {
            cell.ResetCell();
        }
        selectedCells.Clear();
        isSelecting = false;
    }

    private void ApplyRandomColorToSelection()
    {
        Color uniqueColor = GenerateUniqueColor();
        foreach (var cell in selectedCells)
        {
            cell.HighlightCell(uniqueColor);
        }
        for(int i = 1; i < selectedCells.Count; i++)
        {
            var bridge = Instantiate(
                BridgePrefab, 
                Vector3.Lerp(
                    selectedCells[i].transform.position,
                    selectedCells[i-1].transform.position,
                    0.5f
                ),
                Quaternion.identity,
                BridgeContainer);
                bridge.color = uniqueColor;
            if(Mathf.Abs(selectedCells[i].transform.position.x - selectedCells[i-1].transform.position.x) > Mathf.Abs(selectedCells[i].transform.position.y - selectedCells[i-1].transform.position.y))
            {
                bridge.transform.Rotate(0,0,90);
            }
        }
    }

    private void LockSelectedCells()
    {
        foreach (var cell in selectedCells)
        {
            cell.IsLocked = true;
        }
    }

    private Color GenerateUniqueColor()
    {
        Color randomColor = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
        while (recentColors.Contains(randomColor))
        {
            randomColor = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
        }

        if (recentColors.Count >= 5)
        {
            recentColors.Dequeue();
        }
        recentColors.Enqueue(randomColor);

        return randomColor;
    }

    private bool IsAdjacent(CellController cell1, CellController cell2)
    {
        int dx = Mathf.Abs(cell1.x - cell2.x);
        int dy = Mathf.Abs(cell1.y - cell2.y);
        return (dx == 1 && dy == 0) || (dx == 0 && dy == 1);
    }

    public void ClearGrid()
    {
        foreach (Transform child in gridLayout.transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("����� �������.");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}