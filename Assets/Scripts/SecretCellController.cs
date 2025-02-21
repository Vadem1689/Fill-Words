using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SecretCellController : MonoBehaviour, IPointerClickHandler
{
    private CellController parentCell;

    public void Initialize(CellController parent)
    {
        parentCell = parent;
        GetComponentInChildren<TextMeshProUGUI>().text = parent.letter;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (parentCell != null)
        {
            parentCell.IsLocked = false;
            parentCell.cellImage.color = parentCell.originalColor;
            Destroy(gameObject);
        }
    }
}