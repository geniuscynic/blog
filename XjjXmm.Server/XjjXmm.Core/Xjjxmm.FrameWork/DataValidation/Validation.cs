using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XjjXmm.FrameWork.DataValidation
{
    public class XjjxmmValidator
    {
        private readonly object _model;

        public XjjxmmValidator(object model)
        {
            _model = model;
        }

        public List<ValidatonResult> ValidationResults { get; } = new List<ValidatonResult>();

        public ValidatonResult FirstValidationResult => ValidationResults.FirstOrDefault();

        /// <summary>
        /// 验证class
        /// </summary>
        /// <param name="needFullCheck"></param>
        /// <param name="validateType"></param>
        /// <returns></returns>
        private bool ValidateModel(bool needFullCheck = false, ValidateType validateType = ValidateType.ManualValdate)
        {
            var type = _model.GetType();

            var validators = GetValidators(type.GetCustomAttributes());
                                             
          

            if (!validators.Any())
            {
                return true;
            }



            var val = _model;

            foreach (var validator in validators)
            {
                if (validator.ValdateType == ValidateType.ManualValdate && validateType == ValidateType.AutoValdate)
                {
                    continue;

                }

                var result = validator.IsValid(val, _model);

                if (!result)
                {
                    ValidationResults.Add(new ValidatonResult()
                    {
                        CustomMessage = validator.CustomMessage,
                        Description = validator.DisplayName,
                        Field = "",
                        FieldValue = val,
                        DefaultMessage = validator.DefaultMessage
                    });

                    if (!needFullCheck)
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        /// <summary>
        /// 验证property
        /// </summary>
        /// <param name="needFullCheck">false:一个未通过就返回，true:全部验证完在返回</param>
        /// <param name="validateType"></param>
        /// <returns></returns>
        public bool Validate(bool needFullCheck = false, ValidateType validateType = ValidateType.ManualValdate)
        {
            if (_model == null)
            {
                return false;
            }

            if(!ValidateModel(needFullCheck, validateType) && needFullCheck == false)
            {
                return false;
            }

            var type = _model.GetType();

           

            foreach (var propertyInfo in type.GetProperties())
            {
                var validators = GetValidators(propertyInfo.GetCustomAttributes());

                if (!validators.Any())
                {
                    return true;
                }

                var val = propertyInfo.GetValue(_model);

                foreach (var validator in validators)
                {
                    if (validator.ValdateType == ValidateType.ManualValdate && validateType == ValidateType.AutoValdate)
                    {
                        continue;

                    }

                    var result = validator.IsValid(val, _model);

                    if (!result)
                    {
                        ValidationResults.Add(new ValidatonResult()
                        {
                            CustomMessage = validator.CustomMessage,
                            Description = validator.DisplayName,
                            Field = propertyInfo.Name,
                            FieldValue = val,
                            DefaultMessage = validator.DefaultMessage
                        });

                        if (!needFullCheck)
                        {
                            return false;
                        }
                    }
                }


            }

            return ValidationResults.Count == 0;
        }


        private IEnumerable<AbstractValidator> GetValidators(IEnumerable<Attribute> attributes)
        {
            //var attributes = type.GetCustomAttributes();

            foreach (var attribute in attributes)
            {
                var baseName = typeof(AbstractValidator).FullName;
                var baseName2 = typeof(Attribute).FullName;
                var t = attribute.GetType();
                //var isInherit = false;
                while (t.FullName != baseName && t.FullName != baseName2)
                {
                    t = t.BaseType;

                    // isInherit = true;
                    // break;
                }
                // attribute.GetType().FullName;
               // if (attribute.GetType().IsAssignableFrom(typeof(AbstractValidator)))
                //{
                if (t.FullName == baseName)
                {
                    var validator = (AbstractValidator) attribute;
                    yield return validator;
                }
                // }
               
            }
        }
        public void AddCustomMessage(string message)
        {
            ValidationResults.Add(new ValidatonResult()
            {
                CustomMessage = message
            });
        }
    }
}
