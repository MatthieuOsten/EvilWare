using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    #region SINGLETON

    /// <summary>
    ///  Force a avoir qu'un seul LevelManager
    /// </summary>
    [SerializeField] private static LevelManager _instance = null;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManager>();
                // Si vrai, l'instance va etre cree
                if (_instance == null)
                {
                    var newObjectInstance = new GameObject();
                    newObjectInstance.name = typeof(LevelManager).ToString();
                    _instance = newObjectInstance.AddComponent<LevelManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    #region ASCESSEUR
    #endregion

    #region VARIABLE

    [Header("INPUT")]
    [SerializeField] protected InputThree _inputs;

    [Header("SCENE")]
    [SerializeField] private string _sceneIntro;
    [SerializeField] private string _sceneChrono;

    [Header("Intro")]
    /// <summary> Create TextBeautiful class for fun and beautiful display </summary>
    [SerializeField] private string _objective;

    [Header("CHRONO")]
    [SerializeField] private float _timerChrono;
    [SerializeField] private float _timeToChrono;

    #endregion

    #region FUNCTION

    protected virtual void StartLevel()
    {
        SceneManager.LoadScene(_sceneIntro, LoadSceneMode.Additive);
        GameManager.Instance.State = GameManager.GameState.Cinematic;
    }

    protected virtual void UpdateLevel()
    {
        SceneManager.LoadScene(_sceneChrono, LoadSceneMode.Additive);
    }

    public void FinishIntro()
    {
        SceneManager.UnloadSceneAsync(_sceneIntro);
        GameManager.Instance.State = GameManager.GameState.Ingame;
    }

    #endregion

}