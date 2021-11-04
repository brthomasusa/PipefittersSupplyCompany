namespace PipefittersSupplyCompany.Core.Shared
{
    public enum EventType : int
    {
        CashReceiptFromSales = 1,
        CashReceiptFromLoanAgreement = 2,
        CashReceiptFromStockSubscription = 3,
        CashDisbursementForLoanPayment = 4,
        CashDisbursementForDividentPayment = 5,
        CashDisbursementForTimeCardPayment = 6,
        CashDisbursementForInventoryReceipt = 7
    }
}