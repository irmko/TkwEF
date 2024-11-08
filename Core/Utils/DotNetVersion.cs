using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Versioning;

namespace TkwEF.Core.Helper
{
    public static class DotNetVersion
    {
        /// <summary>
        /// Наличие версии 4.5 и более
        /// </summary>
        /// <returns></returns>
        public static bool Has45OrLaterInMashine()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Has45OrLaterInClient(Assembly assembly)
        {
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //var sys = assemblies.First(f => f.GetName().Name == "System");
            //return sys.GetName().Version.Major > 4;

            //var val = assembly.GetCustomAttributesData().First().NamedArguments.First().MemberInfo;

            try
            {
                var attributes = assembly.GetCustomAttributes(true);
                foreach (var attr in attributes)
                {
                    //var typeId = attr.TypeId;
                    Type type = attr.GetType();
                    if (type.FullName == "System.Runtime.Versioning.TargetFrameworkAttribute")
                    {
                        TargetFrameworkAttribute target = (TargetFrameworkAttribute)attr;
                        string val = target.FrameworkName.Substring(".NETFramework,Version=v".Length);
                        string[] arr = val.Split('.');
                        int major, minor;
                        major = int.Parse(arr[0]);
                        minor = int.Parse(arr[1]);
                        return (major == 4 && minor > 0) || major > 4;
                    }
                }
                throw new BLL.BIZ_Base.RdbsBllException(string.Format("Не удалось определить целевую платформу ( {0}, машина={1} )"
                    , assembly.FullName, Environment.MachineName));
            }
            catch (Exception ex)
            {
                throw new BLL.BIZ_Base.RdbsBllException(string.Format("Непредвиденная ошибка при определении целевой платформы ( {0}, машина={1} )"
                    , assembly.FullName, Environment.MachineName), ex);
            }
        }

        // Checking the version using >= will enable forward compatibility.
        private static string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 461808)
                return "4.7.2 or later";
            if (releaseKey >= 461308)
                return "4.7.1";
            if (releaseKey >= 460798)
                return "4.7";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
                return "4.6.1";
            if (releaseKey >= 393295)
                return "4.6";
            if (releaseKey >= 379893)
                return "4.5.2";
            if (releaseKey >= 378675)
                return "4.5.1";
            if (releaseKey >= 378389)
                return "4.5";
            // This code should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }
    }
}
