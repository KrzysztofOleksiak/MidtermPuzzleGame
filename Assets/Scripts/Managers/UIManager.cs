using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text _healtText;

    [SerializeField] private Health _playerhealth;
    [SerializeField] private GameObject _winScreen, _gameOverText, _healthUI;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnEnable()
    {
        _playerhealth.OnHealthUpdate += OnHealthUpdate;
    }


    private void OnDisable()
    {
        _playerhealth.OnHealthUpdate -= OnHealthUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   void OnHealthUpdate(float health)
    {
        _healtText.text = "Health :" + Mathf.Floor(health).ToString(); /// 77.896f --> 78.0f
    }
    public void WinScreen(bool active)
    {
        _healthUI.SetActive(!active);
        _winScreen.SetActive(active);
    }
    public void GameOverScreen()
    {
        _gameOverText.SetActive(true);
        _healthUI.SetActive(false);
    }
}
