using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DateWave
{
    [SerializeField, Tooltip("Set up the possible dates.")]
    private DateScriptableObject[] dates;
    [SerializeField, Tooltip("Set up the possible dates.")]
    private bool saveDates = true;
    [SerializeField, Min(0),
     Tooltip("Set the maximum amount of dates that the player can have. If set to 0, " +
             "the player will have a date with everyone in this wave.")]
    private int maxAmountOfDates;
    
    private List<DateScriptableObject> _datesVisited;
    
    public DateScriptableObject[] Dates
    {
        get { return dates; }
    }
    public bool SaveDates
    {
        get { return saveDates; }
    }
    public int MaxAmountOfDates
    {
        get { return maxAmountOfDates; }
    }
}

public class DateManager : MonoBehaviour
{
    [Header("Date Settings")]
    [SerializeField, Tooltip("Set up the possible dates.")]
    private DateScriptableObject[] dates;
    [SerializeField]
    private DateWave[] dateWaves;
    
    private List<DateScriptableObject> _datesAccepted;
    
    #region SingletonSetUp
    
    public static DateManager instance { get; protected set; }

    protected virtual void Awake()
    {
        _datesAccepted = new List<DateScriptableObject>();
        SetInstance();
    }

    protected virtual void OnEnable()
    {
        SetInstance();
    }

    protected virtual void OnDestroy()
    {
        instance = null;
    }

    protected virtual void OnDisable()
    {
        instance = null;
    }
    
    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
                
            if (transform.parent.gameObject != null) DontDestroyOnLoad(transform.parent.gameObject);
            else DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void AcceptDate(DateScriptableObject date)
    {
        if (dateWaves[0].SaveDates && _datesAccepted != null)
            _datesAccepted.Add(date);
    }

    public DateScriptableObject GetRandomDate()
    {
        if (dates != null && dates.Length > 1)
        {
            return dates[Random.Range(0, dates.Length)];
        }
        else if (dates != null)
        {
            return dates[0];
        }
        
        return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _datesAccepted != null)
        {
            Debug.Log("------- Accepted Dates -------");
            foreach (DateScriptableObject date in _datesAccepted)
            {
                Debug.Log(date.name);
            }
            Debug.Log("------------------------------");
        }

        if (Input.GetKeyDown(KeyCode.Delete) && _datesAccepted != null)
        {
            _datesAccepted.Clear();
            Debug.Log("------- Accepted Dates Removed --------");
        }
    }

    public DateWave[] DateWaves
    {
        get { return dateWaves; }
    }
}
