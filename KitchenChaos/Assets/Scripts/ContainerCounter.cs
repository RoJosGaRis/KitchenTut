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
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPLayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
