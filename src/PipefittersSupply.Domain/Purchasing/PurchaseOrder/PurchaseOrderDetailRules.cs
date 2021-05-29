namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public static class PurchaseOrderDetailRules
    {
        public static bool HasNonNullPurchaseOrderID(this PurchaseOrderDetail purchaseOrderDetail)
            => purchaseOrderDetail.PurchaseOrderId != null;

        public static bool HasNonNullInventoryID(this PurchaseOrderDetail purchaseOrderDetail)
            => purchaseOrderDetail.InventoryId != null;

        public static bool IsValid(this PurchaseOrderDetail purchaseOrderDetail)
        {
            var valid = purchaseOrderDetail.Id != null &&
                purchaseOrderDetail.PurchaseOrderId != null &&
                purchaseOrderDetail.InventoryId != null;

            if (!valid)
            {
                throw new InvalidEntityStateException(purchaseOrderDetail, "Post-checks failed!");
            }

            return true;
        }

        public static void EnsureValidState(this PurchaseOrderDetail purchaseOrderDetail)
        {
            var valid = purchaseOrderDetail.Id != null &&
                purchaseOrderDetail.PurchaseOrderId != null &&
                purchaseOrderDetail.InventoryId != null;

            if (!valid)
            {
                throw new InvalidEntityStateException(purchaseOrderDetail, "Post-checks failed!");
            }
        }
    }
}