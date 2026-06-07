using WComtismc.Models;

namespace WComtismc.Services;

public class ProductCatalog : IProductCatalog
{
    private readonly List<Product> _products =
    [
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111101"), NameAr = "أحمر شفاه مخملي", Name = "Velvet Matte Lipstick", Description = "أحمر شفاه طويل الأمد بتركيبة مخملية غنية باللون، يمنح شفاهك مظهراً أنيقاً يدوم طوال اليوم.", Category = "مكياج", Brand = "W Beauty", Price = 89, OldPrice = 120, ImageUrl = "https://images.unsplash.com/photo-1586495777744-4413d21021fa?w=400&h=400&fit=crop", Stock = 45, Rating = 4.8, IsFeatured = true, IsNew = false },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111102"), NameAr = "كريم أساس ساحر", Name = "Flawless Foundation", Description = "كريم أساس خفيف الوزن بتغطية متوسطة إلى كاملة، يناسب جميع أنواع البشرة ويمنح إشراقة طبيعية.", Category = "مكياج", Brand = "W Beauty", Price = 145, OldPrice = 180, ImageUrl = "https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=400&h=400&fit=crop", Stock = 30, Rating = 4.7, IsFeatured = true, IsNew = true },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111103"), NameAr = "ماسكارا مكثفة", Name = "Volume Boost Mascara", Description = "ماسكارا تمنح رموشك كثافة وطولاً استثنائياً دون تكتل، مقاومة للماء.", Category = "مكياج", Brand = "Lash Luxe", Price = 75, ImageUrl = "https://images.unsplash.com/photo-1631214524020-7e18db9a8f4f?w=400&h=400&fit=crop", Stock = 60, Rating = 4.6, IsFeatured = false, IsNew = true },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111104"), NameAr = "باليت ظلال العيون", Name = "Eyeshadow Palette", Description = "باليت من 12 لوناً متناسقاً بتركيبة ناعمة وسهلة التطبيق والدمج.", Category = "مكياج", Brand = "W Beauty", Price = 165, OldPrice = 210, ImageUrl = "https://images.unsplash.com/photo-1512496015851-a90fb38ba796?w=400&h=400&fit=crop", Stock = 25, Rating = 4.9, IsFeatured = true, IsNew = false },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111105"), NameAr = "سيروم فيتامين سي", Name = "Vitamin C Serum", Description = "سيروم مركز بفيتامين سي يفتح البشرة ويوحد لونها ويحميها من العوامل الخارجية.", Category = "العناية بالبشرة", Brand = "Glow Lab", Price = 195, OldPrice = 250, ImageUrl = "https://images.unsplash.com/photo-1620916565428-75f4c38f9b9c?w=400&h=400&fit=crop", Stock = 40, Rating = 4.9, IsFeatured = true, IsNew = true },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111106"), NameAr = "مرطب يومي فاخر", Name = "Daily Luxury Moisturizer", Description = "مرطب غني بالهيالورونيك أسيد يعيد ترطيب البشرة ويحافظ على نعومتها.", Category = "العناية بالبشرة", Brand = "Glow Lab", Price = 120, ImageUrl = "https://images.unsplash.com/photo-1556228578-0d95b1a5580a?w=400&h=400&fit=crop", Stock = 55, Rating = 4.5, IsFeatured = false, IsNew = false },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111107"), NameAr = "غسول وجه لطيف", Name = "Gentle Face Cleanser", Description = "غسول وجه لطيف يزيل الشوائب والمكياج دون تجفيف البشرة.", Category = "العناية بالبشرة", Brand = "Pure Skin", Price = 68, ImageUrl = "https://images.unsplash.com/photo-1570194065650-d99fb4d3b5c8?w=400&h=400&fit=crop", Stock = 70, Rating = 4.4, IsFeatured = false, IsNew = false },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111108"), NameAr = "عطر ورد فاخر", Name = "Rose Elegance Perfume", Description = "عطر نسائي فاخر بمزيج من الورد والياسمين والمسك الأبيض.", Category = "عطور", Brand = "W Parfum", Price = 320, OldPrice = 400, ImageUrl = "https://images.unsplash.com/photo-1541643600914-78b084683601?w=400&h=400&fit=crop", Stock = 20, Rating = 4.9, IsFeatured = true, IsNew = false },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111109"), NameAr = "عطر فانيليا دافئ", Name = "Warm Vanilla Mist", Description = "عطر خفيف برائحة الفانيليا الدافئة والعنبر، مثالي للاستخدام اليومي.", Category = "عطور", Brand = "W Parfum", Price = 185, ImageUrl = "https://images.unsplash.com/photo-1592945403244-b3fbafd7f539?w=400&h=400&fit=crop", Stock = 35, Rating = 4.6, IsFeatured = false, IsNew = true },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111110"), NameAr = "زيت أرغان للشعر", Name = "Argan Hair Oil", Description = "زيت أرغان مغربي أصلي يغذي الشعر ويمنحه لمعاناً حريرياً.", Category = "العناية بالشعر", Brand = "Silk Hair", Price = 95, OldPrice = 130, ImageUrl = "https://images.unsplash.com/photo-1608248543809-ba3f4c4d8f1b?w=400&h=400&fit=crop", Stock = 40, Rating = 4.7, IsFeatured = true, IsNew = false },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), NameAr = "شامبو الترطيب", Name = "Hydrating Shampoo", Description = "شامبو مرطب بخلاصة الألوفيرا ينظف الشعر بلطف ويحافظ على رطوبته.", Category = "العناية بالشعر", Brand = "Silk Hair", Price = 58, ImageUrl = "https://images.unsplash.com/photo-1535585208547-7f8f9d0484aa?w=400&h=400&fit=crop", Stock = 65, Rating = 4.3, IsFeatured = false, IsNew = false },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), NameAr = "طلاء أظافر لامع", Name = "Glossy Nail Polish", Description = "طلاء أظافر بلمعان عالٍ وسريع الجفاف، يدوم لأكثر من 7 أيام.", Category = "الأظافر", Brand = "Nail Art", Price = 42, ImageUrl = "https://images.unsplash.com/photo-1604654894610-df63bc536371?w=400&h=400&fit=crop", Stock = 80, Rating = 4.5, IsFeatured = false, IsNew = true },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111113"), NameAr = "مجموعة عناية الأظافر", Name = "Nail Care Set", Description = "مجموعة متكاملة للعناية بالأظافر تشمل مقص ومبرد وزيت تغذية.", Category = "الأظافر", Brand = "Nail Art", Price = 110, OldPrice = 145, ImageUrl = "https://images.unsplash.com/photo-1519014816548-bf789fe03ec4?w=400&h=400&fit=crop", Stock = 30, Rating = 4.6, IsFeatured = false, IsNew = false },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111114"), NameAr = "مثبت مكياج", Name = "Makeup Setting Spray", Description = "رذاذ يثبت المكياج ويمنح البشرة إشراقة طبيعية تدوم لساعات.", Category = "مكياج", Brand = "W Beauty", Price = 85, ImageUrl = "https://images.unsplash.com/photo-1522335789203-aabd1fc54bc9?w=400&h=400&fit=crop", Stock = 50, Rating = 4.7, IsFeatured = false, IsNew = true },
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111115"), NameAr = "ماسك الطين المنقي", Name = "Purifying Clay Mask", Description = "قناع طيني يمتص الزيوت وينقي المسام ويمنح البشرة نقاءً فورياً.", Category = "العناية بالبشرة", Brand = "Pure Skin", Price = 78, OldPrice = 99, ImageUrl = "https://images.unsplash.com/photo-1611934611353-3aaa2b9c5753?w=400&h=400&fit=crop", Stock = 45, Rating = 4.8, IsFeatured = true, IsNew = false }
    ];

    public IReadOnlyList<Product> GetAll() => _products;

    public IReadOnlyList<string> GetCategories() =>
        _products.Select(product => product.Category).Distinct().OrderBy(c => c).ToList();

    public Product? GetById(Guid id) => _products.FirstOrDefault(product => product.Id == id);

    public IReadOnlyList<Product> Search(string? query, string? category, string? sort)
    {
        var results = _products.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            results = results.Where(product => product.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var term = query.Trim();
            results = results.Where(product =>
                product.NameAr.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                product.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                product.Brand.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                product.Description.Contains(term, StringComparison.OrdinalIgnoreCase));
        }

        results = sort switch
        {
            "price-asc" => results.OrderBy(product => product.Price),
            "price-desc" => results.OrderByDescending(product => product.Price),
            "rating" => results.OrderByDescending(product => product.Rating),
            "newest" => results.OrderByDescending(product => product.IsNew).ThenByDescending(product => product.Id),
            _ => results.OrderByDescending(product => product.IsFeatured).ThenBy(product => product.NameAr)
        };

        return results.ToList();
    }
}
