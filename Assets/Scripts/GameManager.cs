using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I { get; private set; }
    private GameObject _playerObj;
    private List<GameObject> _EnemyObj = new List<GameObject>();
    public GameObject Player;
    public Transform PlayerTransform;
    public List<GameObject> Enemys;

    private Transform _playerSpawnPos;
    [SerializeField] private List<GameObject> _EnemySpawnPos;

    private bool _iscoroutines = false;  

    private void Awake()
    {
        if(I == null)
        {
            I = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        if (_iscoroutines) return;
        _EnemyObj.Add(Resources.Load<GameObject>("EnemyJelly 0"));
        _playerObj = Resources.Load<GameObject>("Player");

        _playerSpawnPos = GameObject.FindGameObjectWithTag("PlayerSpawnPos").transform;
        _EnemySpawnPos = GameObject.FindGameObjectsWithTag("EnemySpawnPos").ToList();
        EnemySpawn();
        //TODO: �Ѵ� ���ÿ� �����Ǹ� ������ ���� �����Ǹ鼭 Ʈ���ſ� �ɷ��� �׷� �÷��̾� ������ ���� �������;
        //PlayerSpawn();
        StartCoroutine(test());
        _iscoroutines = true;
    }

    void EnemySpawn()
    {
        foreach (GameObject obj in _EnemySpawnPos)
        {
            Enemys.Add(Instantiate(_EnemyObj[0], obj.transform.position, Quaternion.identity));
        }

    }
    IEnumerator test()
    {
        yield return new WaitForSeconds(1);
        PlayerSpawn();
    }
    void PlayerSpawn()
    {
        Player = Instantiate(_playerObj, _playerSpawnPos.position, Quaternion.identity);
        CameraMoveMent.I.Player = Player;
    }
    public void EndGame()
    {
        //TODO: ���� ��ü �ʱ�ȭ
    }
    public void StageEndCheck()
    {
        if(StageManager.I.StageStart && Enemys.Count <= 0)
        {
            StageManager.I.StageStart = false;           
        }
    }
}
