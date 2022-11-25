using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayfabManager))]
public class GameManager : MonoBehaviour
{

    #region GAMESTATE
    public enum GameState
    {
        Ingame,

        Menu,

        Tutorial,
        Paused,

        Victory,
        Gameover,

        Cinematic
    }

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Infinity
    }

    #endregion

    #region SINGLETON

    /// <summary>
    ///  Force a avoir qu'un seul LevelManager
    /// </summary>
    [SerializeField] private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                // Si vrai, l'instance va etre cree
                if (_instance == null)
                {
                    var newObjectInstance = new GameObject();
                    newObjectInstance.name = typeof(GameManager).ToString();
                    _instance = newObjectInstance.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    #region STRUCT

    [System.Serializable]
    public class Player
    {
        [Header("IDENTIFY")]
        [SerializeField] private string _name;
        [SerializeField] private string _id;

        [Header("SCORE")]
        [SerializeField] private int _score;

        [Header("NETWORK")]
        [SerializeField] private bool _isConnected;

        public string Name { get { return _name; } }
        public string ID { get { return _id; } }
        public bool IsConnected { get { return _isConnected; } }
        public int Score { get { return _score; } }

        public Player(string name)
        {
            _name = name;
            _id = SystemInfo.deviceUniqueIdentifier;
            _isConnected = false;
            _score = 0;
        }

        public Player(string name,string id)
        {
            _name = name;
            _id = id;
            _isConnected = false;
            _score = 0;
        }

        public void SetName(string valueString)
        {
            Debug.Log("Name : " + _name + " Change for " + valueString);
            _name = valueString;
        }
        public void SetID(string valueString)
        {
            Debug.Log("ID : " + _id + " Change for " + valueString);
            _id = valueString;
        }
        public void SetConnected(bool valueBool)
        {
            Debug.Log("IsConnected : " + _isConnected + " Change for " + valueBool);
            _isConnected = valueBool;
        }
        public void SetScore(int valueInt)
        {
            Debug.Log("Score : " + _score + " Change for " + valueInt);
            _score = valueInt;
        }

    }

    [System.Serializable]
    public struct HightScore
    {
        [SerializeField] private string _name;

        public int rank;
        public string namePlayer;
        public int score;

        public HightScore(int place = -1, string name = "Billy", int score = 0)
        {
            this.rank = place;
            this.namePlayer = name;
            this.score = score;

            _name = rank.ToString();
        }
    }

    #endregion

    #region VARIABLE

    [Header("GAME")]
    [SerializeField] private GameState _state;
    [SerializeField] private Difficulty _actualDifficulty = Difficulty.Easy;

    [Header("INPUT")]
    [SerializeField] private InputThree _inputManager;

    [Header("PLAYER")]
    [SerializeField] private Player _player;

    [Header("NETWORK")]
    [SerializeField] private PlayfabManager _playfab;
    [SerializeField] private List<HightScore> _leaderboard = new List<HightScore>();

    #endregion

    #region ASCESSEUR

    public List<HightScore> Leaderboard { get { return _leaderboard; } }

    public InputThree Inputs { get { return _inputManager; } }
    public PlayfabManager PlayfabManager { get { return _playfab; } }

    public Player ThePlayer { 
        get { return _player; } 
    }

    public GameState State
    {
        get { return _state; }
        set
        {

            _state = value;

        }
    }
    public Difficulty GetDifficulty { get { return _actualDifficulty; } }
    public int Score { get { return ThePlayer.Score; } }

    #endregion

    #region FUNCTION UNITY

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _player = new Player("Billy");

            // Create playfab object for connecting game to playfab server
            if (_playfab == null)
            {
                if (!TryGetComponent<PlayfabManager>(out _playfab)) {
                    _playfab = gameObject.AddComponent<PlayfabManager>();
                }
            }

            _playfab.Login();
    }

        private void Start()
        {
            EnableInputManager();
        }

    #endregion

    #region FUNCTION

    /// <summary>
    /// Active l'input manager
    /// </summary>
    public void EnableInputManager()
    {
        // On verifie qu'il soit referencer
        if (_inputManager == null) _inputManager = new InputThree();

        Debug.Log("Active les touches");

        // On active les touches
        _inputManager.Default.Enable();
        
    }

    /// <summary>
    /// Desactive l'input manager
    /// </summary>
    public void DisableInputManager()
    {
        // Verify inputManager is null also create this
        if (_inputManager == null) _inputManager = new InputThree();

        Debug.Log("Active les touches");

        // Disable all touch
        _inputManager.Default.Disable();
    }

    #endregion

}
