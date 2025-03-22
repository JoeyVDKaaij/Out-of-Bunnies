using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour
{
    [Header("Date Settings")]
    [SerializeField, Tooltip("Set up the possible dates.")]
    private DateScriptableObject[] dates;
    
    private List<DateScriptableObject> datesAccepted;
    
    #region SingletonSetUp
    
    public static DateManager instance { get; protected set; }

    protected virtual void Awake()
    {
        datesAccepted = new List<DateScriptableObject>();
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
        datesAccepted.Add(date);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("------- Accepted Dates -------");
            foreach (DateScriptableObject date in datesAccepted)
            {
                Debug.Log(date.name);
            }
            Debug.Log("------------------------------");
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            datesAccepted.Clear();
            Debug.Log("------- Accepted Dates Removed --------");
        }
    }

    public DateScriptableObject[] Dates
    {
        get { return dates; }
    }
}
