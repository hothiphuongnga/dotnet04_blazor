using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

public class CryptoService
{
    public CryptoService()
    {
    }


    // ds my crypto (d yêu thích)
    public List<CryptoData> MyCryptos { get; set; } = new List<CryptoData>();


    // thêm crypto vào ds yêu thích
    public void AddToMyCryptos(CryptoData crypto)
    {
        if (!MyCryptos.Exists(c => c.Name == crypto.Name))
        {
            MyCryptos.Add(crypto);
            NotifyStateChanged();
        }
    }
    // xóa crypto khỏi ds yêu thích
    public void RemoveFromMyCryptos(CryptoData crypto)
    {
        MyCryptos.RemoveAll(c => c.Name == crypto.Name);
        NotifyStateChanged();
    }

    public event Action OnChange;
    private void NotifyStateChanged() => OnChange?.Invoke();

}