using POC47Billion.InterFace;
using PuppeteerSharp;
using static POC47Billion.Model.PocModel;
using POC47Billion.Response;
using POC47Billion.DTO;
using System.Text;

namespace POC47Billion.Repository
{
    public class PocControllerRepository : IPocController
    {
        #region Implementation of Convert Html To PDF Method.  
        public async Task<ResultResponse> ConvertHtmlToPDF(Root root)
        {
            ResultResponse resultResponse = new();
            try
            {

                if (root != null)
                {
                    string htmlCode = await SetDynamicValueIntoHtmlAsync(root); //call method to set the dynamic value in html page.
                    PdfResponse pdfResponse = await ConvertHtmlToPdfByHtmlCode(htmlCode); //Call method for converting html to pdf.
                    if (pdfResponse != null)
                    {
                        resultResponse.PdfByte = pdfResponse.PdfByteArr;
                        resultResponse.Message = "PDF gets into Byte.";
                        resultResponse.IsSuccessful = true;
                    }
                    else
                    {
                        resultResponse.Message = "Byte not getting";
                        resultResponse.IsSuccessful = false;
                    }
                }
            }
            catch (Exception ex)
            {
                resultResponse.Message = ex.Message;
            }
            return resultResponse;
        }
        #endregion

        #region Set Dynamic Value Into Html
        public async Task<string> SetDynamicValueIntoHtmlAsync(Root root)  // set the dynamic value in html page.
        {
            string htmlCode;
            try
            {
                PocClass dto = new();
                HtmlStringClass bodyString = new();
                string filePath = @"HTMLPage/Riki_Report.html"; // Replace with the actual path

                htmlCode = await ReadLocalFileAsync(filePath);
                ///Here we can create a function to make it generic to avoid multiple lines of code
                ///to replace values
                #region Replacing for dynamic values signle field

                htmlCode = htmlCode.Replace("{rikIasWords}", Convert.ToString(root?.rikiResultSet?.rikiData?.rikIasWords));
                htmlCode = htmlCode.Replace("{id}", Convert.ToString(root?.rikiResultSet?.consumer?.identifiers?.Id));
                htmlCode = htmlCode.Replace("{date}", Convert.ToString(root?.rikiResultSet?.transactions?.date));
                htmlCode = htmlCode.Replace("{email}", Convert.ToString(root?.rikiResultSet?.consumer?.email));
                htmlCode = htmlCode.Replace("{phoneNumber}", Convert.ToString(root?.rikiResultSet?.consumer?.phoneNumber));
                htmlCode = htmlCode.Replace("{riki}", Convert.ToString(root?.rikiResultSet?.rikiData?.riki));
                htmlCode = htmlCode.Replace("{ccTrend}", Convert.ToString(root?.rikiResultSet?.rikiData?.ccTrend));
                htmlCode = htmlCode.Replace("{cashFlowIndex}", Convert.ToString(root?.rikiResultSet?.rikiData?.cashFlowIndex));
                htmlCode = htmlCode.Replace("{cashFlowIndexTrend}", Convert.ToString(root?.rikiResultSet?.rikiData?.cashFlowIndexTrend));
                htmlCode = htmlCode.Replace("{typicalMonthsTotalIncome}", Convert.ToString(root?.rikiResultSet?.rikiData?.typicalMonthsTotalIncome.ToString("N2")));
                htmlCode = htmlCode.Replace("{typicalMonthsUnadjustedAvailableIncome}", Convert.ToString(root?.rikiResultSet?.rikiData?.typicalMonthsUnadjustedAvailableIncome.ToString("N2")));
                htmlCode = htmlCode.Replace("{monthToMonthStabilityScore}", Convert.ToString(root?.rikiResultSet?.rikiData?.monthToMonthStabilityScore));
                htmlCode = htmlCode.Replace("{maxConsecutive}", Convert.ToString(root?.rikiResultSet?.recurrentItems?.maxConsecutive));
                htmlCode = htmlCode.Replace("{totalMonthlyIncomeTrend}", Convert.ToString(root?.rikiResultSet?.rikiData?.totalMonthlyIncomeTrend));
                htmlCode = htmlCode.Replace("{amount}", Convert.ToString(root?.rikiResultSet?.transactions?.amount.ToString("N2")));
                htmlCode = htmlCode.Replace("{accountId}", Convert.ToString(root?.rikiResultSet?.transactions?.accountId));
                htmlCode = htmlCode.Replace("{description}", Convert.ToString(root?.rikiResultSet?.transactions?.description));
                htmlCode = htmlCode.Replace("{totalMonthlyIncomeTrend}", Convert.ToString(root?.rikiResultSet?.rikiData?.totalMonthlyIncomeTrend));
                htmlCode = htmlCode.Replace("{prospectiveRIKI_500}", Convert.ToString(root?.rikiResultSet?.rikiData?.prospectiveRIKI_500.ToString("N2")));

                #endregion

                #region Binding table data for the looping

                var DaysInMonthNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{DaysInMonth}</td>";

                var AccountsTranckedNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{AccountsTrancked}</td>";

                var IncomeTransactionsNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{IncomeTransactions}</td>";

                var ExpenseTransactionsNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{ExpenseTransactions}</td>";

                var TotalAmountIncomeTransactionsNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{TotalAmountIncomeTransactions}</td>";

                var TotalAmountExpenseTransactionsNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{TotalAmountExpenseTransactions}</td>";

                var TransactionsNotAcctToAcctTransfersNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{TransactionsNotAcctToAcctTransfers}</td>";

                var LargestSingleTransactionNotAcctToAcctTransfersNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{LargestSingleTransactionNotAcctToAcctTransfers}</td>";

                var CombinedBankAccountBalanceAvgNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedBankAccountBalanceAvg}</td>";

                var CombinedCreditCardBalanceAvgNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedCreditCardBalanceAvg}</td>";


