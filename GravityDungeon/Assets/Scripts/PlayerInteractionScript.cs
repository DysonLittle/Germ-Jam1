using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    SphereCollider sc;
    Interactable closestInteractable;
    [HideInInspector]
    public Interactable heldObject;

    public GameObject hoverPromptPrefab;
    GameObject hoverPromptObj;
    GameObject canvas;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        sc = GetComponent<SphereCollider>(); //get the second, larger one, used for interactable objects
        closestInteractable = null;
        heldObject = null;
    }

    void Update()
    {
        FindInteractables();
        CheckSelection();

        if (hoverPromptObj)
            hoverPromptObj.transform.position = GetCanvasSpaceFromWorldSpace(closestInteractable.transform.position);
    }

    void CheckSelection()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (closestInteractable)
            {
                closestInteractable.Interact(gameObject);
            }
            else if (heldObject)
            {
                heldObject.StopInteracting(gameObject);
            }
        }
    }

    void FindInteractables()
    {
        Collider[] colliderHits = Physics.OverlapSphere(sc.center + transform.position, sc.radius * transform.localScale.x);
        
        if (colliderHits.Length == 0) //no gameObjects in sphere
        {
            UpdateclosestInteractable(null);
            return;
        }

        GameObject closestObj = null;
        for (int i = 0; i < colliderHits.Length; i++)
        {
            GameObject currentObj = colliderHits[i].gameObject;
            if (currentObj.GetComponent<Interactable>() && currentObj.GetComponent<Interactable>().CanInteract(gameObject) && currentObj != heldObject)
            {
                if (!closestObj)
                {
                    closestObj = currentObj;
                }
                else if((currentObj.transform.position - sc.center - transform.position).sqrMagnitude < (closestObj.transform.position - sc.center - transform.position).sqrMagnitude)
                {
                    closestObj = currentObj;
                }
            }
        }

        if (!closestObj) //no interactables found
        {
            UpdateclosestInteractable(null);
            return;
        }

        UpdateclosestInteractable(closestObj.GetComponent<Interactable>());
    }

    void UpdateclosestInteractable(Interactable newInteractable)
    {
        if (newInteractable == closestInteractable)
            return;

        if (closestInteractable)
        {
            Destroy(hoverPromptObj);
            hoverPromptObj = null;
        }
        closestInteractable = newInteractable;
        if (closestInteractable)
        {
            hoverPromptObj = Instantiate(hoverPromptPrefab, canvas.transform);
            hoverPromptObj.transform.position = GetCanvasSpaceFromWorldSpace(closestInteractable.transform.position);
        }
    }

    Vector3 GetCanvasSpaceFromWorldSpace(Vector3 worldSpace)
    {
        Vector3 viewSpaceVec = Camera.main.WorldToViewportPoint(worldSpace);
        Vector2 sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
        return new Vector3(viewSpaceVec.x * sizeDelta.x, viewSpaceVec.y * sizeDelta.y, 0);
    }
}
