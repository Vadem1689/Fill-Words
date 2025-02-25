using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public int x, y;
    public string letter;
    public Color originalColor;
    public Image cellImage;
    public GameObject SecretPanel;
    public CellController cellParent;
    public bool SecretWord = false;
    public bool IsLocked { get; set; }

    [SerializeField] private GameObject secretCellPrefab; // Префаб для SecretCellController
    [SerializeField] private Animator cellAnimator; // Аниматор ячейки

    private void Awake()
    {
        cellImage = GetComponent<Image>();
        if (cellImage != null)
        {
            originalColor = cellImage.color;
        }
    }

    public void SetData(int x, int y, string letter)
    {
        this.x = x;
        this.y = y;
        StringBuilder word = new StringBuilder(letter);

        if (word[0] == '_')
        {
            word.Remove(0, 1);
            SecretWord = true;
        }

        GetComponentInChildren<TextMeshProUGUI>().text = word.ToString();
        this.letter = word.ToString();
        IsLocked = false;
    }

    public void HighlightCell(Color color)
    {
        if (cellImage != null)
        {
            cellImage.color = color;
        }
    }

    public void ResetCell()
    {
        if (cellImage != null && !IsLocked)
        {
            cellImage.color = originalColor;
        }
    }

    public void LockCell()
    {
        IsLocked = true;
    }

    public void PlayAnimation()
    {
        if (cellAnimator != null)
        {
            cellAnimator.SetTrigger("Animate");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GridVisualizer.instance != null && cellParent == null)
        {
            GridVisualizer.instance.StartSelection(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GridVisualizer.instance != null && GridVisualizer.instance.IsSelecting() && cellParent == null)
        {
            GridVisualizer.instance.ContinueSelection(this);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GridVisualizer.instance != null && GridVisualizer.instance.IsSelecting() && cellParent == null)
        {
            GridVisualizer.instance.EndSelection();
        }

        if (SecretWord && cellParent == null)
        {
            if (!IsLocked) // Если буква не заблокирована, добавляем
            {
                GameObject secretCellObj = Instantiate(secretCellPrefab, SecretPanel.transform);
                SecretCellController secretCell = secretCellObj.GetComponent<SecretCellController>();
                secretCell.Initialize(this);
                cellImage.color = Color.green;
                IsLocked = true;
                SecretPanel.GetComponent<SecretContainer>().SectretWordPoisk();
            }
        }
    }
}
