// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CatalogApp.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants

    private const string SettingsKey = "FirstStartApplication";
    private static readonly bool SettingFirstStartApplication = true;

    #endregion


    public static bool FirstStartApplication
    {
      get
      {
        return AppSettings.GetValueOrDefault<bool>(SettingsKey, SettingFirstStartApplication);
      }
      set
      {
        AppSettings.AddOrUpdateValue<bool>(SettingsKey, value);
      }
    }

  }
}