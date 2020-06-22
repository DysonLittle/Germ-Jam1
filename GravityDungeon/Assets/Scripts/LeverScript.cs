using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Interactable
{
    GameObject canvas;
    public GameObject hoverPromptPrefab;
    GameObject hoverPromptObj;
    bool hovering = false;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    private void Update()
    {
        if (hovering)
        {
            hoverPromptObj.transform.position = GetCanvasSpaceFromWorldSpace(transform.position);
        }
    }

    public override void Hover(bool toggle = true)
    {
        hovering = toggle;
        if (toggle)
        {
            hoverPromptObj = Instantiate(hoverPromptPrefab, canvas.transform);
            hoverPromptObj.transform.position = GetCanvasSpaceFromWorldSpace(transform.position);
        }
        else
        {
            Destroy(hoverPromptObj);
        }
    }

    public override void Interact(GameObject player)
    {
        Debug.Log("Pull the lever, Kronk!");
        player.GetComponent<PlayerInteractionScript>().currentInteractableSelected = null;
    }

    Vector3 GetCanvasSpaceFromWorldSpace(Vector3 worldSpace)
    {
        Vector3 viewSpaceVec = Camera.main.WorldToViewportPoint(worldSpace);
        Vector2 sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
        return new Vector3(viewSpaceVec.x * sizeDelta.x, viewSpaceVec.y * sizeDelta.y, 0);
    }
}
