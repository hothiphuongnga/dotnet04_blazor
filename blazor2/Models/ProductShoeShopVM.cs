using System.ComponentModel.DataAnnotations;

public class ProductShoeShopVM
{
    public int[] sizes { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public string alias { get; set; }
    public double price { get; set; }
    public string description { get; set; }
    public string size { get; set; }
    public string shortDescription { get; set; }
    public long quantity { get; set; }
    public bool deleted { get; set; }
    public string categories { get; set; }
    public string relatedProducts { get; set; }
    public bool feature { get; set; }
    public string image { get; set; }
    public string imgLink { get; set; }

}



// tạo file mới hoặc viết chung đều được
public class ProductDetailShoeShopVM
{
    public int id { get; set; }
    public string name { get; set; }
    public string alias { get; set; }
    public double price { get; set; }
    public bool feature { get; set; }
    public string description { get; set; }
    public string[] size { get; set; }
    public string shortDescription { get; set; }
    public int quantity { get; set; }
    public string[] detaildetailedImages { get; set; }
    public string image { get; set; }
    public string imgLink { get; set; }
    public Category[] categories { get; set; }
    public RelatedProduct[] relatedProducts { get; set; }
}
public partial class Category
{
    public string id { get; set; }
    public string category { get; set; }
}

public partial class RelatedProduct
{
    public int id { get; set; }
    public string name { get; set; }
    public string alias { get; set; }
    public bool feature { get; set; }
    public double price { get; set; }
    public string description { get; set; }
    public string shortDescription { get; set; }
    public string image { get; set; }
    public string imgLink { get; set; }
}

// THÊM MỚI
public class AddShoeApiVM
{
    public int id { get; set; } = 0;
    [Required(ErrorMessage = "Tên không được để trống")]
    public string name { get; set; }
    [Required(ErrorMessage = "Giá không được để trống")]
    public double price { get; set; }
    [Required(ErrorMessage = "Mô tả không được để trống")]
    public string description { get; set; }
    [Required(ErrorMessage = "Mô tả ngắn không được để trống")]
    public string shortDescription { get; set; }
    [Required(ErrorMessage = "Số lượng không được để trống")]
    public int quantity { get; set; }
    [Required(ErrorMessage = "Link ảnh không được để trống")]

    public string imgLink { get; set; }
}