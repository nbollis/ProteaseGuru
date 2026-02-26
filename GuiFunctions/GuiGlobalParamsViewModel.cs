using Engine;
using Nett;
using Tasks;

namespace GuiFunctions;
public class GuiGlobalParamsViewModel : BaseViewModel
{
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
    private GuiGlobalParamsViewModel() { }

    public string MainWindowTitle => IsRnaMode
        ? $"Rnase Guru: {GlobalVariables.ProteaseGuruVersion}"
        : $"Protease Guru: {GlobalVariables.ProteaseGuruVersion}";

    #region Protease / Rnase Mode

    public static EventHandler<ModeSwitchRequestEventArgs>? RequestModeSwitchConfirmation { get; set; }
    public bool AskAboutModeSwitch
    {
        get => _current.AskAboutModeSwitch;
        set { _current.AskAboutModeSwitch = value; OnPropertyChanged(nameof(AskAboutModeSwitch)); }
    }

    public bool IsRnaMode
    {
        get => _current.IsRnaMode;
        set
        {
            // Invoke the event to check if the user wants to switch modes
            var args = new ModeSwitchRequestEventArgs();

            // Sent from Test or other sources without UI initialization. 
            if (RequestModeSwitchConfirmation is null)
                args.Result = ModeSwitchResult.SwitchKeepFiles;
            // Ask the GUI how to move forward
            // - If we have a default saved and are told not to ask, it will skip the pop-up
            // - if no files are loaded it will tell us to switch, otherwise it will trigger a pop-up
            else
                RequestModeSwitchConfirmation?.Invoke(this, args);

            if (args.RememberMyDecision)
            {
                AskAboutModeSwitch = false;
                CachedModeSwitchResult = args.Result;
            }

            // Do not switch - Force UI to refresh to original state
            if (args.Result == ModeSwitchResult.Cancel)
            {
                OnPropertyChanged(nameof(IsRnaMode));
                return;
            }

            _current.IsRnaMode = value;

            OnPropertyChanged(nameof(IsRnaMode));
            OnPropertyChanged(nameof(MainWindowTitle));
        }
    }

    public ModeSwitchResult CachedModeSwitchResult
    {
        get => _current.CachedModeSwitchResult;
        set { _current.CachedModeSwitchResult = value; OnPropertyChanged(nameof(CachedModeSwitchResult)); }
    }

    #endregion

    #region IO

    // Load from disk
    public void Load()
    {
        if (File.Exists(GlobalParameters.DefaultGlobalParametersFilePath))
        {
            try
            {
                _current = Toml.ReadFile<GlobalParameters>(GlobalParameters.DefaultGlobalParametersFilePath);
            }
            catch
            {
                _current = new GlobalParameters();
                GlobalParameters.ToToml(_current, GlobalParameters.DefaultGlobalParametersFilePath);
            }
        }
        else
        {
            _current = new GlobalParameters();
            GlobalParameters.ToToml(_current, GlobalParameters.DefaultGlobalParametersFilePath);
        }

        IsRnaMode = _current.IsRnaMode;
        _loaded = _current.Clone();
    }

    // Save to disk
    public void Save()
    {
        GlobalParameters.ToToml(_current, GlobalParameters.DefaultGlobalParametersFilePath);
        _loaded = _current.Clone();
    }

    public bool IsDirty() => !_current.Equals(_loaded);

    public static bool SettingsFileExists() => File.Exists(GlobalParameters.DefaultGlobalParametersFilePath);

    #endregion
}

