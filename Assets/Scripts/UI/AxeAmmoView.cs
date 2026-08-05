using TMPro;
using UnityEngine;

/// <summary>
/// The axe-ammo "view": single responsibility is to display the axe count. It
/// finds the player's <see cref="AxeAmmo"/> at runtime via <see cref="PlayerLocator"/>
/// (by "Player" tag) and depends on the <see cref="IAmmoCounter"/> abstraction.
/// </summary>
public class AxeAmmoView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private string format = "Axes: {0}";

    private IAmmoCounter counter;

    private void Awake()
    {
        counter = PlayerLocator.FindComponent<AxeAmmo>();
    }

    private void OnEnable()
    {
        if (counter == null)
            counter = PlayerLocator.FindComponent<AxeAmmo>();

        if (counter == null)
            return;

        counter.OnAmmoChanged += Render;
        Render(counter.Count);
    }

    private void OnDisable()
    {
        if (counter != null)
            counter.OnAmmoChanged -= Render;
    }

    private void Render(int count)
    {
        if (label != null)
            label.text = string.Format(format, count);
    }
}
