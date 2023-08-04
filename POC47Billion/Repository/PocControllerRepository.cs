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
        public async Task<ResultResponse> ConvertHtmlToPDF(Definitions defination,IBrowser browser)
        {
            ResultResponse resultResponse = new();
            try
            {
                
                if (defination != null)
                {
                    string htmlCode = await SetDynamicValueIntoHtmlAsync(defination); //call method to set the dynamic value in html page.
                    PdfResponse pdfResponse = await ConvertHtmlToPdfByHtmlCode(htmlCode, browser); //Call method for converting html to pdf.
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
        public async Task<string> SetDynamicValueIntoHtmlAsync(Definitions definition)  // set the dynamic value in html page.
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
                #region Replacing for dynamic values

                htmlCode = htmlCode.Replace("{Street}", Convert.ToString(definition.Address?.Street));
                htmlCode = htmlCode.Replace("{Street2}", Convert.ToString(definition.Address?.Street2));
                htmlCode = htmlCode.Replace("{City}", Convert.ToString(definition.Address?.City));
                htmlCode = htmlCode.Replace("{Region}", Convert.ToString(definition.Address?.Region));
                htmlCode = htmlCode.Replace("{PostalCode}", Convert.ToString(definition.Address?.PostalCode));
                htmlCode = htmlCode.Replace("{Country}", Convert.ToString(definition.Address?.Country));
                htmlCode = htmlCode.Replace("{Label}", Convert.ToString(definition.CalendarMonthStatistics?.Label));
                htmlCode = htmlCode.Replace("{MonthsRequested}", Convert.ToString(definition.CalendarMonthStatistics?.MonthsRequested));
                htmlCode = htmlCode.Replace("{MonthsDelivered}", Convert.ToString(definition.CalendarMonthStatistics?.MonthsDelivered));
                htmlCode = htmlCode.Replace("{LastDayOfLastFullMonthCovered}", Convert.ToString(definition.CalendarMonthStatistics?.LastDayOfLastFullMonthCovered));
                htmlCode = htmlCode.Replace("{BeginningOfFirstMonth}", Convert.ToString(definition.CalendarMonthStatistics?.BeginningOfFirstMonth));
                htmlCode = htmlCode.Replace("{ConsumerId}", Convert.ToString(definition.Consumer?.ConsumerId));
                htmlCode = htmlCode.Replace("{FirstName}", Convert.ToString(definition.Consumer?.FirstName));
                htmlCode = htmlCode.Replace("{LastName}", Convert.ToString(definition.Consumer?.LastName));
                htmlCode = htmlCode.Replace("{DateOfBirth}", Convert.ToString(definition.Consumer?.DateOfBirth));
                htmlCode = htmlCode.Replace("{Email}", Convert.ToString(definition.Consumer?.Email));
                htmlCode = htmlCode.Replace("{PhoneNumber}", Convert.ToString(definition.Consumer?.PhoneNumber));
                htmlCode = htmlCode.Replace("{Source}", Convert.ToString(definition.Identifier?.Source));
                htmlCode = htmlCode.Replace("{Id}", Convert.ToString(definition.Identifier?.Id));
                htmlCode = htmlCode.Replace("{MonthlyDataCompleteness}", Convert.ToString(definition.MonthlyAnalyses?.MonthlyDataCompleteness));
                htmlCode = htmlCode.Replace("{IncomeExpenseRatio}", Convert.ToString(definition.MonthlyAnalyses?.AvgDailySpending));
                htmlCode = htmlCode.Replace("{MonthlyDataCompleteness}", Convert.ToString(definition.MonthlyAnalyses?.IncomeExpenseRatio));
                htmlCode = htmlCode.Replace("{DepletionDays}", Convert.ToString(definition.MonthlyAnalyses?.DepletionDays));
                htmlCode = htmlCode.Replace("{MonthlyAllocationChangeScore}", Convert.ToString(definition.MonthlyAnalyses?.MonthlyAllocationChangeScore));
                htmlCode = htmlCode.Replace("{EstimatedDiscretionarySpending}", Convert.ToString(definition.MonthlyAnalyses?.EstimatedDiscretionarySpending));
                htmlCode = htmlCode.Replace("{UnadjustedAvailableIncome}", Convert.ToString(definition.MonthlyAnalyses?.UnadjustedAvailableIncome));
                htmlCode = htmlCode.Replace("{CashFlowIndexMonthly}", Convert.ToString(definition.MonthlyAnalyses?.CashFlowIndexMonthly));
                htmlCode = htmlCode.Replace("{UnadjustedCashFlowIndexMonthly}", Convert.ToString(definition.MonthlyAnalyses?.UnadjustedCashFlowIndexMonthly));
                htmlCode = htmlCode.Replace("{ProspectiveCashFlowIndexMonthly500}", Convert.ToString(definition.MonthlyAnalyses?.ProspectiveCashFlowIndexMonthly500.ToString("N2")));
                htmlCode = htmlCode.Replace("{GroupType}", Convert.ToString(definition.RecurrentItems?.GroupType));
                htmlCode = htmlCode.Replace("{Description}", Convert.ToString(definition.RecurrentItems?.Description));
                htmlCode = htmlCode.Replace("{Occurrences}", Convert.ToString(definition.RecurrentItems?.Occurrences));
                htmlCode = htmlCode.Replace("{MaxConsecutive}", Convert.ToString(definition.RecurrentItems?.MaxConsecutive));
                htmlCode = htmlCode.Replace("{StartDate}", Convert.ToString(definition.RecurrentItems?.StartDate));
                htmlCode = htmlCode.Replace("{EndDate}", Convert.ToString(definition.RecurrentItems?.EndDate));
                htmlCode = htmlCode.Replace("{AccountId}", Convert.ToString(definition.ResponseTransaction?.AccountId));
                htmlCode = htmlCode.Replace("{ExternalTransactionId}", Convert.ToString(definition.ResponseTransaction?.ExternalTransactionId));
                htmlCode = htmlCode.Replace("{Date}", Convert.ToString(definition.ResponseTransaction?.Date));
                htmlCode = htmlCode.Replace("{Description}", Convert.ToString(definition.ResponseTransaction?.Description));
                htmlCode = htmlCode.Replace("{Action}", Convert.ToString(definition.ResponseTransaction?.Action));
                htmlCode = htmlCode.Replace("{Amount}", Convert.ToString(definition.ResponseTransaction?.Amount.ToString("N2")));
                htmlCode = htmlCode.Replace("{RIKIasWords}", Convert.ToString(definition.RikiData?.RIKIasWords));
                htmlCode = htmlCode.Replace("{RIKI}", Convert.ToString(definition.RikiData?.RIKI));
                htmlCode = htmlCode.Replace("{MonthToMonthStabilityScore}", Convert.ToString(definition.RikiData?.MonthToMonthStabilityScore));
                htmlCode = htmlCode.Replace("{TypicalMonthsTotalIncome}", Convert.ToString(definition.RikiData?.TypicalMonthsTotalIncome.ToString("N2")));
                htmlCode = htmlCode.Replace("{TypicalMonthsUnadjustedAvailableIncome}", Convert.ToString(definition.RikiData?.TypicalMonthsUnadjustedAvailableIncome.ToString("N2")));
                htmlCode = htmlCode.Replace("{TypicalMonthsAdjustedAvailableIncome}", Convert.ToString(definition.RikiData?.TypicalMonthsAdjustedAvailableIncome));
                htmlCode = htmlCode.Replace("{CashFlowIndex}", Convert.ToString(definition.RikiData?.CashFlowIndex));
                htmlCode = htmlCode.Replace("{UnadjustedRIKI}", Convert.ToString(definition.RikiData?.UnadjustedRIKI));
                htmlCode = htmlCode.Replace("{UnadjustedCashFlowIndex}", Convert.ToString(definition.RikiData?.UnadjustedCashFlowIndex));
                htmlCode = htmlCode.Replace("{ProspectiveRIKI_500}", Convert.ToString(definition.RikiData?.ProspectiveRIKI_500.ToString("N2")));
                htmlCode = htmlCode.Replace("{TotalMonthlyIncomeTrend}", Convert.ToString(definition.RikiData?.TotalMonthlyIncomeTrend));
                htmlCode = htmlCode.Replace("{CashFlowIndexTrend}", Convert.ToString(definition.RikiData?.CashFlowIndexTrend));
                htmlCode = htmlCode.Replace("{CCChangeTrend}", Convert.ToString(definition.RikiData?.CCChangeTrend));
                htmlCode = htmlCode.Replace("{CCTrend}", Convert.ToString(definition.RikiData?.CCTrend));
                htmlCode = htmlCode.Replace("{BankAccountTrend}", Convert.ToString(definition.RikiData?.BankAccountTrend));
                htmlCode = htmlCode.Replace("{BankAccountChangeTrend}", Convert.ToString(definition.RikiData?.BankAccountChangeTrend));
                
               #endregion

                #region Binding table data for the looping

                bodyString.DaysInMonthString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;>{DaysInMonth}</td>";
                bodyString.AccountsTranckedString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{AccountsTrancked}</td>";
                bodyString.IncomeTransactionsString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{IncomeTransactions}</td>";
                bodyString.IncomeTransactionsString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{ExpenseTransactions}</td>";
                bodyString.ExpenseTransactionsString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{TotalAmountIncomeTransactions}</td>";
                bodyString.TotalAmountIncomeTransactionsString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{TotalAmountExpenseTransactions}</td>";
                bodyString.TransactionsNotAcctToAcctTransfersString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{TransactionsNotAcctToAcctTransfers}</td>";
                bodyString.LargestSingleTransactionNotAcctToAcctTransfersString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{LargestSingleTransactionNotAcctToAcctTransfers}</td>";
                bodyString.BankAccountsVisibleString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;>{BankAccountsVisible}</td>";
                bodyString.DDAAccountsVisibleString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{DDAAccountsVisible}</td>";
                bodyString.CombinedBankAccountBalanceAvgString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedBankAccountBalanceAvg}</td>";
                bodyString.CombinedBankAccountBalanceMinString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedBankAccountBalanceMin}</td>";
                bodyString.CombinedCreditCardBalanceMaxString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedCreditCardBalanceMax}</td>";
                bodyString.CreditCardAccountsVisibleString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CreditCardAccountsVisible}</td>";
                bodyString.CombinedCreditCardBalanceAvgString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedCreditCardBalanceAvg}</td>";
                bodyString.CombinedCreditCardBalanceMinString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedCreditCardBalanceMin}</td>";

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
                
                var BankAccountsVisibleNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{BankAccountsVisible}</td>";
                
                var DDAAccountsVisibleNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{DDAAccountsVisible}</td>";
                
                var CombinedBankAccountBalanceAvgNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedBankAccountBalanceAvg}</td>";
                
                var CombinedBankAccountBalanceMinNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedBankAccountBalanceMin}</td>";
                
                var CombinedCreditCardBalanceMaxNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedCreditCardBalanceMax}</td>";
                
                var CreditCardAccountsVisibleNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CreditCardAccountsVisible}</td>";
                
                var CombinedCreditCardBalanceAvgNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedCreditCardBalanceAvg}</td>";
                
                var CombinedCreditCardBalanceMinNewString = @"<td style='font-size: 13px;border-radius: 4px;border: 1px solid #ddd;padding: 5px 10px;
                white-space: nowrap;text-align: right;'>{CombinedCreditCardBalanceMin}</td>";
                
                #region Iterating MonthlyStatistic collection
                for (int i = 0; i < definition.MonthlyStatistic?.Count; i++)
                {
                        
                    MonthlyStatistic? item = definition.MonthlyStatistic[i];
                        
                    bodyString.DaysInMonthString = DaysInMonthNewString;
                    bodyString.DaysInMonthString = bodyString.DaysInMonthString.Replace("{DaysInMonth}", Convert.ToString(item.DaysInMonth));
                    var tbody = dto.DaysInMonth + bodyString.DaysInMonthString;
                    dto.DaysInMonth = tbody;

                    bodyString.AccountsTranckedString = AccountsTranckedNewString;
                    bodyString.AccountsTranckedString = bodyString.AccountsTranckedString.Replace("{AccountsTrancked}", Convert.ToString(item.AccountsTrancked));
                    var tbody1 = dto.AccountsTrancked + bodyString.AccountsTranckedString;
                    dto.AccountsTrancked = tbody1;

                    bodyString.IncomeTransactionsString = IncomeTransactionsNewString;
                    bodyString.IncomeTransactionsString = bodyString.IncomeTransactionsString.Replace("{IncomeTransactions}", Convert.ToString(item.IncomeTransactions));
                    var tbody2 = dto.IncomeTransactions + bodyString.IncomeTransactionsString;
                    dto.IncomeTransactions = tbody2;

                    bodyString.ExpenseTransactionsString = ExpenseTransactionsNewString;
                    bodyString.ExpenseTransactionsString = bodyString.ExpenseTransactionsString.Replace("{ExpenseTransactions}", Convert.ToString(item.ExpenseTransactions));
                    var tbody3 = dto.ExpenseTransactions + bodyString.ExpenseTransactionsString;
                    dto.ExpenseTransactions = tbody3;

                    bodyString.TotalAmountIncomeTransactionsString = TotalAmountIncomeTransactionsNewString;
                    bodyString.TotalAmountIncomeTransactionsString = bodyString.TotalAmountIncomeTransactionsString.Replace("{TotalAmountIncomeTransactions}", Convert.ToString(item.TotalAmountIncomeTransactions));
                    var tbody4 = dto.TotalAmountIncomeTransactions + bodyString.TotalAmountIncomeTransactionsString;
                    dto.TotalAmountIncomeTransactions = tbody4;

                    bodyString.TotalAmountExpenseTransactionsString = TotalAmountExpenseTransactionsNewString;
                    bodyString.TotalAmountExpenseTransactionsString = bodyString.TotalAmountExpenseTransactionsString.Replace("{TotalAmountExpenseTransactions}", Convert.ToString(item.TotalAmountExpenseTransactions));
                    var tbody5 = dto.TotalAmountExpenseTransactions + bodyString.TotalAmountExpenseTransactionsString;
                    dto.TotalAmountExpenseTransactions = tbody5;

                    bodyString.TransactionsNotAcctToAcctTransfersString = TransactionsNotAcctToAcctTransfersNewString;
                    bodyString.TransactionsNotAcctToAcctTransfersString = bodyString.TransactionsNotAcctToAcctTransfersString.Replace("{TransactionsNotAcctToAcctTransfers}", Convert.ToString(item.TransactionsNotAcctToAcctTransfers));
                    var tbody6 = dto.TransactionsNotAcctToAcctTransfers + bodyString.TransactionsNotAcctToAcctTransfersString;
                    dto.TransactionsNotAcctToAcctTransfers = tbody6;

                    bodyString.LargestSingleTransactionNotAcctToAcctTransfersString = LargestSingleTransactionNotAcctToAcctTransfersNewString;
                    bodyString.LargestSingleTransactionNotAcctToAcctTransfersString = bodyString.LargestSingleTransactionNotAcctToAcctTransfersString.Replace("{LargestSingleTransactionNotAcctToAcctTransfers}", Convert.ToString(item.LargestSingleTransactionNotAcctToAcctTransfers));
                    var tbody7 = dto.LargestSingleTransactionNotAcctToAcctTransfers + bodyString.LargestSingleTransactionNotAcctToAcctTransfersString;
                    dto.LargestSingleTransactionNotAcctToAcctTransfers = tbody7;

                    bodyString.BankAccountsVisibleString = BankAccountsVisibleNewString;
                    bodyString.BankAccountsVisibleString = bodyString.BankAccountsVisibleString.Replace("{BankAccountsVisible}", Convert.ToString(item.BankAccountsVisible));
                    var tbody8 = dto.BankAccountsVisible + bodyString.BankAccountsVisibleString;
                    dto.BankAccountsVisible = tbody8;

                    bodyString.DDAAccountsVisibleString = DDAAccountsVisibleNewString;
                    bodyString.DDAAccountsVisibleString = bodyString.DDAAccountsVisibleString.Replace("{DDAAccountsVisible}", Convert.ToString(item.DDAAccountsVisible));
                    var tbody9 = dto.DDAAccountsVisible + bodyString.DDAAccountsVisibleString;
                    dto.DDAAccountsVisible = tbody9;

                    bodyString.CombinedBankAccountBalanceAvgString = CombinedBankAccountBalanceAvgNewString;
                    bodyString.CombinedBankAccountBalanceAvgString = bodyString.CombinedBankAccountBalanceAvgString.Replace("{CombinedBankAccountBalanceAvg}", Convert.ToString(item.CombinedBankAccountBalanceAvg));
                    var tbody10 = dto.CombinedBankAccountBalanceAvg + bodyString.CombinedBankAccountBalanceAvgString;
                    dto.CombinedBankAccountBalanceAvg = tbody10;

                    bodyString.CombinedBankAccountBalanceMinString = CombinedBankAccountBalanceMinNewString;
                    bodyString.CombinedBankAccountBalanceMinString = bodyString.CombinedBankAccountBalanceMinString.Replace("{CombinedBankAccountBalanceMin}", Convert.ToString(item.CombinedBankAccountBalanceMin));
                    var tbody11 = dto.CombinedBankAccountBalanceMin + bodyString.CombinedBankAccountBalanceMinString;
                    dto.CombinedBankAccountBalanceMin = tbody11;

                    bodyString.CombinedCreditCardBalanceMaxString = CombinedCreditCardBalanceMaxNewString;
                    bodyString.CombinedCreditCardBalanceMaxString = bodyString.CombinedCreditCardBalanceMaxString.Replace("{CombinedCreditCardBalanceMax}", Convert.ToString(item.CombinedCreditCardBalanceMax));
                    var tbody12 = dto.CombinedCreditCardBalanceMax + bodyString.CombinedCreditCardBalanceMaxString;
                    dto.CombinedCreditCardBalanceMax = tbody12;

                    bodyString.CreditCardAccountsVisibleString = CreditCardAccountsVisibleNewString;
                    bodyString.CreditCardAccountsVisibleString = bodyString.CreditCardAccountsVisibleString.Replace("{CreditCardAccountsVisible}", Convert.ToString(item.CreditCardAccountsVisible));
                    var tbody13 = dto.CreditCardAccountsVisible + bodyString.CreditCardAccountsVisibleString;
                    dto.CreditCardAccountsVisible = tbody13;

                    bodyString.CombinedCreditCardBalanceAvgString = CombinedCreditCardBalanceAvgNewString;
                    bodyString.CombinedCreditCardBalanceAvgString = bodyString.CombinedCreditCardBalanceAvgString.Replace("{CombinedCreditCardBalanceAvg}", Convert.ToString(item.CombinedCreditCardBalanceAvg));
                    var tbody14 = dto.CombinedCreditCardBalanceAvg + bodyString.CombinedCreditCardBalanceAvgString;
                    dto.CombinedCreditCardBalanceAvg = tbody14;

                    bodyString.CombinedCreditCardBalanceMinString = CombinedCreditCardBalanceMinNewString;
                    bodyString.CombinedCreditCardBalanceMinString = bodyString.CombinedCreditCardBalanceMinString.Replace("{CombinedCreditCardBalanceMin}", Convert.ToString(item.CombinedCreditCardBalanceMin));
                    var tbody15 = dto.CombinedCreditCardBalanceMin + bodyString.CombinedCreditCardBalanceMinString;
                    dto.CombinedCreditCardBalanceMin = tbody15;
                    }
                #endregion

                #endregion Biding done.

                #region Dynamic table
                // Dynamic table 

                htmlCode = htmlCode.Replace("{tableData}", dto.DaysInMonth);
                        htmlCode = htmlCode.Replace("{tableData1}", dto.AccountsTrancked);
                        htmlCode = htmlCode.Replace("{tableData2}", dto.IncomeTransactions);
                        htmlCode = htmlCode.Replace("{tableData3}", dto.ExpenseTransactions);
                        htmlCode = htmlCode.Replace("{tableData4}", dto.TotalAmountIncomeTransactions);
                        htmlCode = htmlCode.Replace("{tableData5}", dto.TotalAmountExpenseTransactions);
                        htmlCode = htmlCode.Replace("{tableData6}", dto.TransactionsNotAcctToAcctTransfers);
                        htmlCode = htmlCode.Replace("{tableData7}", dto.LargestSingleTransactionNotAcctToAcctTransfers);
                        htmlCode = htmlCode.Replace("{tableData8}", dto.BankAccountsVisible);
                        htmlCode = htmlCode.Replace("{tableData9}", dto.DDAAccountsVisible);
                        htmlCode = htmlCode.Replace("{tableData10}", dto.CombinedBankAccountBalanceAvg);
                        htmlCode = htmlCode.Replace("{tableData11}", dto.CombinedBankAccountBalanceMin);
                        htmlCode = htmlCode.Replace("{tableData12}", dto.CombinedCreditCardBalanceMax);
                        htmlCode = htmlCode.Replace("{tableData13}", dto.CreditCardAccountsVisible);
                        htmlCode = htmlCode.Replace("{tableData14}", dto.CombinedCreditCardBalanceAvg);
                        htmlCode = htmlCode.Replace("{tableData15}", dto.CombinedCreditCardBalanceMin);
                        return htmlCode;
                        #endregion table created
            
            }
            catch(Exception ex) 
            {
                 return ex.Message;
            }
        }
        #endregion

        #region Convert Html To Pdf File using Puppeteer sharp
        public async Task<PdfResponse> ConvertHtmlToPdfByHtmlCode(string htmlCode,IBrowser browser)
        {
            PdfResponse pdf = new();
            try
            {  
                var page = await browser.NewPageAsync();

                await page.SetContentAsync(htmlCode);// Replace with your HTML file path

                var pdfOptions = new PdfOptions // Pdf options for page setup
                {
                    Format = PuppeteerSharp.Media.PaperFormat.A3, //for layout of the page
                    PrintBackground = true // for applying the html color on pdf
                };
                var pdfData = await page.PdfDataAsync(pdfOptions);

                await browser.CloseAsync();
                File.WriteAllBytes("PdfFile/Riki_Report.pdf", pdfData); // write file
                pdf.PdfByteArr = File.ReadAllBytes("PdfFile/Riki_Report.pdf"); // Read file for getting the byte.
                pdf.PdfMessage = "PDF Created Successfully";
                return pdf;
            }
            catch(Exception ex)
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
