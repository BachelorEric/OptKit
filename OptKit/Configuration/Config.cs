using OptKit.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OptKit.Configuration
{
    public class Config : IConfig
    {
        FileName configFile;

        public Config(FileName file, IConfigSection section)
        {
            Check.NotNull(file, nameof(file));
            Check.NotNull(section, nameof(section));
            configFile = file;
            Section = section;
        }

        public IConfigSection Section { get; set; }

        public bool Contains(string key)
        {
            return Section.Contains(key);
        }

        public T Get<T>(string key, T defaultValue)
        {
            return Section.Get(key, defaultValue);
        }

        public IReadOnlyList<T> GetList<T>(string key)
        {
            return Section.GetList<T>(key);
        }

        public IConfigSection GetSection(string key)
        {
            return Section.GetSection(key);
        }

        public void Remove(string key)
        {
            Section.Remove(key);
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { Section.PropertyChanged += value; }
            remove { Section.PropertyChanged -= value; }
        }

        public void Save()
        {
            Section.Save(configFile);
        }

        public void Set<T>(string key, T value)
        {
            Section.Set<T>(key, value);
        }

        public void SetList<T>(string key, IEnumerable<T> value)
        {
            Section.SetList<T>(key, value);
        }

        public void SetSection(string key, IConfigSection section)
        {
            Section.SetSection(key, section);
        }
    }
}
