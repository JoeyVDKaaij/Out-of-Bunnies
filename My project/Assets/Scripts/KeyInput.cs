using UnityEngine;

public class KeyInput : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField, Tooltip("Set the key required to press to activate the note detection.")]
    private KeyCode key;
    
    private bool _collideWithNote = false;
    private CleanDishScript _note;
    private Material _m;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _m = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _m.color = Color.gray;
        if (Input.GetKey(key))
        {
            if (_collideWithNote)
            {
                Debug.Log("Hit Note");
                _m.color = Color.green;
                // _note.CleanDish();
            }
            else
            {
                Debug.Log("No Note Hit");
                _m.color = Color.red;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            _collideWithNote = true;
            _note = other.gameObject.GetComponent<CleanDishScript>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            if (other.gameObject.GetComponent<CleanDishScript>() == _note)
            {
                _collideWithNote = false;
                _note = null;
            }
        }
    }
}
