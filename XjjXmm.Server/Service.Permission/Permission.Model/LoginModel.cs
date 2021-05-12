

using DoCare.Zkzx.Core.FrameWork.Tool.DataValidation;

namespace Permission.Model
{
    /// <summary>
    /// 登入viewmodel
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// 密码
        /// </summary>

        [Required]
        public string Password { get; set; }


    }

}
