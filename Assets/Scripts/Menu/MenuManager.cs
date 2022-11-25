using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    enum MenuState
    {
        Menu,
        Leaderboard,
        Credits,

        Start
    }

    #region VARIABLE

    [Header("STATE")]
    [SerializeField] private MenuState _menuState = MenuState.Menu;
    [SerializeField] private MenuState _oldState = MenuState.Menu;

    [Header("SCENE")]
    [SerializeField] private string _sceneMenu;
    [SerializeField] private string _sceneStart;

    [Header("INPUTS")]
    [SerializeField] private List<TextMeshProUGUI> _inputsEnter;
    [SerializeField] private List<TextMeshProUGUI> _inputsLeft;
    [SerializeField] private List<TextMeshProUGUI> _inputsRight;

    [Header("LEADERBOARD")]
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private GameObject _leaderboardInput;
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private bool _leaderboardCharged;

    [SerializeField] [Range(0.1f,1f)] private float _scrollSpeed;

    [Header("TIMER")]
    [SerializeField] private float _timer = 0;
    [SerializeField] private float _timeTo = 3;

    /// <summary>
    /// List of Canvas of the scene 
    /// </summary>
    [Tooltip("The element 0 is the first display")]
    [SerializeField] private List<GameObject> listOfCanvas = new List<GameObject>();

    #endregion

    #region ASCESSEUR

    private bool LeaderboardIsCharged
    {
        get { return _leaderboardCharged; }
        set { 
            if (_leaderboardCharged == false && value == true)
            {
                if (_leaderboardInput != null) { _leaderboardInput.SetActive(false); }
                if (_leaderboardPanel != null) { _leaderboardPanel.SetActive(true); }
            }
            else if (_leaderboardCharged == true && value == false)
            {
                if (_leaderboardInput != null) { _leaderboardInput.SetActive(true); }
                if (_leaderboardPanel != null) { _leaderboardPanel.SetActive(false); }
            }
            else if (value == true)
            {

            }

            _leaderboardCharged = value; }
    }

    #endregion

    #region FUNCTION UNITY

    private void Start()
    {
        GameManager.Instance.EnableInputManager();
        GameManager.Instance.State = GameManager.GameState.Menu;

        LeaderboardIsCharged = false;
        SwitchDisplayLeaderboard(false);

        ChangePanel(0);

        if (_leaderboardInput != null) { _leaderboardInput.SetActive(true); }
        if (_leaderboardPanel != null) { _leaderboardPanel.SetActive(false); }

        string enter,left,right;

        enter = GameManager.Instance.Inputs.Default.InputEnter.bindings[0].ToDisplayString();
        left = GameManager.Instance.Inputs.Default.InputLeft.bindings[0].ToDisplayString();
        right = GameManager.Instance.Inputs.Default.InputRight.bindings[0].ToDisplayString();

        if (Application.systemLanguage == SystemLanguage.French)
        { left = "A"; }

        Debug.Log("Application.systemLanguage : " + Application.systemLanguage.ToString() + " | SystemIsoLanguage : " + System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToString());

        foreach (var text in _inputsEnter)
        {
            text.text = enter;
        }

        foreach (var text in _inputsLeft)
        {
            text.text = left;
        }

        foreach (var text in _inputsRight)
        {
            text.text = right;
        }

    }

    private void Awake()
    {
        if (_sceneMenu == null) { _sceneMenu = SceneManager.GetActiveScene().name; }
    }

    void Update()
    {
        InputManager();

        // Moyen claquer au sol mais je sais pas regler le probleme autrement
        // Si le leaderboard n'est pas charger correctement le "redemarre"
        if (_leaderboard.CountList <= 0 && _leaderboardCharged)
        {
            SwitchDisplayLeaderboard(false);
            SwitchDisplayLeaderboard(true);

            if (_leaderboard.CountList <= 0)
                _leaderboardCharged = false;
        }

    }

    #endregion

    #region FUNCTION

    private void InputManager()
    {
        // SCENE DU MENU DU JEU
        if (_oldState == MenuState.Menu)
        {
            // INPUT PRESSED
            if (GameManager.Instance.State == GameManager.GameState.Menu && GameManager.Instance.Inputs != null)
            {
                if (GameManager.Instance.Inputs.Default.InputLeft.IsPressed())
                {
                    GameManager.Instance.State = GameManager.GameState.Cinematic;
                    _menuState = MenuState.Leaderboard;
                    _timer = _timeTo;
                }

                else if (GameManager.Instance.Inputs.Default.InputRight.IsPressed())
                {
                    GameManager.Instance.State = GameManager.GameState.Cinematic;
                    _menuState = MenuState.Credits;
                    _timer = _timeTo;
                }

                else if (GameManager.Instance.Inputs.Default.InputEnter.IsPressed())
                {
                    GameManager.Instance.State = GameManager.GameState.Cinematic;
                    _menuState = MenuState.Start;
                    _timer = _timeTo;
                }
            }

            // TIMER
            if (Timer())
            {
                if (_menuState == MenuState.Leaderboard)
                {
                    SwitchDisplayLeaderboard(true);
                }

                else if (_menuState == MenuState.Credits)
                {
                    ChangePanel("Credits");
                }

                else if (_menuState == MenuState.Start)
                {
                    ChangePanel(0);
                }

                _oldState = _menuState;
            }
        }

        // LEADERBOARD DISPLAY
        else if (_oldState == MenuState.Leaderboard && GameManager.Instance.Inputs != null)
        {
            // INPUT PRESSED
            if (GameManager.Instance.State == GameManager.GameState.Menu)
            {
                if (GameManager.Instance.Inputs.Default.InputLeft.IsPressed())
                {
                    GameManager.Instance.State = GameManager.GameState.Cinematic;
                    if (_leaderboard != null) { _leaderboard.UpList(_scrollSpeed); }
                    _timer = _timeTo;
                }

                else if (GameManager.Instance.Inputs.Default.InputRight.IsPressed())
                {
                    GameManager.Instance.State = GameManager.GameState.Cinematic;
                    if (_leaderboard != null) { _leaderboard.DownList(_scrollSpeed); }
                    _timer = _timeTo;
                }

                else if (GameManager.Instance.Inputs.Default.InputEnter.IsPressed())
                {
                    GameManager.Instance.State = GameManager.GameState.Cinematic;
                    _menuState = MenuState.Menu;
                    _timer = _timeTo;
                }
            }

            // TIMER
            if (Timer())
            {
                if (_menuState == MenuState.Menu)
                {
                    SwitchDisplayLeaderboard(false);
                }

                _oldState = _menuState;
            }

        }

        // SCENE DES CREDITS DU JEU
        else if (_oldState == MenuState.Credits)
        {
            // INPUT PRESSED
            if (GameManager.Instance.State == GameManager.GameState.Menu && GameManager.Instance.Inputs != null)
            {
                if (GameManager.Instance.Inputs.Default.InputRight.IsPressed())
                {
                    GameManager.Instance.State = GameManager.GameState.Cinematic;
                    _menuState = MenuState.Menu;
                    _timer = _timeTo;
                }
            }

            // TIMER
            if (Timer())
            {
                if (_menuState == MenuState.Menu)
                {

                    ChangePanel(0);

                }

                _oldState = _menuState;
            }

        }
    }

    private void SwitchDisplayLeaderboard(bool valueBool)
    {

        LeaderboardIsCharged = valueBool;

        if (valueBool && _leaderboard != null) {
            _leaderboard.UpdateList(GameManager.Instance.Leaderboard); 
        }

        GameManager.Instance.State = GameManager.GameState.Menu;
    }

    private bool Timer()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_timer == 0) { _timer -= 1; }

            return false;
        }
        else if (_timer < 0)
        {
            GameManager.Instance.State = GameManager.GameState.Menu;
            _timer = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Recupere l'index dans "listOfPanels" du panneau designer par un nom par rapport au nom de l'objet
    /// </summary>
    /// <param name="index">Index encore inconnu</param>
    /// <param name="name">Nom du canvas rechercer</param>
    /// <returns>Retourne si le panneau as ete retrouver ou non</returns>
    private bool TryGetPanelIndexFromName(out int index, string name)
    {
        // Recherche dans la liste des canvas
        for (int i = 0; i < listOfCanvas.Count; i++)
        {
            // Si le nom du canvas correspond a l'entree alors le retourne et retourne vrai
            if (listOfCanvas[i].name == name)
            {
                index = i;
                return true;
            }
        }

        // Sinon l'index est egal a 0 et il retourne faux
        index = 0;
        return false;
    }

    /// <summary>
    /// Change de scene
    /// </summary>
    /// <param name="name">Nom de la scene rechercher</param>
    static public void ChangeScene(string name)
    {
        if (SceneManager.GetActiveScene().name != name)
            SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Ajoute une scene
    /// </summary>
    /// <param name="name">Nom de la scene rechercher</param>
    public void AdditiveScene(string name)
    {
        if (SceneManager.GetSceneByName(name).isLoaded == false)
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Change de panneau par rapport a son nom
    /// </summary>
    /// <param name="name">Nom du panneau que l'on veut afficher</param>
    public void ChangePanel(string name)
    {
        int index = 0;

        if (TryGetPanelIndexFromName(out index, name))
        {
            Debug.Log("Panel : " + name + " is actived");
            ChangePanel(index);
        }
        else
        {
            Debug.LogWarning("Panel : " + name + " is not found");
        }
    }

    /// <summary>
    /// Change de panneau par rapport a son index dans "listOfPanels"
    /// </summary>
    /// <param name="index">Index du panneau dans la liste</param>
    public void ChangePanel(int index)
    {
        if (listOfCanvas.Count == 0)
        {
            Debug.LogWarning("List of panel is Empty");
            return;
        }

        for (int i = 0; i < listOfCanvas.Count; i++)
        {
            if (index == i) { listOfCanvas[i].SetActive(true); }
            else { listOfCanvas[i].SetActive(false); }
        }
    }

    #endregion

}
