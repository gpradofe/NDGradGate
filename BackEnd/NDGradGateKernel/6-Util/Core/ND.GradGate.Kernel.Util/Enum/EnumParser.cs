using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Util.Enum
{
    public static class EnumParser
    {
        #region Methods
        public static string GetName<T>(T value) where T : System.Enum
        {
            if (value == null) return null;
#pragma warning disable CS8603 // Possible null reference return.
            return System.Enum.GetName(typeof(T), value);
#pragma warning restore CS8603 // Possible null reference return.

        }
        public static T Parse<T>(string value) where T : struct, System.Enum
            => System.Enum.Parse<T>(value);

        public static T? ParseNullable<T>(string value) where T : struct, System.Enum
        {
            if (string.IsNullOrEmpty(value)) return null;
            return System.Enum.Parse<T>(value);
        }
        #endregion

    }
}
