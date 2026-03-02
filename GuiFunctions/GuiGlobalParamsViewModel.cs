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

    #endregion
}

