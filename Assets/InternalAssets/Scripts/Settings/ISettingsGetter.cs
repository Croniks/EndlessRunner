using System;

public interface ISettingsGetter 
{
    public event Action SettingsChanged;

    public float SomeFloatSetting { get; }
    public SomeSubSettings SomeSubSettings { get; }
}