using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SUPERCharacter;
using Unity.Cinemachine;
using Interfaces;
using System.Linq;
public class OutlineShown : MonoBehaviour
{
    public CinemachineCamera playerCamera;
    private float interactRange = 4f;
    private int interactableLayer = -1;
    RaycastHit h;
    private void Update()
    {
        CheckCollision();
    }
    void CheckCollision()
    {
        if (Physics.SphereCast(playerCamera.transform.position, 0.25f, playerCamera.transform.forward, out h, interactRange, interactableLayer, QueryTriggerInteraction.Ignore))
        {
            IOutlineable i = h.collider.GetComponent<IOutlineable>();
            if (i != null)
            {
                i.ShowOutline();
            }
            else
            {
                DisableAllOutlines();
            }
        }
    }

    private void DisableAllOutlines()
    {
        // Find all objects and disable outlines (if needed)
        foreach (var outlineable in FindObjectsOfType<MonoBehaviour>().OfType<IOutlineable>())
        {
            outlineable.DisableOutline();
        }
    }


}