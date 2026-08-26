using System;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheelColliders;

    public event Action<string> OnLevelFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            OnLevelFinished?.Invoke("You win!");
        }
    }    
}