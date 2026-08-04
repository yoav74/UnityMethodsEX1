using TMPro;
using UnityEngine;

/// <summary>
/// The coin "view": single responsibility is to display the coin count.
/// It depends on the <see cref="ICoinCounter"/> abstraction (Dependency Inversion)
/// and gets its references through the inspector instead of GameObject.Find,
/// so it never searches the scene at runtime.
/// </summary>
public class CoinCountView : MonoBehaviour
{
    [SerializeField] private SC_CoinsManager coinsManager;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private string format = "Coins: {0}";

    private ICoinCounter counter;

    private void Awake()
    {
        counter = coinsManager;
    }

    private void OnEnable()
    {
        if (counter == null)
            return;

        counter.OnCoinsChanged += Render;
        Render(counter.Count); // show the correct value immediately
    }

    private void OnDisable()
    {
        if (counter != null)
            counter.OnCoinsChanged -= Render;
    }

    private void Render(int count)
    {
        label.text = string.Format(format, count);
    }
}
