using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Application.Shared.Helpers
{
    [ExcludeFromCodeCoverage]
    public class ResourceHelper
    {
        public static string Get(string query)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Shared.Queries.{query}.sql"))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Query not found: {query}");
                }

                using (var reader = new StreamReader(stream))
                {
                    int charCode;

                    StringBuilder sb = new();

                    while ((charCode = reader.Read()) != -1)
                    {
                        char charactere = (char)charCode;

                        if (sb.Length >= int.MaxValue)
                        {
                            throw new FileLoadException("Too long input");
                        }

                        sb.Append(charactere);
                    }

                    return sb.ToString();
                }
            }
        }
    }
}

