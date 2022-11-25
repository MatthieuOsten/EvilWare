using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{

#if UNITY_EDITOR

    [SerializeField] private bool _debugCreateContent;
    [SerializeField] private int _debugNbrObject;

    private void OnValidate()
    {
        if (_debugCreateContent)
        {
            _debugCreateContent = false;

            List<GameManager.HightScore> list = new List<GameManager.HightScore>();
            for (int i = 0; i < _debugNbrObject; i++)
            {
                GameManager.HightScore hight = new GameManager.HightScore();
                list.Add(hight);
            }

            GenerateList(list);

        }
    }

#endif

    [SerializeField] private List<HightScoreObject> _listHightScoreObject;

    [SerializeField] private Scrollbar _scrollBar;

    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _prefab;

    public int CountList { get { return _listHightScoreObject.Count; } }

    public void UpList(float valueFloat)
    {
        float checkValue = _scrollBar.value += valueFloat;

        if (checkValue < 0) { _scrollBar.value = 0; }
        if (checkValue > 1) { _scrollBar.value = 1; }
        else { _scrollBar.value = checkValue; }
    }

    public void DownList(float valueFloat)
    {
        float checkValue = _scrollBar.value -= valueFloat;

        if (checkValue < 0 ) { _scrollBar.value = 0; }
        if (checkValue > 1) { _scrollBar.value = 1; }
        else { _scrollBar.value = checkValue; }

    }

    public void UpdateList(List<GameManager.HightScore> listHightScores)
    {
        ResetList();

        GameManager.Instance.PlayfabManager.GetLeaderboard();
        GenerateList(listHightScores);
    }

    private void ResetList()
    {
        foreach (var item in _listHightScoreObject)
        {
            Destroy(item.gameObject);
        }

        _listHightScoreObject.Clear();
    }

    private void GenerateList(List<GameManager.HightScore> listHightScores)
    {
        foreach (var item in listHightScores)
        {
            HightScoreObject hight;
            hight = Instantiate<GameObject>(_prefab,_parent).GetComponent<HightScoreObject>();
            _listHightScoreObject.Add(hight);

            InitialisePlayerInfo(hight, item.rank, item.namePlayer, item.score);

        }
    }

    private void InitialisePlayerInfo(HightScoreObject hight, int rank, string name, int score) 
    {
        if      (rank == 1)
        {
            hight.imageRank.color = Color.yellow;
        }
        else if (rank == 2)
        {
            hight.imageRank.color = Color.grey;
        }
        else if (rank == 3)
        {
            hight.imageRank.color = Color.red;
        }
        else
        {
            hight.imageRank.color = Color.blue;
        }

        hight.rank.text = rank.ToString();
        hight.name.text = name;
        hight.score.text = score.ToString();

    }
}