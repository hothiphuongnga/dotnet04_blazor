using System;

public class CounterService
{
    // 
    public int CurrentCounter = 0;

    // hàm để thay đổi giá trị của CurrentCounter
    public void TangCouter()
    {

        CurrentCounter++;
        NotifyStateChanged();
        // StateHasChanged();
    }
    public void GiamCounter()
    {

        CurrentCounter--;
        NotifyStateChanged();
    }

    // đăng ký sự kiện để thông báo khi giá trị thay đổi
    public event Action OnChange;
    // hàm để gọi sự kiện OnChange
    public void NotifyStateChanged() => OnChange?.Invoke();
}

// 