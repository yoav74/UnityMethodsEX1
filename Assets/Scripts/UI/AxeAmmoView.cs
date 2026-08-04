using TMPro;
using UnityEngine;

/// <summary>
/// The axe-ammo "view": single responsibility is to display the axe count.
/// It depends on the <see cref="IAmmoCounter"/> abstraction (Dependency Inversion)
/// and gets its references through the inspector instead of GameObject.Find.
/// </summary>
public class AxeAmmoView : MonoBehaviour
{
    [SerializeField] private AxeAmmo axeAmmo;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private string format = "Axes: {0}";

    private IAmmoCounter counter;

    private void Awake()
    {
        counter = axeAmmo;
    }

    private void OnEnable()
    {
        if (counter == null)
            return;

        counter.OnAmmoChanged += Render;
        Render(counter.Count); // show the correct value immediately
    }

    private void OnDisable()
    {
        if (counter != null)
            counter.OnAmmoChanged -= Render;
    }

    private void Render(int count)
    {
        label.text = string.Format(format, count);
    }
}
