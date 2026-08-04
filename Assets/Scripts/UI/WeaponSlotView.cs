using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Renders a single weapon entry in the selector: its icon and name, plus a
/// selected/unselected highlight. Single responsibility: display one weapon slot.
/// </summary>
public class WeaponSlotView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private Color unselectedColor = Color.grey;

    public void Bind(string weaponName, Sprite weaponIcon)
    {
        if (label != null)
            label.text = weaponName;

        if (icon != null)
        {
            icon.sprite = weaponIcon;
            icon.enabled = weaponIcon != null; // hide the image when a weapon has no icon
        }
    }

    public void SetSelected(bool selected)
    {
        if (label != null)
            label.color = selected ? selectedColor : unselectedColor;
    }
}
