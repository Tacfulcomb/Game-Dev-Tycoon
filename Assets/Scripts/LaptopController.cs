using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
//using SUPERCharacter;
using UnityEngine;
using Interfaces;

public class LaptopController : MonoBehaviour, IOutlineable
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
    public Outline outline;

    private void Start()
    {
        laptopCollider = laptop.GetComponent<Collider>();
        outline.enabled = false;
    }
    public bool Interact()
    {
        StartCoroutine(TransitioningStates());
        return false;
    }

    IEnumerator TransitioningStates()
    {
        //playerController.crosshairImg.gameObject.SetActive(false);
        (playerCamera.Priority, laptopViewCamera.Priority) = (laptopViewCamera.Priority, playerCamera.Priority);
        yield return new WaitForSeconds(1.2f);
        Debug.Log("Change state to Computer movement");
        player.SetActive(false);
        //playerController.enabled = false;
        laptopCameraInteraction.SetActive(true);
        laptopControls.enabled = true;
        laptopControlsEverything.enabled = true;
        laptopCollider.enabled = false;
        //playerController.defaultState = States.ComputerState;
    }

    public void ShowOutline()
    {
        outline.enabled = true;
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }
}