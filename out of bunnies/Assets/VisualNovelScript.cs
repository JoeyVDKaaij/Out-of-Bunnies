using JoUnityAddOn.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class VNShot
{
    [SerializeField, Tooltip("Set the background image.")]
    private Sprite backgroundSprite;
    [SerializeField, Tooltip("Set the name of the person speaking.")]
    private string name;
    [SerializeField, Tooltip("Set what is being said.")]
    private string conversation;

    public Sprite BackgroundSprite
    {
        get { return backgroundSprite; }
    }

    public string Name
    {
        get { return name; }
    }

    public string Conversation
    {
        get { return conversation; }
    }
}

public class VisualNovelScript : MonoBehaviour
{
    [Header("Setup Visual Novel")]
    [SerializeField, Tooltip("Set the background image component")]
    private Image image;
    [SerializeField, Tooltip("Set the name text component")]
    private TMP_Text nameText;
    [SerializeField, Tooltip("Set the conversation text component")]
    private TMP_Text conversationText;
    
    [Header("Visual Novel")]
    [SerializeField, Tooltip("Set the visual novel shots.")]
    private VNShot[] vnShots = null;
    
    private int vnShotIndex = 0;
    
    private void Awake()
    {
        if (VisualNovelSetUp())
        {
            VisualNovelShowShot(vnShots[vnShotIndex]);
        }
    }

    void Update()
    {
        if (VisualNovelSetUp() && Input.GetMouseButtonDown(0))
        {
            vnShotIndex++;
            if (vnShotIndex >= vnShots.Length)
                SceneManager.LoadScene(0);
            else
                VisualNovelShowShot(vnShots[vnShotIndex]);
        }
    }

    private void VisualNovelShowShot(VNShot vnShot)
    {
        image.sprite = vnShot.BackgroundSprite;
        nameText.SetText(vnShot.Name);
        conversationText.SetText(vnShot.Conversation);
    }

    private bool VisualNovelSetUp()
    {
        if (image == null || nameText == null || conversationText == null || vnShots == null || vnShots.Length <= 0)
        {
            Debug.Log("Something is not set up right!");
            Debug.Log($"{image == null}");
            Debug.Log($"{nameText == null}");
            Debug.Log($"{conversationText == null}");
            Debug.Log($"{vnShots == null}");
            Debug.Log($"{vnShots.Length <= 0}");
            return false;
        }
        
        return true;
    }
}
