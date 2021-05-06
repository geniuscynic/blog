using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DoCare.Zkzx.Core.Database.DataValidation
{
    public class DoCareValidator
    {
        private readonly object _model;

        public DoCareValidator(object model)
        {
            _model = model;
        }

        public List<ValidatonResult> ValidationResults { get; } = new List<ValidatonResult>();

        public ValidatonResult FirstValidationResult => ValidationResults.FirstOrDefault();

        public bool Validate(bool needFullCheck = false)
        {
            if (_model == null)
            {
                return false;
            }

            var type = _model.GetType();

            foreach (var propertyInfo in type.GetProperties())
            {
                var validators = propertyInfo.GetCustomAttributes<AbstractValidator>();

                var val = propertyInfo.GetValue(_model);

                foreach (var validator in validators)
                {
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

        public void AddCustomMessage(string message)
        {
            ValidationResults.Add(new ValidatonResult()
            {
                CustomMessage = message
            });
        }
    }
}
