using System;
using System.Collections;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float defaultMoveSpeed;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float lookSensitivity = 100f;

    [Header("References")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Volume postProcessingVolume;
    //[SerializeField] private Animator animator;

    #region Movement Variables
    private Vector2 moveInput;
    private Vector2 jumpInput;
    private Vector2 lookInput;
    private float xRotation = 0f;
    private float yRotation = 0f;
    #endregion
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnInitialization();
        //Time.timeScale = 0.2f;
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        ChangeLookRotation();
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * MoveDirection());
    }
    #region Input Actions 
    private void OnInitialization()
    {
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
    }
    private void OnDestroy()
    {
        playerInput.actions["Move"].performed -= OnMove;
        playerInput.actions["Move"].canceled -= OnMove;
    }
    private void OnPause()
    {

    }
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    #endregion
    #region Movement
    private Vector3 MoveDirection()
    {
        Vector3 playerForward = transform.forward;
        Vector3 playerRight = transform.right;
        Vector3 moveDirection = (playerForward * moveInput.y + playerRight * moveInput.x).normalized;

        if (moveDirection != Vector3.zero)
        {
            if (Physics.Raycast(transform.position, moveDirection, 1f))
            {
                return Vector3.zero;
            }
        }

        return moveDirection;
    }

    private void ChangeLookRotation()
    {
        // look input
        lookInput = playerInput.actions["Look"].ReadValue<Vector2>();
        // look up and down
        xRotation -= lookInput.y * lookSensitivity * Time.fixedDeltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // look right and left
        yRotation += lookInput.x * lookSensitivity * Time.fixedDeltaTime;
        //Synchornization is a big fuzz!!! Pay attention to Time.deltaTime or fixedDeltaTime

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation,0f, 0f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    #endregion
    #region PlayerProperties
    #endregion
    #region Animations
    private void AnimationHandler()
    {
        moveSpeed = defaultMoveSpeed;

    }
    #endregion
}
