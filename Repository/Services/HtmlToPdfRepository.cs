
using DinkToPdf;
using DinkToPdf.Contracts;
using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using RazorLight;
using static JSON_To_PDF.Response.Result;


namespace JSON_To_PDF.Repository.Services
{
    public class HtmlToPdfRepository : IHtmlToPdfRepository
    {
        private readonly IConverter _converter;
      

        private readonly IRazorLightEngine _razorLightEngine;

        public HtmlToPdfRepository(IRazorLightEngine razorLightEngine, IConverter converter)
        {
            _razorLightEngine = razorLightEngine;
            _converter = converter;
        }

        #region main calling unit
        public async Task<ResultResponse> GeneratePdfFromModel(RikiResultSet rikiResult)
        {
            ResultResponse result = new();
            try
            {               
                if (rikiResult != null)
                {

                    String[] File_Paths = new string[] { "QualifiedBorrowerReport.cshtml", "RikiReport.cshtml" };

                    foreach (var filepath in File_Paths)
                    {
                        /// to read file and return html string
                        string htmlContent;

                        using (var reader = new StreamReader(@"Views/QualifiedBorrowerReport.cshtml"))
                        {
                            htmlContent = await reader.ReadToEndAsync();
                        }

                        //to read file and return html string

                        string htmlCode = PopulateHtmlWithDynamicValues(htmlContent, rikiResult);
                       
                        result = await GeneratePdfFromModel(htmlCode);
                        
                        if (result != null && result.Status && result.PdfInByte != null)
                        {
                           //to save data to desktop folder directly
                            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //get the desktop path
                            string outputPath = Path.Combine(desktopPath, "Reports"); // Add subfolder on the desktop
                            string fetchFileNameFromfilepath = System.IO.Path.GetFileNameWithoutExtension(filepath);
                            string outputFilePath = Path.Combine(outputPath, fetchFileNameFromfilepath + DateTime.Now.ToString("dd-H.mmtt") + ".pdf");

                            Directory.CreateDirectory(outputPath); // Create the output directory if it doesn't exist
                                                                   //to save data to desktop folder directly

                            File.WriteAllBytes(outputFilePath, result.PdfInByte);

                            Console.WriteLine("PDF saved successfully.");

                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                result.Message = ex.Message;
                result.Status = false;
            }
            return result;

        }
        #endregion


        #region main calling unit
        public async Task<ResultResponse> GeneratePdfRikiReportFromModel(RikiResultSet rikiResult)
        {
            ResultResponse result = new();
            try
            {
                if (rikiResult != null)
                {

                    String[] File_Paths = new string[] { "QualifiedBorrowerReport.cshtml", "RikiReport.cshtml" };

                    foreach (var filepath in File_Paths)
                    {

                        // to read file and return html string

                        string htmlContent;

                        using (var reader = new StreamReader(@"Views/RikiReport.cshtml"))
                        {
                            htmlContent = await reader.ReadToEndAsync();
                        }

                        //to read file and return html string

                        string htmlCode = PopulateHtmlWithDynamicValues(htmlContent, rikiResult);
                        // var convertedInByte = await ConvertHtmlToPdf(htmlCode);
                        result = await GeneratePdfFromModel(htmlCode);

                        if (result != null && result.Status && result.PdfInByte != null)
                        {
                            //to save data to desktop folder directly
                            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //get the desktop path
                            string outputPath = Path.Combine(desktopPath, "Reports"); // Add subfolder on the desktop
                            string fetchFileNameFromfilepath = System.IO.Path.GetFileNameWithoutExtension(filepath);
                            string outputFilePath = Path.Combine(outputPath, fetchFileNameFromfilepath + DateTime.Now.ToString("dd-H.mmtt") + ".pdf");

                            Directory.CreateDirectory(outputPath); // Create the output directory if it doesn't exist
                                                                   //to save data to desktop folder directly

                            File.WriteAllBytes(outputFilePath, result.PdfInByte);

                            Console.WriteLine("PDF saved successfully.");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = false;
            }
            return result;

        }
        #endregion

        #region populate dynamic value to html

        private string PopulateHtmlWithDynamicValues(string htmlContent, RikiResultSet model)
        {
            try
            {
                ///testing with html

                string templateKey = "myUniqueTemplateKey" + DateTime.Now.ToString("dd-H.mmtt");
                var result = _razorLightEngine.CompileRenderStringAsync(templateKey,
                    htmlContent, model).GetAwaiter().GetResult();

                ///testing with html

                return result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region convert html code to pdf
        private async Task<ResultResponse> GeneratePdfFromModel(string htmlCode)
        {
            ResultResponse result = new();
           try
            {
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                    },
                    Objects =
                {
                  new ObjectSettings()
                  {
                    PagesCount = true,
                    HtmlContent = htmlCode,
                    UseLocalLinks = true,
                    UseExternalLinks = true,
                    WebSettings = { DefaultEncoding = "utf-8",
                    EnableJavascript=true,
                    PrintMediaType=true,
                    
                      },
                    LoadSettings={RepeatCustomHeaders =true},
                 HeaderSettings = { FontName = "Arial", FontSize = 9, Line = false, Center="47Billion"},
                 FooterSettings = { FontName = "Arial", FontSize = 9, Line = false,Center="47Billion"}

                   }
                },
                };
                var pdfData = _converter.Convert(doc);

                result.PdfInByte = pdfData;
                result.Status = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = false;
            }
            return result;


        }
        #endregion
    }
}
