using System.Collections.Generic;
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
    [SerializeField, Tooltip("Set what is being said.")]
    private bool hasChoices;
    [SerializeField, Tooltip("Set what is being said.")]
    private string choiceA;
    [SerializeField, Tooltip("Set what is being said.")]
    private string choiceB;
    [SerializeField, Tooltip("Set what is being said.")]
    private string choiceC;
    [SerializeField, Tooltip("")]
    private int[] choiceToShot = null;
    [SerializeField, Tooltip("")]
    private int toShot = 0;

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

    public bool HasChoices
    {
        get { return hasChoices; }
    }

    public string ChoiceA
    {
        get { return choiceA; }
    }

    public string ChoiceB
    {
        get { return choiceB; }
    }

    public string ChoiceC
    {
        get { return choiceC; }
    }

    public int[] ChoiceToShot
    {
        get { return choiceToShot; }
    }

    public int ToShot
    {
        get { return toShot; }
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
    [SerializeField, Tooltip("Set the Game Object of Choice A.")]
    private TMP_Text choiceA;
    [SerializeField, Tooltip("Set the Game Object of Choice B.")]
    private TMP_Text choiceB;
    [SerializeField, Tooltip("Set the Game Object of Choice C.")]
    private TMP_Text choiceC;
    
    [Header("Visual Novel")]
    [SerializeField, Tooltip("Set the visual novel shots.")]
    private VNShot[] vnShots = null;
    
    private int vnShotIndex = 0;
    private bool waitForButton = false;
    
    private void Awake()
    {
        if (VisualNovelSetUp())
        {
            VisualNovelShowShot(vnShots[vnShotIndex]);
        }
    }

    void Update()
    {
        if (VisualNovelSetUp() && Input.GetMouseButtonDown(0) && !waitForButton)
        {
            if (vnShots[vnShotIndex].HasChoices)
            {
                choiceA.transform.parent.gameObject.SetActive(true);
                choiceB.transform.parent.gameObject.SetActive(true);
                choiceC.transform.parent.gameObject.SetActive(true);
                waitForButton = true;
            }
            else
            {
                vnShotIndex = vnShots[vnShotIndex].ToShot;
                if (vnShotIndex >= vnShots.Length)
                    SceneManager.LoadScene(0);
                else
                    VisualNovelShowShot(vnShots[vnShotIndex]);
            }
        }
    }

    private void VisualNovelShowShot(VNShot vnShot)
    {
        image.sprite = vnShot.BackgroundSprite;
        nameText.SetText(vnShot.Name);
        conversationText.SetText(vnShot.Conversation);

        choiceA.SetText(vnShot.ChoiceA);
        choiceB.SetText(vnShot.ChoiceB);
        choiceC.SetText(vnShot.ChoiceC);
        
        choiceA.transform.parent.gameObject.SetActive(false);
        choiceB.transform.parent.gameObject.SetActive(false);
        choiceC.transform.parent.gameObject.SetActive(false);
    }

    private bool VisualNovelSetUp()
    {
        if (image == null || nameText == null || conversationText == null || vnShots == null || vnShots.Length <= 0 || choiceA == null || choiceB == null || choiceC == null)
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

    public void ChoiceA()
    {
        vnShotIndex = vnShots[vnShotIndex].ChoiceToShot[0];
        waitForButton = false;
        VisualNovelShowShot(vnShots[vnShotIndex]);
    }

    public void ChoiceB()
    {
        vnShotIndex = vnShots[vnShotIndex].ChoiceToShot[0];
        waitForButton = false;
        VisualNovelShowShot(vnShots[vnShotIndex]);
    }

    public void ChoiceC()
    {
        vnShotIndex = vnShots[vnShotIndex].ChoiceToShot[0];
        waitForButton = false;
        VisualNovelShowShot(vnShots[vnShotIndex]);
    }
    
}
