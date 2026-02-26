using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Engine;
using GuiFunctions;
using NUnit.Framework;
using Tasks;

namespace Test;
internal class Parameters
{
    private static string SettingsPath => GlobalParameters.DefaultGlobalParametersFilePath;

    [SetUp]
    public void SetUp()
    {
        // Ensure singleton reset before each test
        ResetSingleton();

        // Clear any event handlers from previous tests
        ClearEventHandlers();
    }

    [TearDown]
    public void TearDown()
    {
        // cleanup created settings file to avoid cross-test interference
        try
        {
            if (File.Exists(SettingsPath))
            {
                File.Delete(SettingsPath);
            }
        }
        catch { }

        ResetSingleton();
        ClearEventHandlers();
    }

    private static void ResetSingleton()
    {
        var vmType = typeof(GuiGlobalParamsViewModel);
        var instField = vmType.GetField("_instance", BindingFlags.NonPublic | BindingFlags.Static);
        instField.SetValue(null, null);
    }

    private static void ClearEventHandlers()
    {
        // Clear static event handlers to prevent cross-test contamination
        var requestModeSwitchField = typeof(GuiGlobalParamsViewModel)
            .GetProperty("RequestModeSwitchConfirmation", BindingFlags.Public | BindingFlags.Static);
        requestModeSwitchField.SetValue(null, null);
    }
    [Test]
    public void BooleanProperties_SetAndGet_TriggerPropertyChanged()
    {
        var vm = GuiGlobalParamsViewModel.Instance;
        var vmType = typeof(GuiGlobalParamsViewModel);
        var props = vmType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        int testedCount = 0;

        foreach (var prop in props)
        {
            // skip non-bool properties, non-read/write, and indexers
            if (prop.PropertyType != typeof(bool)) continue;
            if (!prop.CanRead || !prop.CanWrite) continue;
            if (prop.GetIndexParameters().Length > 0) continue;
            // Skip IsRnaMode as it has special behavior tested separately
            if (prop.Name == nameof(GuiGlobalParamsViewModel.IsRnaMode)) continue;

            testedCount++;

            bool eventFired = false;
            PropertyChangedEventHandler handler = (s, e) =>
            {
                if (e.PropertyName == prop.Name) eventFired = true;
            };

            vm.PropertyChanged += handler;
            try
            {
                // read original value
                bool before = (bool)prop.GetValue(vm);
                // set to opposite
                prop.SetValue(vm, !before);
                // assert value changed
                var afterObj = prop.GetValue(vm);
                Assert.That(afterObj, Is.EqualTo((object)!before), $"Property {prop.Name} did not toggle as expected.");
                // assert event fired
                Assert.That(eventFired, Is.True, $"Changing {prop.Name} should raise PropertyChanged for that property.");
                // restore original to avoid side-effects
                prop.SetValue(vm, before);
            }
            finally
            {
                vm.PropertyChanged -= handler;
            }
        }

        Assert.That(testedCount, Is.GreaterThan(0), "No boolean properties were found to test on GuiGlobalParamsViewModel.");
    }

}
