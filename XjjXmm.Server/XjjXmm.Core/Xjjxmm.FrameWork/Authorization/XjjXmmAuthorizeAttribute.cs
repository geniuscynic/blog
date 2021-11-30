using Microsoft.AspNetCore.Authorization;

namespace XjjXmm.FrameWork.Authorization
{
    public class XjjXmmAuthorizeAttribute : AuthorizeAttribute
    {
        public XjjXmmAuthorizeAttribute() : base(policy: "XjjXmmPolicy")
        {

        }
    }
}
