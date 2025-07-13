using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputRelaySource : MonoBehaviour
{
    [SerializeField] LayerMask RaycastMask = ~0;
    [SerializeField] float RaycastDistance = 400f;
    [SerializeField] UnityEvent<Vector2> OnCursorInput = new UnityEvent<Vector2>();
    public Camera camera1;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        // retrieve a ray based on the mouse location
        Ray mouseRay = camera1.ScreenPointToRay(Input.mousePosition);
        
        // raycast to find what we have hit
        RaycastHit hitResult;
        if (Physics.Raycast(mouseRay, out hitResult, RaycastDistance, RaycastMask, QueryTriggerInteraction.Ignore))
        {
           
            // ignore if not us
            if (hitResult.collider.gameObject != gameObject)
                return;

            if (OnCursorInput != null)
            {
                OnCursorInput.Invoke(hitResult.textureCoord);
            }
            else
            {
                Debug.LogWarning("OnCursorInput UnityEvent is n(ot assigned.");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(camera1.ScreenPointToRay(Input.mousePosition));
    }
}
