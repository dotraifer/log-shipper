using LogShipperProject.pipeline.pipelines;
using System;
using System.IO;
using System.Text.RegularExpressions;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NodeDeserializers;

public class EnvironmentVariableNodeDeserializer : INodeDeserializer
{
    private readonly INodeDeserializer _innerDeserializer;

    public EnvironmentVariableNodeDeserializer(INodeDeserializer innerDeserializer)
    {
        _innerDeserializer = innerDeserializer ?? throw new ArgumentNullException(nameof(innerDeserializer));
    }

    /// <summary>
    /// Deserilize env varibles in the yaml
    /// </summary>
    /// <param name="parser"></param>
    /// <param name="expectedType"></param>
    /// <param name="nestedObjectDeserializer"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
    {
        if (_innerDeserializer.Deserialize(parser, expectedType, nestedObjectDeserializer, out value))
        {
            if (value is string stringValue)
            {
                MatchCollection matches = Regex.Matches(stringValue, @"\${\b\w+\b}");
                foreach (Match match in matches)
                {
                    string envVarible = match.Value.Substring(2, (match.Value.Length) - 3);
                    Console.WriteLine(Environment.GetEnvironmentVariable(envVarible));
                    Console.WriteLine(envVarible);
                    value = match.Value.Replace(envVarible, Environment.GetEnvironmentVariable(envVarible) ?? "NO_ENV_FOUND");
                }
            }
            return true;
        }

        return false;
    }
}