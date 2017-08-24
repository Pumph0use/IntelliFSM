using System.Collections.Generic;
using System;
using System.Linq;

namespace IntelligentMachine
{
    public static class Utilities
    {
        public static T[] BuildClassArrayFromInterface<T>() where T : class
        {
            var result = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(T)) && type.IsClass)
                        result.Add((T)Activator.CreateInstance(type));
                }
            }

            return result.ToArray();
        }

        public static IEnumerable<T> GetEnumValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static T CreateDelegate<T>(object origin, string methodRoot) where T : class
        {
            var mtd = origin.GetType().GetMethod(methodRoot, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public
                                          | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod);
            if (mtd != null)
            {
                return Delegate.CreateDelegate(typeof(T), origin, mtd) as T;
            }
            else
                return null;
        }

    }
}

