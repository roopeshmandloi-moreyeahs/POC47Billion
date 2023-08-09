namespace POC47Billion.Model
{
    public class PocModel
    {
        public class Address
        {
            public string street { get; set; } = string.Empty;
            public string street2 { get; set; } = string.Empty;
            public string city { get; set; } = string.Empty;
            public string region { get; set; } = string.Empty;
            public string postalCode { get; set; } = string.Empty;
            public string country { get; set; } = string.Empty;
        }

        public class CalendarMonthStatistic
        {
            public string? label { get; set; }
            public int monthsRequested { get; set; }
            public int monthsDelivered { get; set; }
            public string? lastDayOfLastFullMonthCovered { get; set; }
            public string? beginningOfFirstMonth { get; set; }
            public List<MonthlyStatistic>? monthlyStatistics { get; set; }
            public List<MonthlyAnalysis>? monthlyAnalysis { get; set; }
        }

        public class Consumer
        {
            public string? firstName { get; set; }
            public string? lastName { get; set; }
            public Identifier? identifiers { get; set; }
            public string? dateOfBirth { get; set; }
            public string? email { get; set; }
            public string? phoneNumber { get; set; }
            public string? associatedCustomerId { get; set; }
            public string? consumerId { get; set; }
        }


        public class GroupedAccountDatum
        {
            public string? accountNumber { get; set; }
            public string? accountType { get; set; }
            public string? externalAccountId { get; set; }
            public int currentBalance { get; set; }
            public DateTime currentBalanceDate { get; set; }
            public List<Transaction>? transactions { get; set; }
        }
        public class GroupedTransaction
        {
            public string? groupType { get; set; }
            public List<GroupedAccountDatum>? groupedAccountData { get; set; }
        }
        public class Identifier
        {
            public string? Source { get; set; }
            public string? Id { get; set; }
        }
        public class MonthlyAnalysis
        {
            public int monthlyDataCompleteness { get; set; }//in use
            public int avgDailySpending { get; set; }//in use
            public int incomeExpenseRatio { get; set; }//in use
            public int depletionDays { get; set; }
            public int monthlyAmountChangeScore { get; set; }
            public int monthlyAllocationChangeScore { get; set; }
            public int estimatedDiscretionarySpending { get; set; }
            public int unadjustedAvailableIncome { get; set; }
            public int adjustedAvailableIncome { get; set; }
            public int cashFlowIndexMonthly { get; set; }
            public int unadjustedCashFlowIndexMonthly { get; set; }
            public int prospectiveCashFlowIndexMonthly500 { get; set; }
        }
        public class MonthlyStatistic
        {
            public int daysInMonth { get; set; }
            public int daysInFullMonth { get; set; }
            public int accountsTracked { get; set; }
            public int incomeTransactions { get; set; }
            public int totalAmountIncomeTransactions { get; set; }
            public int expenseTransactions { get; set; }
            public int totalAmountExpenseTransactions { get; set; }
            public int transactionsNotAcctToAcctTransfers { get; set; }
            public int totalAmountTransactionsNotAcctToAcctTransfers { get; set; }
            public int largestSingleTransactionNotAcctToAcctTransfers { get; set; }
            public int bankAccountsVisible { get; set; }
            public int ddaAccountsVisible { get; set; }
            public int combinedBankAccountBalanceAvg { get; set; }
            public int combinedBankAccountBalanceMin { get; set; }
            public int combinedBankAccountBalanceMax { get; set; }
            public int creditCardAccountsVisible { get; set; }
            public int combinedCreditCardBalanceAvg { get; set; }
            public int combinedCreditCardBalanceMin { get; set; }
            public int combinedCreditCardBalanceMax { get; set; }
            public int creditCardCharges { get; set; }
            public int creditCardPayments { get; set; }
            public int totalAmountCreditCardCharges { get; set; }
            public int totalAmountCreditCardPayments { get; set; }
        }

        public class RecurrentItem
        {
            public string? groupType { get; set; }
            public string? description { get; set; }
            public int occurrences { get; set; }
            public int maxConsecutive { get; set; }
            public string? startDate { get; set; }
            public string? endDate { get; set; }
        }

        public class Remarks
        {
            public string? label { get; set; }
            public string? message { get; set; }
        }
        public class RikiData
        {
            public string? rikIasWords { get; set; }
            public double riki { get; set; }
            public double monthToMonthStabilityScore { get; set; }
            public double typicalMonthsTotalIncome { get; set; }
            public double typicalMonthsUnadjustedAvailableIncome { get; set; }
            public double typicalMonthsAdjustedAvailableIncome { get; set; }
            public double cashFlowIndex { get; set; }
            public double unadjustedCashFlowIndex { get; set; }
            public double unadjustedRIKI { get; set; }
            public double prospectiveRIKI_500 { get; set; }
            public double totalMonthlyIncomeTrend { get; set; }
            public double cashFlowIndexTrend { get; set; }
            public double ccTrend { get; set; }
            public double ccChangeTrend { get; set; }
            public double bankAccountChangeTrend { get; set; }
            public double bankAccountTrend { get; set; }

        }

        public class RikiResultSet
        {
            public string? rikiId { get; set; }
            public Consumer? consumer { get; set; }
            public RikiData? rikiData { get; set; }
            public List<Remarks>? remarks { get; set; }
            public RecurrentItem? recurrentItems { get; set; }
            public CalendarMonthStatistic? calendarMonthStatistics { get; set; }
            public Transaction? transactions { get; set; }
        }
        public class Root
        {
            public bool isSuccessful { get; set; }
            public string? status { get; set; }
            public DateTime timeOfRequest { get; set; }
            public List<string>? requestedReportIds { get; set; }
            public string? rikiId { get; set; }
            public RikiResultSet? rikiResultSet { get; set; }
        }
        public class Transaction
        {
            public string? accountId { get; set; }
            public string? externalTransactionId { get; set; }
            public DateTime? date { get; set; }
            public string? description { get; set; }
            public string? action { get; set; }
            public double amount { get; set; }
        }
        public class Data1
        {
            public object? JsonData { get; set; }
            public string? JwtToken { get; set; }
            public string? Bytarr { get; set; }
        }

    }
}
