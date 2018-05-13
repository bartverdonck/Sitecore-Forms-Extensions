using System.Threading.Tasks;

namespace Feature.FormsExtensions.Business.ReCaptcha
{
    public interface IReCaptchaService
    {
        Task<bool> Verify(string response);
        bool VerifySync(string response);
    }
}
