using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

[System.Serializable]
public class PlayfabManager : MonoBehaviour
{

#if UNITY_EDITOR

    [Header("DEBUG")]
    [SerializeField] private bool _debugLogin;
    [SerializeField] private bool _debugUpdateName;
    [SerializeField] private bool _debugAddHigscore;
    [SerializeField] private bool _debugUpdateLeaderboard;

    private void OnValidate()
    {
        if (_debugLogin)
        {
            _debugLogin = false;
            Login();
        }

        if (_debugUpdateName)
        {
            _debugUpdateName = false;
            UpdateDisplayName();
        }

        if (_debugAddHigscore)
        {
            _debugAddHigscore = false;
            SendLeaderboard();
        }

        if (_debugUpdateLeaderboard)
        {
            _debugUpdateLeaderboard = false;
            GetLeaderboard();
        }
    }


#endif

    [SerializeField] private string _nameLeaderboard = "Score";
    [SerializeField] [Range(1,100)] private int _leaderboardCount = 20;

    // ------ LOGIN ------
    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };

        GameManager.Instance.ThePlayer.SetID(request.CustomId);

        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private void OnSuccess(LoginResult result)
    {
        GameManager.Instance.ThePlayer.SetConnected(true);

        Debug.Log("Successful login/account create !");
    }

    private void OnError(PlayFabError error)
    {
        GameManager.Instance.ThePlayer.SetConnected(false);

        Debug.LogWarning("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    // ------ LEADERBOARD ------
    public void SendLeaderboard()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = _nameLeaderboard,
                    Value = GameManager.Instance.ThePlayer.Score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        GameManager.Instance.ThePlayer.SetConnected(true);

        Debug.Log("Successfull leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = _nameLeaderboard,
            StartPosition = 0,
            MaxResultsCount = _leaderboardCount
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        GameManager.Instance.ThePlayer.SetConnected(true);

        GameManager.Instance.Leaderboard.Clear();

        foreach (var item in result.Leaderboard)
        {
            Debug.Log( '|' + item.Position + "| " + item.PlayFabId + " | Score : " + item.StatValue);

            GameManager.Instance.Leaderboard.Add(new GameManager.HightScore(item.Position,item.DisplayName,item.StatValue));

        }
    }

    // ------ NAME ------
    public void UpdateDisplayName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = GameManager.Instance.ThePlayer.Name
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        GameManager.Instance.ThePlayer.SetConnected(true);

        Debug.Log("Successfull display name updated");
    }
}
