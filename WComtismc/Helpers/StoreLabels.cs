using WComtismc.Models;

namespace WComtismc.Helpers;

public static class StoreLabels
{
    public static string GetPaymentLabel(string method) => method switch
    {
        "card" => "بطاقة ائتمان / مدى",
        "applepay" => "Apple Pay",
        _ => "الدفع عند الاستلام"
    };

    public static string GetStatusLabel(OrderStatus status) => status switch
    {
        OrderStatus.Confirmed => "مؤكد",
        OrderStatus.Shipped => "تم الشحن",
        OrderStatus.Delivered => "تم التسليم",
        _ => "قيد المعالجة"
    };
}
