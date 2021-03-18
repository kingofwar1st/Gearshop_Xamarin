using System;
using System.Collections.Generic;
using System.Text;
using GearShop.ViewModel;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace GearShop
{
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

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
        public static string HoTen
        {
            get => AppSettings.GetValueOrDefault(nameof(HoTen), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(HoTen), value);
        }
        public static string Sdt
        {
            get => AppSettings.GetValueOrDefault(nameof(Sdt), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Sdt), value);
        }
        public static string Email
        {
            get => AppSettings.GetValueOrDefault(nameof(Email), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Email), value);
        }
        public static string Pass
        {
            get => AppSettings.GetValueOrDefault(nameof(Pass), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Pass), value);
        }
        public static void clearEverything()
        {
            AppSettings.Clear();
        }
    }
}

