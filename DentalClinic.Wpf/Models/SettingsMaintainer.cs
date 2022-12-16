namespace DentalClinic.Wpf
{
    using DentalClinic.Windows;
    using System;
    using System.IO;
    using System.Text;
    using System.Windows;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class SettingsMaintainer
    {
        protected readonly static string secret = "DentalClinic_2019";

        private static string SettingsFile
        {
            get { return App.container.Resolve<Container>().ConfigFile; }
        }

        public static AppSettings AppSettings
        {
            get { return App.container.Resolve<Container>().AppSettings; }
            set { App.container.Resolve<Container>().AppSettings = value; }
        }

        private static GuiSettings GuiSettings
        {
            get { return App.container.Resolve<Container>().GuiSettings; }
            set { App.container.Resolve<Container>().GuiSettings = value; }
        }

        private static string GuiSettingsFile { get { return App.container.Resolve<Container>().GuiSettingFile; } }

        public static void LoadSettings()
        {
            if (new DirectoryInfo(App.container.Resolve<Container>().XmlFileFolder).Exists)
            {
                if (new FileInfo(SettingsFile).Exists)
                {
                    AppSettings = XmlHelper.FromXmlFile<AppSettings>(SettingsFile);
                    if (new FileInfo(GuiSettingsFile).Exists)
                        GuiSettings = XmlHelper.FromXmlFile<GuiSettings>(GuiSettingsFile);
                }
                else
                {
                    SaveSettings();
                    LoadSettings();
                }
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(App.container.Resolve<Container>().XmlFileFolder);
                SaveSettings();
                LoadSettings();
            }
            

            App.container.Resolve<ISmsNotify>().LoadFromXml();

            if (string.IsNullOrWhiteSpace(AppSettings.DataBaseIP))
            {
                AppSettings.DataBaseIP = "localhost";
            }
            if (string.IsNullOrWhiteSpace(AppSettings.DataBaseName))
            {
                AppSettings.DataBaseName = "DentalClinic";
            }
            if (string.IsNullOrWhiteSpace(AppSettings.DataBaseUser))
            {
                AppSettings.DataBaseUser = "dental";
            }
            if (string.IsNullOrWhiteSpace(AppSettings.SMSServicePhoneIP))
            {
                AppSettings.SMSServicePhoneIP = "192.168.1.14";
            }

        }

        public static void SaveSettings()
        {
            //Crypto.ByteArrayToFile(
            //    App.container.Resolve<Container>().ConfigFile, Encoding.ASCII.GetBytes(Crypto.EncryptStringAES(XmlHelper.ToXml(App.container.Resolve<Container>().AppSettings), secret)));
           
            XmlHelper.ToXmlFile(AppSettings, SettingsFile);

            GuiSettings.AppWindowHeight = App.container.Resolve<MainWindow>().Height;

            GuiSettings.AppWindowWidth = App.container.Resolve<MainWindow>().Width;

            GuiSettings.AppWindowState = App.container.Resolve<MainWindow>().WindowState;

            XmlHelper.ToXmlFile(GuiSettings, GuiSettingsFile);

            App.container.Resolve<ISmsNotify>().SaveToXml();
        }

        public event EventHandler SettingsFileNotExists;

        public event EventHandler WrongDataBaseIP;

        public event EventHandler WrongDataBaseName;

        protected virtual void OnSettingsFileNotExists(EventArgs e)
        {
            if (!(SettingsFileNotExists is null))
            {
                SettingsFileNotExists(this, e);
            }
        }

        protected virtual void OnWrongDataBaseIP(EventArgs e)
        {
            if (!(WrongDataBaseIP is null))
            {
                WrongDataBaseIP(this, e);
            }
        }

        protected virtual void OnWrongDataBaseName(EventArgs e)
        {
            if (!(WrongDataBaseName is null))
            {
                WrongDataBaseName(this, e);
            }
        }
    }
}
