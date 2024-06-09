using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float _ghostDelay = 0.02f;
    private float _ghostDelaySeconds;
    private Sprite _ghostSprite;

    public GameObject Ghostobj;
    public bool IsmakeGhost = false;

    private bool _initialize = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (_initialize)
            return;
        _ghostDelaySeconds = _ghostDelay;
        _ghostSprite = GetComponent<SpriteRenderer>().sprite;

        _initialize = true;
    }

    void FixedUpdate()
    {
        if (IsmakeGhost)
        {
            if (_ghostDelaySeconds > 0)
            {
                _ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(Ghostobj, transform.position, transform.rotation);
                currentGhost.transform.localScale = transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = _ghostSprite;
                _ghostDelaySeconds = _ghostDelay;
                Destroy(currentGhost, 0.5f);
            }
        }
    }
}
