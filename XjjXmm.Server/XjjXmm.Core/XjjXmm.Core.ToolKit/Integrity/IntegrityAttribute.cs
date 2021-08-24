using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using DoCare.Zkzx.Core.FrameWork.Tool.ToolKit;

namespace DoCare.Zkzx.Core.FrameWork.Tool.Integrity
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IntegrityAttribute : Attribute
    {
        public string Alias { get; set; }

        public string Description { get; set; }

        public bool IsEnable { get; set; } = true;

        public int Weight { get; set; } = 1;
    }

    public class IntegrityExtraData 
    {
        public string Name { get; internal set; }

        public string Description { get; internal set; }

        public object Data { get; internal set; }

        internal bool IsEnable { get;  set; }

        //internal bool IsExclude { get;  set; }

        internal int Weight { get; set; }

       

        internal PropertyInfo PropertyInfo { get; set; }

        internal List<IExtraProcessor> Processors { get; set; } = new List<IExtraProcessor>();
    }

    public class IntegrityEntity
    {
        public int Total { get; internal set; }

        private int _ok = 0;
        public int OK
        {
            get => _ok;

            internal set
            {
                _ok = value;
                _failed = Total - value;
            }
        }

        private int _failed = 0;

        public int Failed
        {
            get => _failed;

            internal set
            {
                _failed = value;
                _ok = Total - value;
            }
        }

        public decimal Rate
        {
            get
            {
                if (Total <= 0)
                {
                    return 100;
                }

                return decimal.Divide(OK, Total) * 100;
            }
        }

        public List<IntegrityExtraData> IntegrityExtraDatas { get; internal set; } = new List<IntegrityExtraData>();
    }

    public interface IExtraProcessor
    {
        /// <summary>
        /// 是否排除
        /// </summary>
        /// <param name="ic">可算完整度</param>
        /// <param name="member">成员</param>
        /// <param name="integrityExtraDatas"></param>
        /// <returns></returns>
        bool IsExclude();

        bool IsNullOrEmpty();
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ExtraProcessorAttribute : Attribute, IExtraProcessor
    {
        public IntegrityExtraData CurrentIntegrityExtraData { get; internal set; }

        public List<IntegrityExtraData> IntegrityExtraDatas { get; internal set; }

        public virtual bool IsExclude()
        {
            return false;
        }

        public virtual bool IsNullOrEmpty()
        {
            return ObjectKit.IsNullOrEmpty(CurrentIntegrityExtraData.Data, CurrentIntegrityExtraData.PropertyInfo);
        }
    }

    public class WhenValueExtraProcessorAttribute : ExtraProcessorAttribute
    {
        private readonly string _ifName;
        private readonly string _ifValue;


        public WhenValueExtraProcessorAttribute(string ifName, string ifValue)
        {
            _ifName = ifName;
            _ifValue = ifValue;
        }

        public override bool IsExclude()
        {
            var field = IntegrityExtraDatas.FirstOrDefault(t => t.Name == _ifName);

            if (field == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, $"找不到字段名为{_ifName}的字段");
            }

            if (field.Data == null)
            {
                return true;
            }

            if (field.Data.ToString() == _ifValue)
            {
                return false;
            }

            return true;
        }
    }
}
