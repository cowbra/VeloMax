using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdd
{
    public static class ApplicationState
    {
        private static Dictionary<string, object> _values =
                   new Dictionary<string, object>();
        public static void SetValue(string key, object value)
        {
            if (_values.ContainsKey(key))
            {
                _values.Remove(key);
            }
            _values.Add(key, value);
        }
        public static T GetValue<T>(string key)
        {
            if (_values.ContainsKey(key))
            {
                return (T)_values[key];
            }
            else
            {
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
                return default(T);
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
            }
        }
    }
}
