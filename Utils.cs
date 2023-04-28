using System;
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
            var yamlString = File.ReadAllText(YamlFilePath);
            var dynamicObject = deserializer.Deserialize<ExpandoObject>(yamlString);
            return dynamicObject;
            /*
            foreach (var property in dynamicObject)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;
                Console.WriteLine(propertyName);
                Dictionary<object, object> dictionary = (Dictionary<object, object>)propertyValue; // replace with your own code to get the dictionary

                foreach (KeyValuePair<object, object> entry in dictionary)
                {
                    Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
                }

                // Do something with the property and its value
            }
            */
        }
    }
}
