

/*
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        // Sol tık Mouse.current.leftButton.wasPressedThisFrame
        if (Input.GetMouseButtonDown(0))
        {
            // Main kameradan bir ray at
            Camera cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("Main Camera bulunamadı! Kamerana 'MainCamera' tag'ini ver.");
                return;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Clicked on: " + hit.collider.name);

                // Tıklanan objede BuildSpot var mı bak
                BuildSpot spot = hit.collider.GetComponent<BuildSpot>();
                if (spot != null)
                {
                    spot.OnClicked();
                }
            }
        }
    }
}
*/

using UnityEngine;
using UnityEngine.InputSystem;   // 🔹 Yeni Input System

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        bool clicked = false;
        Vector2 screenPos = Vector2.zero;

        // 🔹 1) PC / Editor: Mouse sol tık
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            clicked = true;
            screenPos = Mouse.current.position.ReadValue();
        }
        // 🔹 2) Android / Mobil: Dokunmatik (ilk parmak)
        else if (Touchscreen.current != null &&
                 Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            clicked = true;
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        // Hiç tıklama / dokunma yoksa çık
        if (!clicked)
            return;

        // Main kameradan bir ray at
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera bulunamadı! Kamerana 'MainCamera' tag'ini ver.");
            return;
        }

        Ray ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Clicked on: " + hit.collider.name);

            // Tıklanan objede BuildSpot var mı bak
            BuildSpot spot = hit.collider.GetComponent<BuildSpot>();
            if (spot != null)
            {
                spot.OnClicked();
            }
        }
    }
}