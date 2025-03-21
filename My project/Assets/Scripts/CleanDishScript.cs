using UnityEngine;

public class CleanDishScript : MonoBehaviour
{
    [Header("Material Settings")]
    [SerializeField, Tooltip("Set the clean material")]
    private Texture cleanDishMaterial;

    private Material _m;
    
    private void Start()
    {
        _m = GetComponent<Renderer>().material;
    }

    // Change texture to a clean dish texture
    public void CleanDish()
    {
        _m.mainTexture = cleanDishMaterial;
    }
}
