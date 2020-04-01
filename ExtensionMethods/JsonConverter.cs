using Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace E37SalesApi.ExtensionMethods
{
    public static class JsonConverter
    {
        public static T GetObjectFronJson<T>(this string jsonString)
        {
            T tObject;

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(T));
                tObject = (T)deserializer.ReadObject(ms);
            }

            return tObject;
        }
        //public static SaleVM GetSaleVMFronJson(this string jsonString)
        //{
        //    SaleVM tObject = new SaleVM();
        //    List<string> jsonStringArr = jsonString.Split(',').ToList();

        //    tObject.reference = GetValueFromJsonStringArray("reference", jsonStringArr);
        //    tObject.dateCreated = DateTime.Parse(GetDateTimeFromJsonStringArray("dateCreated", jsonStringArr));
        //    tObject.dateSold = DateTime.Parse(GetDateTimeFromJsonStringArray("dateSold", jsonStringArr));
        //    tObject.statusId = (Status)int.Parse(GetValueFromJsonStringArray("statusId", jsonStringArr));
        //    tObject.customer = GetObjectFromJsonStringArray("customer", jsonStringArr);
        //    tObject.articleRows = GetListObjectFromJsonStringArray("articleRows", jsonStringArr);
        //    //using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
        //    //{
        //    //    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(T));
        //    //    tObject = (T)deserializer.ReadObject(ms);
        //    //}

        //    return tObject;
        //}

        //private static string GetDateTimeFromJsonStringArray(string prop, List<string> jsonStringArr)
        //{
        //    string propVal = jsonStringArr.Find(s => s.Contains(prop));
        //    List<string> propValArr = propVal.Split(':').ToList();
        //    string propValSecond = propValArr[1];
        //    string retVal = string.Empty;

        //    if (!propValSecond.Contains(','))
        //    {
        //        retVal = propValSecond.Trim('\"').Trim('}');
        //    }

        //    return retVal.Substring(0, 10);
        //}

        //private static List<ArticleRow> GetListObjectFromJsonStringArray(string prop, List<string> jsonStringArr)
        //{
        //    List<ArticleRow> retVal = new List<ArticleRow>();

        //    return retVal;
        //}

        //private static SelectedCustomer GetObjectFromJsonStringArray(string prop, List<string> jsonStringArr)
        //{
        //    string propVal = jsonStringArr.Find(s => s.Contains(prop));
        //    List<string> propValArr = propVal.Split(':').ToList();
        //    string propValSecond = propValArr[1];
        //    SelectedCustomer retVal = new SelectedCustomer();

        //    if (propValSecond.Contains(','))
        //    {
        //        List<string> jsonStringArrChild = propValSecond.Split(',').ToList();

        //        retVal.name = GetValueFromJsonStringArray("name", jsonStringArrChild);
        //        retVal.customerNumber = GetValueFromJsonStringArray("customerNumber", jsonStringArrChild);
        //    }

        //    return retVal;
        //}

        //private static string GetValueFromJsonStringArray(string prop, List<string> jsonStringArr)
        //{
        //    string propVal = jsonStringArr.Find(s => s.Contains(prop));
        //    List<string> propValArr = propVal.Split(':').ToList();
        //    string propValSecond = propValArr[1];
        //    string retVal = string.Empty;

        //    if(!propValSecond.Contains(','))
        //    {
        //        retVal = propValSecond.Trim('\"').Trim('}');
        //    }

        //    return retVal;
        //}
    }
}
