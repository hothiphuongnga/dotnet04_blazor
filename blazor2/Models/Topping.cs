public class Topping
{
    public string Ten { get; set; }
    public decimal Gia { get; set; }
    public int SoLuong { get; set; }
    
    // tính thành tiền của topping
    public decimal ThanhTien => Gia * SoLuong;
}