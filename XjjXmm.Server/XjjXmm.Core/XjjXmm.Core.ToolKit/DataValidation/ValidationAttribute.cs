using System;
using System.Collections;

namespace DoCare.Zkzx.Core.FrameWork.Tool.DataValidation
{
    public enum ValidateType
    {
        AutoValdate,
        CustomValdate,
    }
    public abstract class AbstractValidator : Attribute
    {
        /// <summary>
        /// 自定义错误
        /// </summary>
        public virtual string CustomMessage { get; set; }

        internal virtual string DefaultMessage { get; set; }

        public string DisplayName { get; set; } = "";

        public ValidateType ValdateType { get; set; } = ValidateType.AutoValdate;

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public abstract bool IsValid(object value, object model);
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : AbstractValidator
    {
        internal override string DefaultMessage { get; set; } = "必填";

        public override bool IsValid(object value, object model)
        {
            if (value == null) return false;

            if (value is ICollection array && array.Count == 0) return false;

            if (string.IsNullOrEmpty(value.ToString())) return false;

            return true;


        }
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class StringLengthAttribute : AbstractValidator
    {
        internal override string DefaultMessage { get; set; } = "长度不在指定范围";

        public int Min { get; set; } = 0;

        public int Max { get; set; } = int.MaxValue;

        public override bool IsValid(object value, object model)
        {
            if (value == null) return false;

            if (value is string)
            {
                var len = value.ToString().Length;

                if (len <= Min || len >= Max)
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("参数不是string类型");
            }

            return true;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NumberAttribute : AbstractValidator
    {
        internal override string DefaultMessage { get; set; } = "不是数字类型";

        public override bool IsValid(object value, object model)
        {
            if (value == null) return true;

            try
            {
                int.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class RangeAttribute : AbstractValidator
    {
        internal override string DefaultMessage { get; set; } = "不在指定范围";

        public int Min { get; set; } = 0;

        public int Max { get; set; } = int.MaxValue;

        public override bool IsValid(object value, object model)
        {
            if (value == null) return true;

            try
            {
                var result = Convert.ToDecimal(value);

                if (result < Min || result > Max)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }


    public class ValidatonResult
    {
        public string Field { get; set; }

        public string Description { get; set; }

        public object FieldValue { get; set; }

        internal string CustomMessage { get; set; }

        internal string DefaultMessage { get; set; }

        public string ErrorMessage
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CustomMessage))
                {
                    return CustomMessage;
                }

                var desc = Description;

                if (string.IsNullOrWhiteSpace(desc))
                {
                    desc = Field;
                }

                return $"{desc}{DefaultMessage}";
            }
        }
    }
}
