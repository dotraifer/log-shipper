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

namespace log_shipper
{
    class Utils
    {
        /// <summary>
        /// parsing a yaml file to a dynamic object
        /// </summary>
        /// <param name="YamlFilePath">path to the yaml configuration file</param>
        /// <returns>the dynamic object - ExpandoObject</returns>
        public static ExpandoObject YamlParser(string YamlFilePath)
        {

            // Deserialize the YAML file into a dynamic object
            var deserializer = new DeserializerBuilder().Build();
            try
            {
                var yamlString = File.ReadAllText(YamlFilePath);
                var dynamicObject = deserializer.Deserialize<ExpandoObject>(yamlString);
                return dynamicObject;
            }
            catch (FileNotFoundException ex) 
            {
                Log.Error("configuration file not fount in {0} ", YamlFilePath);
                throw new FileNotFoundException("configuration file not fount in {0} ", YamlFilePath);
            }

        }
    }
}
