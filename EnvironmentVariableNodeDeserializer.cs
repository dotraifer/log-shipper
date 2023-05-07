﻿using log_shipper.pipeline.pipelines;
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

    public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
    {
        if (_innerDeserializer.Deserialize(parser, expectedType, nestedObjectDeserializer, out value))
        {
            if (value is string stringValue)
            {
                MatchCollection match = Regex.Matches(stringValue, @"\${\b\w+\b}");
                foreach (Match match2 in match)
                {
                    string env_var = match2.Value.Substring(2, (match2.Value.Length) - 3);
                    Console.WriteLine(Environment.GetEnvironmentVariable(env_var));
                    Console.WriteLine(env_var);
                    value = match2.Value.Replace(env_var, Environment.GetEnvironmentVariable(env_var) ?? "NO_ENV_FOUND");
                }
            }
            return true;
        }

        return false;
    }
}