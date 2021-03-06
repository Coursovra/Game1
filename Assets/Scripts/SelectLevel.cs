﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    #region fields
    public Text LevelName;
    public Button PlayButton;
    public Camera Level01Camera;
    public Camera Level02Camera;
    public Camera LevelBossCamera;
    public Camera MainCamera;
    public GameObject Island01;
    public GameObject Island02;
    public GameObject IslandBoss;
    public Light DirectionalLight;
    private string _activeLevelInfo;
    private Ray _ray;
    private RaycastHit _hit;
    private bool _showingInfo;
    #endregion

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        _ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(MainCamera.transform.position, _ray.direction, out _hit, 100f))
            {
                if (_hit.transform.name == "Level01Island")
                    Level01();
                if (_hit.transform.name == "Level02Island")
                    Level02();
                if (_hit.transform.name == "BossIsland")
                    LevelBoss();
            }
        }
    }

    public void Back_Button()
    {
        if(!_showingInfo)
         SceneManager.LoadScene("Scenes/MainMenu");
        else
        {
            Island01.SetActive(true);
            Island02.SetActive(true);
            IslandBoss.SetActive(true);
            SwitchUI(false, "");
            switch (_activeLevelInfo)
            {
                case "Level01":
                    Level01Camera.gameObject.SetActive(false);
                    break;
                case "Level02":
                    Level02Camera.gameObject.SetActive(false);
                    break;
                case "LevelBoss":
                    LevelBossCamera.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public void Play_Button()
    {
        SceneManager.LoadScene(_activeLevelInfo);
    }

    private void Level01()
    {
        SwitchUI(true, "Level 1");
        _activeLevelInfo = "Level01";
        Level01Camera.gameObject.SetActive(true);
        Island02.SetActive(false);
        IslandBoss.SetActive(false);
    }
    private void Level02()
    {
        SwitchUI(true, "Level 2");
        _activeLevelInfo = "Level02";
        Level02Camera.gameObject.SetActive(true);
        Island01.SetActive(false);
        IslandBoss.SetActive(false);
    }
    
    private void LevelBoss()
    {
        SwitchUI(true, "Boss");
        _activeLevelInfo = "LevelBoss";
        LevelBossCamera.gameObject.SetActive(true);
        Island01.SetActive(false);
        Island02.SetActive(false);
    }

    private void SwitchUI(bool value, string level)
    {
        if(level == "Boss")
            DirectionalLight.gameObject.SetActive(false);
        else
            DirectionalLight.gameObject.SetActive(true);
        MainCamera.gameObject.SetActive(!value);
        _showingInfo = value;
        LevelName.gameObject.SetActive(value);
        if(level != "")
            LevelName.text = level;
        else
            LevelName.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(value);
    }
}