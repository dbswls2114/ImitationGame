using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager I;
    public string[] Stagenames = new string[4] { "StartScenes", "Stage1", "Stage2", "Stage3" };
    public int ThisStage { get; private set; } = 0;

    public bool StageStart = false;
    private bool _iscoroutines = false;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }

    }
    public void Init()
    {
        if (_iscoroutines) return;
        ThisStage = 0;

        _iscoroutines = true;
    }
    void Start()
    {
        Init();
    }
    public void StageClear()
    {

    }

    void NextStageGo()
    {
        ThisStage++;
        if (ThisStage < Stagenames.Length)
        {
            SceneManager.LoadScene(Stagenames[ThisStage]);
            StageStart = true;
        }
        else
        {
            //TODO : endgame
        }
    }

}
