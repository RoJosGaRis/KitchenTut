using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance {get; private set;}
    

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs{
        public ClearCounter selectedCounter;
    }
    

    [SerializeField] private int speed;
    [SerializeField]private float playerRadius = 0.7f, playerHeight = 2f, moveDistance, interactDistance = 2f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    

    private bool isWalking;
    private Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake(){
        if(Instance != null) Debug.LogError("There is more than one Player");
        Instance = this;
    }

    private void Start(){
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e){
        if(selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }
    private void Update(){
        HandleMovement();
        HandleInteractions();
    }
    private void HandleInteractions(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDirection != Vector3.zero) lastInteractDirection = moveDirection;

        if(Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask)){
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)){
                if(clearCounter != selectedCounter){
                    SetSelectedCounter(clearCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDistance = Time.deltaTime * speed;
        
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
        
        if(!canMove){
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);
            if(canMove){
                moveDirection = moveDirectionX;
            } else {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);
                if(canMove){
                    moveDirection = moveDirectionZ;
                }
            }
        }

        if(canMove) transform.position += moveDistance * moveDirection;

        isWalking = moveDirection != Vector3.zero;
        if(isWalking) transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * 10f);
    }

    public bool IsWalking(){
        return isWalking;
    }
    private void SetSelectedCounter(ClearCounter selectedCounter){
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{selectedCounter = selectedCounter});
    }

    public Transform GetKitchenObjectFollowTransform(){
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
}
