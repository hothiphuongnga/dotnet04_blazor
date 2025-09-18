using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ShoeShopStateService
{
    private readonly HttpClient _http;


    // dùng factory
    private readonly IHttpClientFactory _httpClientFactory;
    // public ShoeShopStateService(HttpClient http)
    // {
    //     _http = http;
    // }
    public ShoeShopStateService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _http = _httpClientFactory.CreateClient("ShoeShopApi");
        // gán lại cái khác
    }
    // DS Sp
    public List<ProductShoeShopVM> lsProduct = new List<ProductShoeShopVM>();

    // sp chi tiết
    public ProductDetailShoeShopVM productDetail = new ProductDetailShoeShopVM();

    public event Action OnChange;
    private void NotifyStateChanged() => OnChange?.Invoke();


    //viết function getdata
    public async Task GetAllData()
    {
        var data = await _http.GetFromJsonAsync<ResponseEntity<List<ProductShoeShopVM>>>("api/Product");
        lsProduct = data.content;
        NotifyStateChanged();

    }
    // laasy sp chi tieets

    public async Task GetProductById(int id)
    {
        var data = await _http.GetFromJsonAsync<ResponseEntity<ProductDetailShoeShopVM>>($"api/Product/getbyid?id={id}");
        productDetail = data.content;
        NotifyStateChanged();
    }

    // THÊM  MỚI

    public async Task<string> AddNewShoe(AddShoeApiVM newShoe)
    {
        var response = await _http.PostAsJsonAsync("api/Product/addNew", newShoe);
        if (response.IsSuccessStatusCode)
        {
            // Nếu thêm mới thành công, bạn có thể làm gì đó ở đây
            // lấy nội dung từ phản hồi của api
            var responseContent = await response.Content.ReadFromJsonAsync<ResponseEntity<string>>(); // có await hoặc .Result
                                                                                                      //  Console.WriteLine("Thêm mới giày thành công: " +
                                                                                                      // Console.WriteLine(response.Content.ReadFromJsonAsync<ResponseEntity<string>>().Result.content);
            Console.WriteLine("Thêm mới giày thành công: " + responseContent.content);
            return responseContent.content;
        }
        else
        {
            // Xử lý lỗi nếu cần
            Console.WriteLine("Lỗi khi thêm mới giày: " + response.ReasonPhrase);
            return "Lỗi khi thêm mới giày: " + response.ReasonPhrase;
        }

    }


    // DELETE
    public async Task<string> DeleteShoe(int id)
    {
        var response = await _http.DeleteAsync($"api/Product/{id}");
        if(response.IsSuccessStatusCode)
        {

            // getall lại
            await GetAllData();
            NotifyStateChanged();
            var responseContent = await response.Content.ReadFromJsonAsync<ResponseEntity<string>>();
            Console.WriteLine("Xóa giày thành công: " + responseContent.content);
            return responseContent.content;
        }
        else
        {
            Console.WriteLine("Lỗi khi xóa giày: " + response.ReasonPhrase);
            return "Lỗi khi xóa giày: " + response.ReasonPhrase;
        }
    }
}
