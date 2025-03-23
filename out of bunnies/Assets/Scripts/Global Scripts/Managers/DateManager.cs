using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JoUnityAddOn;
using OGSceneManagment = UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using JoUnityAddOn.SceneManagement;
using UnityEditor;

[Serializable]
public class DateWave
{
    [Header("Date Wave Settings")]
    [SerializeField, Tooltip("Set up the possible dates.")]
    private DateScriptableObject[] dates;
    [SerializeField, Tooltip("Set to true if the dates should be saved.")]
    private bool saveDates = true;
    [SerializeField, Min(0),
     Tooltip("Set the maximum amount of dates that the player can have. If set to 0, " +
             "the player will have a date with everyone in this wave.")]
    private int maxAmountOfDates = 0;
    
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
    [SerializeField, Tooltip("Set to true if the game should use the date wave mechanic.")]
    private bool useDateWaves = false;
    [SerializeField, Tooltip("Set up the possible dates.")]
    private DateScriptableObject[] dates;
    
    [SerializeField]
    private DateWave[] dateWaves;
    [SerializeField, Tooltip("Set up when the pop shows up."), Min(1)]
    private int popUpAfterWave = 1;
    [SerializeField, Tooltip("Set up when the date app ends. If set to 0, it will end once every wave has passed.")]
    private int endDateApp = 3;
    
    [SerializeField, Tooltip("Set the Pop Up Game Object.")]
    private GameObject popUpObject = null;
    [SerializeField, Tooltip("Set the Pop Up Game Object.")]
    private SceneAsset scene = null;

    private List<DateScriptableObject> _datesAccepted;
    private Queue<DateScriptableObject> _datesQueued;
    private DateWave _currentDateWave;
    private int _dateWaveId;
    
    public static DateManager instance { get; protected set; }
    
    #region Startup and Cleanup

    protected virtual void Awake()
    {
        _datesAccepted = new List<DateScriptableObject>();
        _datesQueued = new Queue<DateScriptableObject>();
        if (dateWaves != null && dateWaves.Length > 0)
        {
            SetNextWave();
        }
        
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
                
            // if (transform.parent.gameObject != null) DontDestroyOnLoad(transform.parent.gameObject);
            // else DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void AcceptDate(DateScriptableObject date)
    {
        if (dateWaves[_dateWaveId].SaveDates && _datesAccepted != null)
            _datesAccepted.Add(date);
    }

    public DateScriptableObject GetDate()
    {
        if (useDateWaves)
        {
            if (_datesQueued.Count <= 0)
                SetNextWave();
            
            return _datesQueued.Dequeue();
        }
        
        if (dates != null && dates.Length > 1)
        {
            return dates[Random.Range(0, dates.Length)];
        }
        if (dates != null)
        {
            return dates[0];
        }
        
        return null;
    }

    private void SetNextWave()
    {
        if (_currentDateWave == null)
        {
            _currentDateWave = dateWaves[0];
            
            List<DateScriptableObject> dates = RandomizeDates(_currentDateWave.Dates);
            
            foreach (DateScriptableObject date in dates)
                _datesQueued.Enqueue(date);
            
            return;
        }

        

        _dateWaveId++;
        if (_dateWaveId == popUpAfterWave && popUpObject != null)
        {
            popUpObject.SetActive(true);
        }
        else if (_dateWaveId >= endDateApp && _dateWaveId != 0)
        {
            if (scene != null)
            {
                SceneManager.LoadScene(scene.name);
                return;
            }
            Debug.Log(_datesAccepted != null && _datesAccepted.Count > 0);
            if (_datesAccepted != null && _datesAccepted.Count > 0)
            {
                DateScriptableObject date = _datesAccepted[Random.Range(0, _datesAccepted.Count)];
                OGSceneManagment.Scene scene = SceneManager.GetSceneByName(date.visualNovelScene.name);
                Debug.Log($"{scene} scene with build index {scene.buildIndex} from {date}");
                if (scene != null) SceneManager.LoadScene(date.visualNovelScene.name);
                else SceneManager.LoadScene(0);
            }
            else SceneManager.LoadScene(0);
        }
        Debug.Log("------- Next Date Wave --------");
        if (_dateWaveId >= dateWaves.Length)
            _dateWaveId = 0;
        
        _currentDateWave = dateWaves[_dateWaveId];
        
        foreach (DateScriptableObject date in _currentDateWave.Dates)
            _datesQueued.Enqueue(date);
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

    private List<DateScriptableObject> RandomizeDates(DateScriptableObject[] dates)
    {
        List<DateScriptableObject> dateList = new List<DateScriptableObject>();
        foreach (DateScriptableObject date in dates)
            dateList.Add(date);
        
        dateList = dateList.OrderBy(_ => Random.value).ToList();
        
        return dateList;
    }
    
    public DateScriptableObject[] Dates
    {
        get { return dates; }
    }
    
    public DateWave[] DateWaves
    {
        get { return dateWaves; }
    }

    public bool UseDateWaves
    {
        get { return useDateWaves; }
    }
}
