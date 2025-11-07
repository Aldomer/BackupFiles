using System.Collections.Generic;
using System.Xml;

namespace Copier
{
    public class Settings
    {
        public List<Watch> Watch = new List<Watch>(); //Used by Backup Files

        public XmlDocument xSettings = new XmlDocument();

        public void SettingsLoad()
        {
            for (int i = 0; i < xSettings.GetElementsByTagName("filelocation").Count; i++)
            {
                Watch.Add(new Watch() { Original = xSettings.GetElementsByTagName("Original")[i].InnerText, Backup = xSettings.GetElementsByTagName("Backup")[i].InnerText, BackupChanges = xSettings.GetElementsByTagName("BackupChanges")[i].InnerText });
            }
        }

        public void ResetAll()
        {
            ClearAll();

            SettingsLoad();
        }

        private void ClearAll()
        {
            Watch.Clear();
        }
    }
}
