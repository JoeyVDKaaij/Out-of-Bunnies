using UnityEngine;

public class DestroyNotesScript : MonoBehaviour
{
    // Destroy Note on enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note")) Destroy(other.gameObject);
    }
}
