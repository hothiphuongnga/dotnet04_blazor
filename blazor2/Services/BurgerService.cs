// quản lý state
// ds topping
using System;
using System.Collections.Generic;

public class BurgerService
{
    public List<Topping> Toppings { get; set; } = new List<Topping>()
    {
        new Topping() { Ten = "cheese", Gia = 10000, SoLuong = 2 },
        new Topping() { Ten = "beef", Gia = 20000, SoLuong = 3 },
        new Topping() { Ten = "salad", Gia = 5000, SoLuong = 1 }
    };

    // hàm thay đổi giá trị của topping 
    // hàm thây đổi soluong topping ( nhận vào tên và số lượn cần thay đổi)

    // vd hiện 5 salad -: nhận vào +1 => 5+1 = 6
    // vd hiện 5 salad -: nhận vào -1 => 5 + (-1) =
    public void ThayDoiSoLuong(string ten, int soLuong)
    {
        var topping = Toppings.Find(t => t.Ten == ten);
        if (topping != null)
        {
            // thay đổi số lượng
            if (topping.SoLuong == 0 && soLuong < 0)
            {
                return;
            }
            
            topping.SoLuong += soLuong;
            NotifyStateChanged();
        }

    }
    // tính tổng tiền
    public decimal TongTien()
    {
        decimal tong = 0;
        foreach (var t in Toppings)
        {
            tong += t.ThanhTien;
        }
        return tong;
    }
    public event Action OnChange;
    
    private void NotifyStateChanged() => OnChange?.Invoke();

    


}