                var incomeExpenseRatioNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{incomeExpenseRatio}</td>";

                var monthlyDataCompletenessNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{monthlyDataCompleteness}</td>";

                var avgDailySpendingNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{avgDailySpending}</td>";

                var depletionDaysNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{depletionDays}</td>";
                var estimatedDiscretionarySpendingNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{estimatedDiscretionarySpending}</td>";

                var adjustedAvailableIncomeNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{adjustedAvailableIncome}</td>";

                #endregion Biding done.

                #region Iterating MonthlyStatistic collection
                for (int i = 0; i < root?.rikiResultSet?.calendarMonthStatistics?.monthlyStatistics?.Count; i++)
                {

                    MonthlyStatistic? item = root.rikiResultSet.calendarMonthStatistics.monthlyStatistics[i];

                    dto.DaysInMonth = dto.DaysInMonth + DaysInMonthNewString.Replace("{DaysInMonth}", Convert.ToString(item.daysInMonth));
                    
                    dto.AccountsTrancked = dto.AccountsTrancked + AccountsTranckedNewString.Replace("{AccountsTrancked}", Convert.ToString(item.accountsTracked));

                    dto.IncomeTransactions = dto.IncomeTransactions + IncomeTransactionsNewString.Replace("{IncomeTransactions}", Convert.ToString(item.incomeTransactions));
                    
                    dto.ExpenseTransactions = dto.ExpenseTransactions + ExpenseTransactionsNewString.Replace("{ExpenseTransactions}", Convert.ToString(item.expenseTransactions));
                    
                    dto.TotalAmountIncomeTransactions = dto.TotalAmountIncomeTransactions + TotalAmountIncomeTransactionsNewString.Replace("{TotalAmountIncomeTransactions}", Convert.ToString(item.totalAmountIncomeTransactions));
                   
                    dto.TotalAmountExpenseTransactions = dto.TotalAmountExpenseTransactions + TotalAmountExpenseTransactionsNewString.Replace("{TotalAmountExpenseTransactions}", Convert.ToString(item.totalAmountExpenseTransactions));
                    
                    dto.TransactionsNotAcctToAcctTransfers = dto.TransactionsNotAcctToAcctTransfers + TransactionsNotAcctToAcctTransfersNewString.Replace("{TransactionsNotAcctToAcctTransfers}", Convert.ToString(item.transactionsNotAcctToAcctTransfers));
                   
                    dto.LargestSingleTransactionNotAcctToAcctTransfers = dto.LargestSingleTransactionNotAcctToAcctTransfers + LargestSingleTransactionNotAcctToAcctTransfersNewString.Replace("{LargestSingleTransactionNotAcctToAcctTransfers}", Convert.ToString(item.largestSingleTransactionNotAcctToAcctTransfers));
                    
                    dto.CombinedBankAccountBalanceAvg = dto.CombinedBankAccountBalanceAvg + CombinedBankAccountBalanceAvgNewString.Replace("{CombinedBankAccountBalanceAvg}", Convert.ToString(item.combinedBankAccountBalanceAvg));
                    
                    dto.CombinedCreditCardBalanceAvg = dto.CombinedCreditCardBalanceAvg + CombinedCreditCardBalanceAvgNewString.Replace("{CombinedCreditCardBalanceAvg}", Convert.ToString(item.combinedCreditCardBalanceAvg));
                 
                }
                #endregion

                #region Dynamic table
                // Dynamic table 

                htmlCode = htmlCode.Replace("{DaysInMonthTable}", dto.DaysInMonth);
                htmlCode = htmlCode.Replace("{monthlyDataCompletenessTable}", dto.monthlyDataCompleteness);
                htmlCode = htmlCode.Replace("{AccountsTranckedTable}", dto.AccountsTrancked);
                htmlCode = htmlCode.Replace("{IncomeTransactionsTable}", dto.IncomeTransactions);
                htmlCode = htmlCode.Replace("{ExpenseTransactionsTable}", dto.ExpenseTransactions);
                htmlCode = htmlCode.Replace("{TotalAmountIncomeTransactionsTable}", dto.TotalAmountIncomeTransactions);
                htmlCode = htmlCode.Replace("{TotalAmountExpenseTransactionsTable}", dto.TotalAmountExpenseTransactions);
                htmlCode = htmlCode.Replace("{TransactionsNotAcctToAcctTransfersTable}", dto.TransactionsNotAcctToAcctTransfers);
                htmlCode = htmlCode.Replace("{LargestSingleTransactionNotAcctToAcctTransfersTable}", dto.LargestSingleTransactionNotAcctToAcctTransfers);
                htmlCode = htmlCode.Replace("{CombinedBankAccountBalanceAvgTable}", dto.CombinedBankAccountBalanceAvg);
                htmlCode = htmlCode.Replace("{CombinedCreditCardBalanceAvgTable}", dto.CombinedCreditCardBalanceAvg);

