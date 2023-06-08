using System;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class SettingsObject : ScriptableObject, ISettingsGetter, ISettingsSetter
{
    public event Action SettingsChanged;

    public float SomeFloatSetting { get => _someFloatSetting; set => _someFloatSetting = value; }
    [SerializeField, Space, Header("SomeHeader")] private float _someFloatSetting = 100f;

    public SomeSubSettings SomeSubSettings { get => _someSubSettings; set => _someSubSettings = value; }
    [SerializeField] private SomeSubSettings _someSubSettings;


    [SerializeField, Space] private bool _overridePlayerPrefs = false;

    
    public void SaveSettings()
    {
        if (_overridePlayerPrefs == false)
        {
            PlayerPrefs.Save();
        }
        
        SettingsChanged?.Invoke();
    }

    public void LoadSettings()
    {
        if (_overridePlayerPrefs == true)
        {
            return;
        }
    }
}

[Serializable]
public class SomeSubSettings
{
    public IEnumerable<GradeValueCost> SomeGradeSetting => _someGradeSetting;
    [SerializeField] private List<GradeValueCost> _someGradeSetting;


    [Serializable]
    public struct GradeValueCost
    {
        public int upgradeLvl;
        public float value;
        public int cost;
    } 
}

public class PlayerPrefsSettingsNames
{
    
}