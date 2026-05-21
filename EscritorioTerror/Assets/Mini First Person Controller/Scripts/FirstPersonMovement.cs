using System.Collections.Generic;
using UnityEngine;
// Importa o namespace do novo Input System
using UnityEngine.InputSystem; 

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    // KeyCode foi removido pois não é compatível com o novo sistema.
    // Por padrão, usaremos a tecla Shift Esquerda diretamente no código.

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();


    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Variáveis para armazenar o input do teclado atual
        float horizontalInput = 0;
        float verticalInput = 0;
        bool isShiftPressed = false;

        // Verifica se há um teclado conectado e ativo
        if (Keyboard.current != null)
        {
            // Substitui o Input.GetKey(runningKey)
            isShiftPressed = Keyboard.current.leftShiftKey.isPressed;

            // Substitui o Input.GetAxis("Horizontal") usando as teclas WASD ou Setas
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalInput = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalInput = 1f;

            // Substitui o Input.GetAxis("Vertical") usando as teclas WASD ou Setas
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) verticalInput = -1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) verticalInput = 1f;
        }

        // Update IsRunning from input.
        IsRunning = canRun && isShiftPressed;

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(horizontalInput * targetMovingSpeed, verticalInput * targetMovingSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
    }
}
