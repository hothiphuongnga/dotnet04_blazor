### **Khởi tạo (Initialization)**

- **`OnInitialized` / `OnInitializedAsync`**
    - Gọi một lần khi component được khởi tạo lần đầu.
    - Thường dùng để chuẩn bị dữ liệu ban đầu (gọi API, set biến, đăng ký service).
    - `Async` thích hợp cho thao tác bất đồng bộ.
    
    ```csharp
    protected override async Task OnInitializedAsync()
    {
        data = await ApiService.GetDataAsync();
    }
    ```
    

### **Tham số thay đổi (Parameter Setting)**


- **`SetParametersAsync`**

    - Đây là **phương thức hệ thống được gọi trước khi OnParametersSet**, khi Blazor chuẩn bị gán các parameter cho component.
    - Thường chỉ override khi bạn muốn can thiệp thật sâu vào quá trình nhận parameter, ví dụ: custom logic hoặc kiểm tra đặc biệt.
    - Hiếm khi cần dùng trong ứng dụng Blazor thông thường.

    - **Ví dụ:**


        ```csharp
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
            // Custom logic khi nhận parameter (hiếm khi dùng)
        }
        ```

    - `ParameterView parameters` chứa **toàn bộ danh sách các parameter được truyền vào**.
- **`OnParametersSet` / `OnParametersSetAsync`**
    - Gọi khi Blazor gán hoặc cập nhật các `[Parameter]` từ parent component.
    - Gọi nhiều lần (mỗi khi param từ cha đổi).
    - Dùng để xử lý logic phụ thuộc vào parameter mới.
    
    ```csharp
    protected override void OnParametersSet()
    {
        Console.WriteLine($"Param value: {SomeParam}");
    }
    ```

    

### **Render UI (Rendering)**


- **`ShouldRender`**
    - Không chạy lần đầu
    - Quyết định component có cần render lại không (default: `true`).
    - Có thể override để tối ưu hiệu năng.
    
    ```csharp
    protected override bool ShouldRender()
    {
        return IsDataChanged;
    }
    ```
- **`OnAfterRender` / `OnAfterRenderAsync`**
    - Gọi sau khi component đã render UI ra DOM.
    - `firstRender` (bool) cho biết đây có phải lần render đầu tiên.
    - Dùng khi cần tương tác với DOM hoặc JS interop (JSRuntime).
    
    ```csharp
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("console.log", "First render done");
        }
    }
    ```
    

    

### **Hủy component (Disposal)**

- **`Dispose` / `IAsyncDisposable.DisposeAsync`**
    - Được gọi khi component bị loại bỏ khỏi UI.
    - Thường để giải phóng tài nguyên: hủy event handler, đóng kết nối, dispose `IDisposable` objects.
    
    ```csharp
    public void Dispose()
    {
        subscription?.Dispose();
    }
    ```



    