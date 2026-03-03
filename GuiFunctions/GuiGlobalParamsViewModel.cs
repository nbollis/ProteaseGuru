using Engine;
using Nett;
using Tasks;

namespace GuiFunctions;
public class GuiGlobalParamsViewModel : BaseViewModel
{
    public static string DefaultGlobalParametersFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GlobalParameters.toml");
    private static GuiGlobalParamsViewModel _instance;
    private GlobalParameters _current;
    private GlobalParameters _loaded;

    public static GuiGlobalParamsViewModel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GuiGlobalParamsViewModel();
                _instance.Load();
            }
            return _instance;
        }
    }

    #region Properties

    public RunParameters DefaultRunParameters
    {
        get => _current.RunParameters;
        set
        {
            if (_current.RunParameters.Equals(value))
                return;

            _current.RunParameters = value;
            OnPropertyChanged(nameof(DefaultRunParameters));
        }
    }

    public bool AskAboutSettingsChangesOnClose
    {
        get => _current.AskAboutSettingsChangeOnClose;
        set
        {
            if (_current.AskAboutSettingsChangeOnClose == value)
                return;
            _current.AskAboutSettingsChangeOnClose = value;
            OnPropertyChanged(nameof(AskAboutSettingsChangesOnClose));
        }
    }

    public bool OverwriteSettingsWithoutAsking
    {
        get => _current.OverwriteSettingsWithoutAsking;
        set
        {
            if (_current.OverwriteSettingsWithoutAsking == value)
                return;
            _current.OverwriteSettingsWithoutAsking = value;
            OnPropertyChanged(nameof(OverwriteSettingsWithoutAsking));
        }
    }



    #endregion

    private GuiGlobalParamsViewModel() { }

    public string MainWindowTitle => $"Protease Guru: {GlobalVariables.ProteaseGuruVersion}";

    #region IO

    // Load from disk
    public void Load()
    {
        if (SettingsFileExists())
        {
            try
            {
                _current = GlobalParameters.FromToml(DefaultGlobalParametersFilePath);
            }
            catch
            {
                _current = new GlobalParameters();
                GlobalParameters.ToToml(_current, DefaultGlobalParametersFilePath);
            }
        }
        else
        {
            _current = new GlobalParameters();
            GlobalParameters.ToToml(_current, DefaultGlobalParametersFilePath);
        }

        _loaded = _current.Clone();
    }

    // Save to disk
    public void Save()
    {
        GlobalParameters.ToToml(_current, DefaultGlobalParametersFilePath);
        _loaded = _current.Clone();
    }
    
    public static bool SettingsFileExists() => File.Exists(DefaultGlobalParametersFilePath);
    
    public bool IsDirty() => !_current.Equals(_loaded);

    /// <summary>
    /// Call this method when parameters are changed through the DigestionConditionsSetupViewModel
    /// to ensure the dirty flag is properly updated.
    /// </summary>
    public void NotifyParametersChanged()
    {
        // Force the dirty check by updating the loaded state's last-known hash
        // This is needed because direct modifications to the RunParameters object
        // don't trigger property change notifications in this ViewModel
        OnPropertyChanged(nameof(DefaultRunParameters));
    }

    /// <summary>
    /// Loads parameters from a TOML file and updates the current state.
    /// This maintains the same RunParameters reference so existing ViewModels stay in sync.
    /// </summary>
    /// <param name="filePath">Path to the TOML file</param>
    public void LoadParametersFromFile(string filePath)
    {
        var loadedParams = RunParameters.FromToml(filePath);
        
        // Copy values into the existing RunParameters object to maintain reference
        _current.RunParameters.TreatModifiedPeptidesAsDifferent = loadedParams.TreatModifiedPeptidesAsDifferent;
        _current.RunParameters.MinPeptideMassAllowed = loadedParams.MinPeptideMassAllowed;
        _current.RunParameters.MaxPeptideMassAllowed = loadedParams.MaxPeptideMassAllowed;
        _current.RunParameters.OutputFolder = loadedParams.OutputFolder;
        
        // Replace the protease-specific parameters list
        _current.RunParameters.ProteaseSpecificParameters.Clear();
        foreach (var param in loadedParams.ProteaseSpecificParameters)
        {
            _current.RunParameters.ProteaseSpecificParameters.Add(param);
        }
        
        OnPropertyChanged(nameof(DefaultRunParameters));
    }

    #endregion
}

