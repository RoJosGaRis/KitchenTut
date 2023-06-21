using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private void Update(){
        
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * Time.deltaTime * speed;

        isWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * 10f);
    }

    public bool IsWalking(){
        return isWalking;
    }
}
