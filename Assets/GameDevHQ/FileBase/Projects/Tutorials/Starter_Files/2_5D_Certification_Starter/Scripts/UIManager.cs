using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _score;
    private static UIManager _instance;
    public static UIManager _Instance
    {
        get
        {
            if (_instance==null)
            {
                Debug.Log("_instance is null");
            }
            return _instance;
        }         
    }
    private void Awake()
    {
        _instance = this;
    }
    public void ScoreUpdate(int totalScore)
    {
        _score.text = "Score: " + totalScore.ToString();
    }
}
