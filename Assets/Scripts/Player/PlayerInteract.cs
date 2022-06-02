using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;

    // makes visible in inspector
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        // need a ray cast - invisible line that will detect colliders
        // 1. Ray Variable which contains an origin and a direction
        // creates a ray at the center of the camera, shooting outwards
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        // 2. RaycastHit variable which stores the hit information
        // has info like .transform .rigidbody .collider .distance
        RaycastHit hitInfo;

        // 3. Raycast() function - used to actually check for collisions along the ray 
        // out makes the function return something here, in this case it's hitInfo
        if (Physics.Raycast(ray, out hitInfo, distance, mask)) 
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null) 
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered) 
                {
                    interactable.BaseInteract();
                }
            }
        }   
    }
}
