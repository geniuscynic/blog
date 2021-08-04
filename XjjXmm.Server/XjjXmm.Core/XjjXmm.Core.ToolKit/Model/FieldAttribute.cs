using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DoCare.Zkzx.Core.FrameWork.Tool.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : Attribute
    {
        public bool IsRequired = false;
        public string Description { get; set; } = "";

        public string DictCode { get; set; } = "001.aa";
        public string Type { get; set; } = "checkbox";
    }

    public class FieldSetting
    {
        public string IsDisplay = "1";

        public string Path { get; set; }

        public string Default { get; set; }

        
        public string[] Fields => Path.Split('.');
    }

    public class FieldHelper

    {
        private static void AddChiled(object model, Dictionary<string, dynamic> result)
        {
            if (model == null)
            {
                return;
            }

            var type = model.GetType();
            


            foreach (var propertyInfo in type.GetProperties())
            {
                var field = propertyInfo.GetCustomAttribute<FieldAttribute>();
                if (field != null)
                {
                    result[propertyInfo.Name+"_displayname"] = field.Description;
                    result[propertyInfo.Name + "_dictcode"] = field.DictCode;
                    result[propertyInfo.Name + "_controltype"] = field.Type;
                }
                else
                {
                    var values = propertyInfo.GetValue(model);
                    type = values?.GetType();
                    if (values== null || type == typeof(string) || type.IsPrimitive || type == typeof(DateTime) || values is IEnumerable)
                    {
                        result[propertyInfo.Name] = values;
                    }
                    else
                    {
                        var temp = new Dictionary<string, dynamic>();
                        AddChiled(values, temp);
                        result[propertyInfo.Name] = temp;
                    }

                   
                }
            }
        }

        public static object AddModel<T>(T model, List<FieldSetting> settings)
        {
            if (model == null)
            {
                return null;
            }



            var result = new Dictionary<string, dynamic>();

            AddChiled(model, result);

            settings.ForEach(t =>
            {
                Dictionary<string, dynamic> temp = result;
               
                for (var i = 0; i < t.Fields.Length; i++)
                {
                    var key = t.Fields[i];

                    if (i == t.Fields.Length - 1)
                    {
                        var a = "";
                        temp[key + "_isdisplay"] = t.IsDisplay;
                       // temp[key + "_dictcode"] = t.DictCode;


                        if (t.Default == "now")
                        {
                            //if (temp[key] == null || string.IsNullOrEmpty(temp[key]))
                           // {
                           temp[key] = DateTime.Now.ToString("yyy-MM-dd");
                          
                            // }
                        }
                    }
                    else if (temp.ContainsKey(key))
                    {
                        temp = temp[key] as Dictionary<string, dynamic>;
                        // temp = (Dictionary<string, dynamic>)temp[key];
                    }
                    else
                    {
                        break;
                    }
                }

               


            });

            return result;
        }
    }
}
