using UnityEngine;

public class Loss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            Debug.Log("YOU LOST!");
        }
    }
}
