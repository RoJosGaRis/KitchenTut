using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    public void Interact(){
        if(kitchenObject == null){
            Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectSOTransform.localPosition = Vector3.zero;
            kitchenObject = kitchenObjectSOTransform.GetComponent<KitchenObject>();
        }


    }
}
