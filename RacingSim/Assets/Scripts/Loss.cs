using System;
using UnityEngine;

public class Loss : MonoBehaviour
{
    public event Action<string> OnLevelFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            OnLevelFinished?.Invoke("You lost!");
        }
    }
}
