namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public enum CashTransactionType : int
    {
        CashReceiptSales = 1,
        CashReceiptDebtIssueProceeds = 2,
        CashReceiptStockIssueProceeds = 3,
        CashDisbursementLoanPayment = 4,
        CashDisbursementDividentPayment = 5,
        CashDisbursementTimeCardPayment = 6,
        CashDisbursementPurchaseReceipt = 7,
        CashReceiptAdjustment = 8,
        CashDisbursementAdjustment = 9
    }
}
