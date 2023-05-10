﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using System.IO;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Reflection;
using Serilog;
using YamlDotNet.Serialization.NodeDeserializers;

namespace LogShipperProject
{
    class Utils
    {
        /// <summary>
        /// parsing a yaml file to a dynamic object
        /// </summary>
        /// <param name="yamlFilePath">path to the yaml configuration file</param>
        /// <returns>the dynamic object - ExpandoObject</returns>
        public static ExpandoObject YamlParser(string yamlFilePath)
        {

            // Deserialize the YAML file into a dynamic object
            var deserializer = new DeserializerBuilder().WithNodeDeserializer(inner => new EnvironmentVariableNodeDeserializer(inner), s => s.InsteadOf<ScalarNodeDeserializer>()).Build();
            try
            {
                var yamlString = File.ReadAllText(yamlFilePath);
                var dynamicObject = deserializer.Deserialize<ExpandoObject>(yamlString);
                return dynamicObject;
            }
            catch (FileNotFoundException ex) 
            {
                Logger.Error("configuration file not fount in {0} ", yamlFilePath);
                throw;
            }

        }
    }
}
