using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Translates player input into weapon commands: fire the selected weapon and
/// switch which weapon is selected. Single responsibility: input -> WeaponsHandler.
/// It holds no weapon state of its own.
/// </summary>
public class WeaponInput : MonoBehaviour
{
    [SerializeField] private WeaponsHandler handler;

    private void Awake()
    {
        if (handler == null)
            handler = GetComponent<WeaponsHandler>();
    }

    private void Update()
    {
        if (handler == null)
            return;

        HandleFire();
        HandleNumberKeys();
        HandleScroll();
    }

    private void HandleFire()
    {
        if (Keyboard.current != null && Keyboard.current.leftCtrlKey.wasPressedThisFrame)
            handler.FireSelected();
    }

    private void HandleNumberKeys()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null)
            return;

        // Digit1..Digit9 are contiguous in Key; keys 1..9 select weapon slots 0..8.
        for (int i = 0; i < 9; i++)
        {
            if (kb[(Key)((int)Key.Digit1 + i)].wasPressedThisFrame)
                handler.Select(i);
        }
    }

    private void HandleScroll()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
            return;

        float scroll = mouse.scroll.ReadValue().y;
        if (scroll > 0f)
            handler.SelectNext();
        else if (scroll < 0f)
            handler.SelectPrevious();
    }
}
