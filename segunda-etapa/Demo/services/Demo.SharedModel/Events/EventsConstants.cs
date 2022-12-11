namespace Demo.SharedModel.Events
{
    public class EventsConstants
    {
        //events names
        public const string EVENT_PRODUCT_WAS_INCLUDED = "PRODUCT_WAS_INCLUDED";
        public const string EVENT_PRODUCT_WAS_UPDATED = "PRODUCT_WAS_UPDATED";
        public const string EVENT_PRODUCT_WAS_EXCLUDED = "PRODUCT_WAS_EXCLUDED";


        //endpoints names
        public const string ENDPOINT_PRODUCT_STOCK_EVENTS = "demo.productstock.events";
        public const string ENDPOINT_PRODUCT_CATALOG_RECEIVE_NOTIFICATIONS = "demo.productscatalog.stock_notifications";
    }
}