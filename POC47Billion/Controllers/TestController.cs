using Microsoft.AspNetCore.Mvc;
using POC47Billion.Repository;
using POC47Billion.Response;
using PuppeteerSharp;
using System;
using System.Collections;
using System.Text;

namespace POC47Billion.Controllers
{
    public class TestController : Controller
    {
        #region Convert Html To PDF API

        [HttpPost(nameof(ConvertHtmlToPDF1))]
        public async Task<byte[]> ConvertHtmlToPDF1([FromBody] Model.PocModel.Definitions def)
        {
            ResultResponse rootResponse = new(); // Initializing response
            PocControllerRepository repository = new(); // Initializing repository
            ErrorResponse errorResponse;
            try
            {
                // Download the Chromium browser for PuppeteerSharp library
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);// Using for PuppeteerSharp library

                // Launch the browser
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true
                });

                // Call the repository method to convert HTML to PDF
                rootResponse = await repository.ConvertHtmlToPDF(def, browser);// calling repository

                if (rootResponse.IsSuccessful)  // If success 
                {
                    rootResponse.Message = "Pdf created successfully.";
                    rootResponse.IsSuccessful = true;
                    return rootResponse.PdfByte;
                    //return Ok(Result<ResultResponse>.Success("Pdf Generated Successfully!!!", rootResponse));
                }
                else // If failed
                {
                    rootResponse.Message = "Pdf not created!Error-" + rootResponse.Message;
                    rootResponse.IsSuccessful = false;
                    return rootResponse.PdfByte;
                    //return Ok(Result<DBNull>.Failure("Pdf not Generated!!!"));
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
                return null;
                //return BadRequest(errorResponse);

            }

        }

        #endregion
    }
}
