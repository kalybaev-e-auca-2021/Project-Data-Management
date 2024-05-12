using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Application.Common
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public AssemblyMappingProfile(Assembly assembly) {
            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                        ?? type.GetInterfaces().FirstOrDefault(i =>
                                            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>))?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
