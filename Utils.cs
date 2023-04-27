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
        public static void YamlParser(string YamlFilePath)
        {

            // Deserialize the YAML file into a dynamic object
            var deserializer = new DeserializerBuilder().Build();
            var yamlString = File.ReadAllText(YamlFilePath);
            var dynamicObject = deserializer.Deserialize<ExpandoObject>(yamlString);
            foreach (var property in dynamicObject)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;

                // Do something with the property and its value
            }
        }
    }
}
