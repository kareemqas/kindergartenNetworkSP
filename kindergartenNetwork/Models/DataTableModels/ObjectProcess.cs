using System;

namespace kindergartenNetwork.Models.DataTableModels
{
    public static class ObjectProcess
    {
        public static string DataTableParamDegeriGetir(Object oObject, string tcPropName)
        {
            var oObjectClassType = typeof(JQueryDataTableParamModel);
            //string xx = oObjectClassType.GetProperty(tcPropName).GetValue(oObject).ToString();;
            return oObjectClassType.GetProperty(tcPropName).GetValue(oObject).ToString();
        }

        public static Object ObjectDegeriGetir(Object oObject, string tcPropName)
        {
            var oObjectClassType = typeof(JQueryDataTableParamModel);

            return oObjectClassType.GetProperty(tcPropName).GetValue(oObject);
        }

        public static string SerializeObject<T>(this T toSerialize)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(toSerialize.GetType());
            System.IO.StringWriter textWriter = new System.IO.StringWriter();

            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
        }

        public static Object Deserialize(String stringObject)
        {
            Object returnObject;
            byte[] bytes = Convert.FromBase64String(stringObject);

            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytes);

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            returnObject = bformatter.Deserialize(stream);

            return returnObject;
        }

    }
}