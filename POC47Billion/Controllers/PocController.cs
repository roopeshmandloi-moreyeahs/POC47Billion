using Microsoft.AspNetCore.Mvc;
using POC47Billion.Repository;
using POC47Billion.Response;
using PuppeteerSharp;
using System;
using System.Collections;
using System.Net;
using System.Text;

namespace POC47Billion.Controllers
{
    public class PocController : Controller
    {
        #region Convert Html To PDF API

        [HttpPost(nameof(ConvertHtmlToPDF))]
        public async Task<IActionResult> ConvertHtmlToPDF([FromBody] Model.PocModel.Root root)
        {

            PocControllerRepository repository = new(); // Initializing repository
            ErrorResponse errorResponse;
            try
            {

                // Call the repository method to make html dynamic & then convert HTML to PDF
                ResultResponse rootResponse = await repository.ConvertHtmlToPDF(root);// calling repository

                if (rootResponse.IsSuccessful)  // If success 
                {
                    rootResponse.Message = "Pdf created successfully.";
                    rootResponse.IsSuccessful = true;
                    #region Uncomment following 2 lines of Code to create pdf file in project pdf folder
                    ////var byteArray = rootResponse.PdfByte;
                    ////var byteArray1 = System.IO.File.ReadAllBytes("PdfFile/Riki_Report.pdf");
                    ///Tried to attach byte array in header content
                    ////HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    ////result.Content.Headers.ContentType =
                    ////new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    #endregion
                    return Ok(Result<ResultResponse>.Success("Pdf Generated Successfully!!!", rootResponse));
                }
                else // If failed
                {
                    rootResponse.Message = "Pdf not created!Error-" + rootResponse.Message;
                    rootResponse.IsSuccessful = false;
                    return Ok(Result<DBNull>.Failure("Pdf not Generated!!!"));
                }
            }
            catch (Exception ex)
            {
                // Exception occurred, handle it and set appropriate response
                errorResponse = new()
                {
                    ErrorCode = 500,
                    Message = ex.Message

                };
                // Return 500 Internal Server Error status code along with the result object.
                return BadRequest(errorResponse);

            }

        }

        #endregion
    }
}
