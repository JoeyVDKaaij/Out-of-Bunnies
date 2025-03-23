using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Date", menuName = "Scriptable Objects/Date")]
public class DateScriptableObject : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public SceneAsset visualNovelScene;
}
