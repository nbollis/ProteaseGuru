namespace Tasks;
public class GlobalParameters : ParameterBaseClass<GlobalParameters>, IEquatable<GlobalParameters>
{
    public static string DefaultGlobalParametersFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GlobalParameters.toml");

    public bool IsRnaMode { get; set; } = false;
    public RunParameters? DefaultParameters { get; set; } = null;
    public bool AskAboutModeSwitch { get;  set; } = true;
    /// <summary>
    /// Saved Result if user checked "Remember My Decisions" in the mode warning pop-up
    /// </summary>
    public ModeSwitchResult CachedModeSwitchResult { get; set; } = ModeSwitchResult.Cancel;
    public GlobalParameters Clone()
    {
        return new GlobalParameters
        {
            IsRnaMode = this.IsRnaMode,
            AskAboutModeSwitch = this.AskAboutModeSwitch,
            CachedModeSwitchResult = this.CachedModeSwitchResult,
            DefaultParameters = this.DefaultParameters?.Clone()
        };
    }

    public bool Equals(GlobalParameters? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IsRnaMode == other.IsRnaMode && Equals(DefaultParameters, other.DefaultParameters) && AskAboutModeSwitch == other.AskAboutModeSwitch && CachedModeSwitchResult == other.CachedModeSwitchResult;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GlobalParameters)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IsRnaMode, DefaultParameters, AskAboutModeSwitch, (int)CachedModeSwitchResult);
    }
}

/// <summary>
/// Event arguments for requesting mode switch confirmation from the UI
/// </summary>
public class ModeSwitchRequestEventArgs : EventArgs
{
    public ModeSwitchResult Result { get; set; } = ModeSwitchResult.Cancel;
    public bool RememberMyDecision { get; set; } = false;
}

/// <summary>
/// Represents the user's choice when switching modes
/// </summary>
public enum ModeSwitchResult
{
    /// <summary>
    /// User chose to cancel the mode switch
    /// </summary>
    Cancel,

    /// <summary>
    /// User chose to switch modes and keep all loaded files
    /// </summary>
    SwitchKeepFiles,

    /// <summary>
    /// User chose to switch modes and remove all loaded files
    /// </summary>
    SwitchRemoveFiles
}
