using POC47Billion.Response;
using PuppeteerSharp;
using static POC47Billion.Model.PocModel;

namespace POC47Billion.InterFace
{
    public interface IPocController
    {
        #region Method defination
        public Task<ResultResponse> ConvertHtmlToPDF(Root root);
        #endregion
    }
}