                #endregion table created

                #region Iterating MonthlyAnalysis collection
                for (int j = 0; j < root?.rikiResultSet?.calendarMonthStatistics?.monthlyAnalysis?.Count; j++)
                {
                    MonthlyAnalysis? item1 = root.rikiResultSet.calendarMonthStatistics.monthlyAnalysis[j];

                    dto.incomeExpenseRatio = dto.incomeExpenseRatio + incomeExpenseRatioNewString.Replace("{incomeExpenseRatio}", Convert.ToString(item1.incomeExpenseRatio));
                    
                    dto.monthlyDataCompleteness = dto.monthlyDataCompleteness + monthlyDataCompletenessNewString.Replace("{monthlyDataCompleteness}", Convert.ToString(item1.monthlyDataCompleteness));
                    
                    dto.avgDailySpending = dto.avgDailySpending + avgDailySpendingNewString.Replace("{avgDailySpending}", Convert.ToString(item1.avgDailySpending));
                    
                    dto.depletionDays = dto.depletionDays + depletionDaysNewString.Replace("{depletionDays}", Convert.ToString(item1.depletionDays));
                    
                    dto.estimatedDiscretionarySpending = dto.estimatedDiscretionarySpending + estimatedDiscretionarySpendingNewString.Replace("{estimatedDiscretionarySpending}", Convert.ToString(item1.estimatedDiscretionarySpending));
                    
                    dto.adjustedAvailableIncome = dto.adjustedAvailableIncome + adjustedAvailableIncomeNewString.Replace("{adjustedAvailableIncome}", Convert.ToString(item1.adjustedAvailableIncome));
                   
                }
                #endregion

                #region Dynamic table monthlyAnalysis
                // Dynamic table 
                htmlCode = htmlCode.Replace("{adjustedAvailableIncomeTable}", dto.adjustedAvailableIncome);
                htmlCode = htmlCode.Replace("{estimatedDiscretionarySpendingTable}", dto.estimatedDiscretionarySpending);
                htmlCode = htmlCode.Replace("{depletionDaysTable}", dto.depletionDays);
                htmlCode = htmlCode.Replace("{incomeExpenseRatioTable}", dto.incomeExpenseRatio);
                htmlCode = htmlCode.Replace("{avgDailySpendingTable}", dto.avgDailySpending);

                #endregion table created
                return htmlCode;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Convert Html To Pdf File using Puppeteer sharp
        public async Task<PdfResponse> ConvertHtmlToPdfByHtmlCode(string htmlCode)
        {
            PdfResponse pdf = new();
            try
            {
                // Download the Chromium browser for PuppeteerSharp library
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);// Using for PuppeteerSharp library

                // Launch the browser
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true
                });

                var page = await browser.NewPageAsync();

                await page.SetContentAsync(htmlCode);// Replace with your HTML file path

                var pdfOptions = new PdfOptions // Pdf options for page setup
                {
                    Format = PuppeteerSharp.Media.PaperFormat.A3, //for layout of the page
                    PrintBackground = true // for applying the html color on pdf
                };
                pdf.PdfByteArr = await page.PdfDataAsync(pdfOptions);

                await browser.CloseAsync();
                /// Uncomment following 2 lines to create pdf in our Pdf folder in project directory 
                //File.WriteAllBytes("PdfFile/Riki_Report.pdf", pdfData); // write file
                //pdf.PdfByteArr = File.ReadAllBytes("PdfFile/Riki_Report.pdf"); // Read file for getting the byte.
                pdf.PdfMessage = "PDF Created Successfully";
                return pdf;
            }
            catch (Exception ex)
            {
                pdf.PdfMessage = ex.Message;
                return pdf;
            }
        }
        #endregion

        #region Read Local File
        static async Task<string> ReadLocalFileAsync(string filePath)
        {
            string fileContent;

            using (var reader = new StreamReader(filePath))
            {
                fileContent = await reader.ReadToEndAsync();
            }

            return fileContent;
        }
        #endregion
        public class PdfConverter
        {
            public byte[] ConvertPdfToByteArray(string filePath)
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("The PDF file does not exist.", filePath);
                }

                // Read all bytes from the file and return as a byte array
                byte[] pdfBytes = File.ReadAllBytes(filePath);
                return pdfBytes;
            }
        }
    }
}
