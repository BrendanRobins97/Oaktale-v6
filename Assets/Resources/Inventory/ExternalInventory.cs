using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalInventory : Inventory {

    public bool open = false;

    [SerializeField]
    private float maxDistance = 10f;

    protected virtual void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!open) OpenInventory();
            else CloseInventory();
        }
    }

    public virtual void OpenInventory()
    {
        UIHandler uiHandler = player.uiHandler;
        inventoryPanel.transform.SetParent(uiHandler.externalInventory.transform);
        uiHandler.externalInventory.SetActive(true);
        uiHandler.externInv = this;
        inventoryPanel.SetActive(true);
        open = true;
    }

    public virtual void CloseInventory()
    {
        UIHandler uiHandler = player.uiHandler;
        inventoryPanel.transform.SetParent(transform);
        uiHandler.externalInventory.SetActive(false);
        inventoryPanel.SetActive(false);
        open = false;
    }
    protected virtual void Update()
    {
        if (open && (player.transform.position - transform.position).magnitude > maxDistance)
        {
            CloseInventory();
        }
    }


}
