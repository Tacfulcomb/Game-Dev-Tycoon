using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
//using SUPERCharacter;
using UnityEngine;
using Interfaces;

public class ReturnToPlayerState : MonoBehaviour
{
    //public SUPERCharacterAIO playerController;
    public GameObject player;
    public GameObject laptopCameraInteraction;
    public GameObject laptop;
    private Collider laptopCollider;
    public InputRelaySource laptopControls;
    public InputRelaySource laptopControlsEverything;

    public CinemachineCamera playerCamera;
    public CinemachineCamera laptopViewCamera;

    private void Start()
    {
        laptopCollider = laptop.GetComponent<Collider>();
    }
    private void Update()
    {
        // if (playerController.defaultState == States.ComputerState && Input.GetKeyDown(KeyCode.LeftShift))
        // {
        //     Cursor.visible = false;
        //     Cursor.lockState = CursorLockMode.Locked;
        //     StartCoroutine(ReturnToPlayer());
        // }
    }

    // IEnumerator ReturnToPlayer()
    // {
    //     Debug.Log("Begin Changing computer state to player state");
    //     playerController.defaultState = States.PlayerState;
    //     player.SetActive(true);
    //     laptopCameraInteraction.SetActive(false);
    //     laptopControls.enabled = false;
    //     laptopControlsEverything.enabled = false;
    //     StartCoroutine(SwitchCamera());
    //     yield return new WaitForSeconds(1f);
    //     Debug.Log("Finished changing to playerState");
    //     laptopCollider.enabled = true;
    //     playerController.enabled = true;
    //     playerController.crosshairImg.gameObject.SetActive(true);
    // }

    IEnumerator SwitchCamera()
    {
        yield return new WaitForSeconds(0.1f);
        (laptopViewCamera.Priority, playerCamera.Priority) = (playerCamera.Priority, laptopViewCamera.Priority);
    }
}
