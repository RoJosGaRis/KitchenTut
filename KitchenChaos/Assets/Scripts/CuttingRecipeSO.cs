using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject
{
    public int cuttingProgressMax;
    public KitchenObjectSO input;
    public KitchenObjectSO output;
}
