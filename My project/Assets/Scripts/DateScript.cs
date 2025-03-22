using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateScript : MonoBehaviour
{
    #region Variables
    [Header("Set Up Date Settings")]
    [SerializeField, Tooltip("Set the tText Object where the name will be displayed.")]
    private TMP_Text dateNameText;
    [SerializeField, Tooltip("Set the image Object where the sprite will be displayed.")]
    private Image dateImage;
    
    private DateManager _dm;
    private DateScriptableObject _currentDate = null;
    #endregion
    
    #region Unity Methods
    void Start()
    {
        _dm = DateManager.instance;
    }

    void Update()
    {
        if (ReadyToDate())
        {
            if (_currentDate == null)
            {
                _currentDate = _dm.GetRandomDate();
                return;
            }
            
            dateNameText.SetText(_currentDate.name);
            
            dateImage.sprite = _currentDate.sprite;
        }
    }
    #endregion
    
    #region Methods
    public void AcceptCurrentDate()
    {
        if (ReadyToDate() && _currentDate != null)
        {
            _dm.AcceptDate(_currentDate);
            _currentDate = null;
        }
    }

    public void RejectCurrentDate()
    {
        _currentDate = null;
    }

    private bool ReadyToDate()
    {
        if (_dm == null)
        {
            Debug.LogError("There is no DateManager in the scene.");
            _dm = DateManager.instance;
            return false;
        }
        else if (_dm.Dates == null || _dm.Dates.Length < 1)
        {
            Debug.LogError("There are no dates in the dateManager.");
            return false;
        }
        if (dateNameText == null)
        {
            Debug.LogError("There is no text object that holds the name.");
            return false;
        }
        if (dateImage == null)
        {
            Debug.LogError("There is no image object that shows the date.");
            return false;
        }
        
        return true;
    }
    #endregion
}
