namespace Demo.SharedModel.Events
{
    public class EventsConstants
    {
        //events names
        public const string EVENT_PRODUCT_WAS_INCLUDED = "PRODUCT_WAS_INCLUDED";
        public const string EVENT_PRODUCT_WAS_UPDATED = "PRODUCT_WAS_UPDATED";
        public const string EVENT_PRODUCT_WAS_EXCLUDED = "PRODUCT_WAS_EXCLUDED";
        public const string EVENT_CART_WAS_CHECKOUTED = "CART_WAS_CHECKOUTED";
        public const string EVENT_PAYMENT_WAS_APPROVED = "PAYMENT_WAS_APPROVED";
        public const string EVENT_PAYMENT_WAS_REJECTED = "PAYMENT_WAS_REJECTED";


        //endpoints names [events]
        public const string ENDPOINT_PRODUCT_STOCK_EVENTS = "demo.productstock.events";
        public const string ENDPOINT_PRODUCT_CATALOG_EVENTS = "demo.productscatalog.events";
        public const string ENDPOINT_PAYMENTS_EVENTS = "demo.payments.events";

        //endpoints names [notifications]
        public const string ENDPOINT_PRODUCT_CATALOG_RECEIVE_NOTIFICATIONS = "demo.productscatalog.stock_notifications";
        public const string ENDPOINT_PAYMENT_RECEIVE_NOTIFICATIONS = "demo.payment.catalog_notifications";

    }
}