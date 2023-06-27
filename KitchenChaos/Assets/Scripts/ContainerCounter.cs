using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPLayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSO.prefab);
            
            kitchenObjectSOTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPLayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
