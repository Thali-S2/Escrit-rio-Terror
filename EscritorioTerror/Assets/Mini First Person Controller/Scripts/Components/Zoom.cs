using UnityEngine;
// É necessário importar o namespace do novo Input System
using UnityEngine.InputSystem; 

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera camera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;


    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        camera = GetComponent<Camera>();
        if (camera)
        {
            defaultFOV = camera.fieldOfView;
        }
    }

    void Update()
    {
        // Verifica se o mouse está conectado e ativoooooo
        if (Mouse.current != null)
        {
            // Lê o valor do scroll vertical usando o novo Input System
            float scrollDeltaY = Mouse.current.scroll.ReadValue().y;

            // O novo sistema retorna valores maiores (ex: 120 ou -120), 
            // então normalizamos multiplicando por 0.01f antes de aplicar a sua lógica
            currentZoom += scrollDeltaY * 0.01f * sensitivity * .05f;
        }

        // Update the currentZoom and the camera's fieldOfView.
        currentZoom = Mathf.Clamp01(currentZoom);
        camera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
