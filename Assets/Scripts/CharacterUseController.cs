using UnityEngine;
using UnityEngine.UI;

public class CharacterUseController : MonoBehaviour
{
    [SerializeField] private float pickupRange = 3f;
    [SerializeField] private Transform holdPosition; 
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Text pickupText;
    
    private GameObject _heldItem; 

    void Update()
    {
        CheckForUsableItem();
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_heldItem == null)
            {
                TryPickupItem();
            }
            else
            {
                DropItem();
            }
        }
    }
    
    private void CheckForUsableItem()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Usable") && _heldItem == null)
            {
                pickupText.gameObject.SetActive(true);
                return;
            }
        }
        pickupText.gameObject.SetActive(false);
    }

    private void TryPickupItem()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Usable"))
            {
                _heldItem = hit.collider.gameObject;
                _heldItem.transform.SetParent(holdPosition);
                _heldItem.transform.localPosition = Vector3.zero; 
                _heldItem.transform.localRotation = Quaternion.identity; 
                Rigidbody rb = _heldItem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true; 
                }
                pickupText.gameObject.SetActive(false);
            }
        }
    }

    private void DropItem()
    {
        if (_heldItem != null)
        {
            _heldItem.transform.SetParent(null);
            Rigidbody rb = _heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; 
                rb.AddForce(transform.forward * 2f, ForceMode.VelocityChange); 
            }
            _heldItem = null; 
        }
    }
}
