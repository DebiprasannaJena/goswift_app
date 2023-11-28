using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace CommonDataExtensionHelper
{
    /// <summary>
    /// Reflection class for discover property of a Type
    /// </summary>
    /// <auther>Manas Bej</auther>
    public class Reflection
    {
        public void FillObjectWithProperty(ref object objectTo, string propertyName, object propertyValue)
        {
            Type tOb2 = objectTo.GetType();
            tOb2.GetProperty(propertyName).SetValue(objectTo, propertyValue, null);
        }
    }
    /// <summary>
    /// Common extention method 
    /// </summary>
    /// <auther>Manas Bej</auther>
    public static class CommonExtention
    {
        /// <summary>
        /// Extention method : for mapping the DataReader to Generic type 
        /// </summary>
        /// <auther>Manas Bej</auther>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="dr">DataReader</param>
        /// <returns>IEnumerable<T></returns>
        public static IEnumerable<T> FromDataReader<T>(this IEnumerable<T> list, DbDataReader dr)
        {
            //Instance reflec object from Reflection class coded above
            Reflection reflec = new Reflection();
            //Declare one "instance" object of Object type and an object list
            Object instance;
            List<Object> lstObj = new List<Object>();

            //dataReader loop
            while (dr.Read())
            {
                //Create an instance of the object needed.
                //The instance is created by obtaining the object type T of the object
                //list, which is the object that calls the extension method
                //Type T is inferred and is instantiated
                instance = Activator.CreateInstance(list.GetType().GetGenericArguments()[0]);

                // Loop all the fields of each row of dataReader, and through the object
                // reflector (first step method) fill the object instance with the datareader values
                foreach (DataRow drow in dr.GetSchemaTable().Rows)
                {
                    reflec.FillObjectWithProperty(ref instance,
                            drow.ItemArray[0].ToString(), dr[drow.ItemArray[0].ToString()]);
                }

                //Add object instance to list
                lstObj.Add(instance);
            }

            List<T> lstResult = new List<T>();
            foreach (Object item in lstObj)
            {
                lstResult.Add((T)Convert.ChangeType(item, typeof(T)));
            }

            return lstResult;
        }
        /// <summary>
        /// Extention method : for mapping the DataReader to Generic type 
        /// </summary>
        /// <auther>Manas Bej</auther>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static List<T> DataReaderMapToList<T>(this IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {                    
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }                
                list.Add(obj);
            }
            return list;
        }
        /// <summary>
        /// Extention method : Overloading method for mapping the DataReader to Generic type 
        /// </summary>
        /// <auther>Manas Bej</auther>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="direction"></param>
        /// <exception cref="Exception">The Exception will be thown when Property name mis-match with DataReader</exception>
        /// <returns></returns>
        public static List<T> DataReaderMapToList<T>(this IDataReader dr,MappingDirection direction)
        {           
            List<T> list = new List<T>();
            string lastPropName = string.Empty;
            string errorFormat = string.Empty;
            T obj = default(T);
            try
            {
                obj = Activator.CreateInstance<T>();
                if (direction == MappingDirection.Auto)
                {                    
                    direction = dr.FieldCount > obj.GetType().GetProperties().Length ? MappingDirection.AllFromProperty : MappingDirection.AllFromDataReader;
                }
                if (direction == MappingDirection.AllFromDataReader)
                    errorFormat = @"Property name {0} not found in the class {1}, with mapping direction {2}";
                else
                    errorFormat = @"Column name {0} not found in the IDataReader for the class {1}, with mapping direction {2}";
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    if (direction == MappingDirection.AllFromProperty)
                    {
                        foreach (PropertyInfo prop in obj.GetType().GetProperties())
                        {
                            lastPropName = prop.Name;
                            if (!object.Equals(dr[prop.Name], DBNull.Value))
                            {
                                prop.SetValue(obj, dr[prop.Name], null);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            lastPropName = dr.GetName(i); 
                            if (!object.Equals(dr[i], DBNull.Value))
                                obj.GetType().GetProperty(dr.GetName(i)).SetValue(obj, dr[i], null);
                        }
                    }
                    list.Add(obj); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(errorFormat, lastPropName,obj.GetType(), direction.ToString()), ex);
            }
            return list;
        }

        public static string SerializeToXMLString<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
        }        
    }
    /// <summary>
    /// Enumeration for mapping type
    /// </summary>
    /// <auther>Manas Bej</auther>
    public enum MappingDirection
    {
        /// <summary>
        /// Automatically detect the mapping type
        /// </summary>
        Auto,
        /// <summary>
        /// This will map all the property from "Property Class"
        /// Exception : It will throw exception when property is not found in datareader
        /// </summary>
        AllFromProperty,
        /// <summary>
        /// This will map all the property from "DataReader"
        /// Exception : It will throw exception when DataColumn is not found in Property Class
        /// </summary>
        AllFromDataReader
    }
}