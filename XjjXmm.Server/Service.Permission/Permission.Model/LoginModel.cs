

using DoCare.Zkzx.Core.FrameWork.Tool.DataValidation;
using Permission.IRepository;

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

    //public class LoginModelUserExistsCustomValidator : AbstractValidator
    //{
    //    public override string CustomMessage { get; set; } = "用户名或者密码错误";
    //    public IUserRepository UserRepository { get; set; }

    //    public override bool IsValid(object value, object model)
    //    {
    //        LoginModel loginModel = (LoginModel) model;

    //        var user = UserRepository.FirstOrDefault(t => t.Account == loginModel.Login && t.Password == loginModel.Password).Result;

    //        if (user == null)
    //        {
    //            return false;
    //        }

    //        return true;
    //    }
    //}
}
