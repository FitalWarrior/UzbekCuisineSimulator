using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class GrabSystemController : MonoBehaviour
{
    [SerializeField]
    private Camera characterCamera;

    [SerializeField] private Transform _grabSlot;

    private PickableItem pickedItem;

    private void Start()
    {
        characterCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (pickedItem)
            {
                DropItem(pickedItem);
            }
            else
            {
                var ray = characterCamera.ViewportPointToRay(Vector3.one*0.5f);
                RaycastHit hit;
                if (Physics.Raycast(ray,out hit,3f))
                {
                    var pickable = hit.transform.GetComponent<PickableItem>();
                    if (pickable)
                    {
                        PickItem(pickable);
                    }
                }
            }
        }
    }

    private void PickItem(PickableItem item) 
    {
        pickedItem = item;
        item.Rb.isKinematic = true;
        item.Rb.velocity= Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;

        item.transform.SetParent(_grabSlot);

        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }

    private void DropItem(PickableItem item)
    {
        
        pickedItem = null;
        
        item.transform.SetParent(null);
        
        item.Rb.isKinematic = false;
        
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }
}
