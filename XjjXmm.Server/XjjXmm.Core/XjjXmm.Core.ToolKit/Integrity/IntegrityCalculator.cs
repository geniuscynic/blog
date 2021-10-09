using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.ToolKit;

namespace DoCare.Zkzx.Core.FrameWork.Tool.Integrity
{
    public class IntegrityCalculator
    {
        private readonly List<IntegrityExtraData> integrityExtraDatas = new List<IntegrityExtraData>();
        //private List<ExtraProcessorAttribute> processors = new List<ExtraProcessorAttribute>();

        private void AddModel(object model)
        {
            if (model == null)
            {
                return;
            }

            var type = model.GetType();



            foreach (var propertyInfo in type.GetProperties())
            {

                var propertyVal = propertyInfo.GetValue(model);
                if (propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.IsArray && !(propertyVal is ICollection) && !TypeKit.IsSimpleType(propertyInfo.PropertyType))
                {
                    AddModel(propertyVal);
                    continue;
                }


                var integrityAttribute = propertyInfo.GetCustomAttribute<IntegrityAttribute>();
                if (integrityAttribute == null)
                {
                    continue;
                }

                //var propertyName = propertyInfo.Name;
                //if (!string.IsNullOrWhiteSpace(moduleName))
                //{
                //    propertyName = $"{moduleName}.{propertyName}";
                //}



                //if (timeNodeAttribute.IsTimelineModel)
                //{
                //    AddModel(propertyInfo.GetValue(model), propertyName);
                //    continue;

                //}

                //var code = integrityAttribute.Code;
                //var desc = integrityAttribute.Description;
                //var data = propertyInfo.GetValue(model);
                //var isEnabled = integrityAttribute.IsEnable;
                //var weight = integrityAttribute.Weight;
                if (string.IsNullOrEmpty(integrityAttribute.Alias))
                {
                    integrityAttribute.Alias = propertyInfo.Name;
                }
                var integrityExtraData = new IntegrityExtraData
                {
                    Name = integrityAttribute.Alias,
                    Description = integrityAttribute.Description,
                    Data = propertyInfo.GetValue(model),
                    IsEnable = integrityAttribute.IsEnable,
                    Weight = integrityAttribute.Weight,
                    //IsExclude = false,
                    PropertyInfo = propertyInfo
                };

                integrityExtraDatas.Add(integrityExtraData);



                var extraProcessor = propertyInfo.GetCustomAttributes<ExtraProcessorAttribute>();
                foreach (var extraProcessorAttribute in extraProcessor)
                {
                    extraProcessorAttribute.CurrentIntegrityExtraData = integrityExtraData;
                    extraProcessorAttribute.IntegrityExtraDatas = integrityExtraDatas;
                }

                integrityExtraData.Processors.AddRange(extraProcessor);
            }


        }

        public IntegrityEntity Calculate(object model)
        {
            AddModel(model);

            int total = 0, failed = 0;

            IntegrityEntity integrityEntity = new IntegrityEntity();

            foreach (var integrityExtraData in integrityExtraDatas)
            {
                if (!integrityExtraData.IsEnable)
                {
                    continue;
                }

                if (integrityExtraData.Processors.Any())
                {
                    var processors = integrityExtraData.Processors.Where(t => t.IsExclude() == false).ToList();

                    if (processors.Any())
                    {
                        integrityEntity.IntegrityExtraDatas.Add(integrityExtraData);
                        total += integrityExtraData.Weight;
                    }

                    foreach (var processor in processors)
                    {
                        if (processor.IsNullOrEmpty())
                        {
                            failed += integrityExtraData.Weight;
                            break;
                        }
                    }


                }
                else
                {
                    integrityEntity.IntegrityExtraDatas.Add(integrityExtraData);
                    total += integrityExtraData.Weight;
                    if (ObjectKit.IsNullOrEmpty(integrityExtraData.Data, integrityExtraData.PropertyInfo))
                    {
                        failed += integrityExtraData.Weight;
                    }
                }
            }

            integrityEntity.Total = total;
            integrityEntity.Failed = failed;

            return integrityEntity;
        }
    }


}
