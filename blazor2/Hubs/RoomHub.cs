using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

// Hub để quản lý các kết nối và tương tác realtime
// định nghĩa những phương thức mà client có thể gọi tới server / lắng nghe server
public class RoomHub : Hub
{
    private readonly RoomChatService _roomChatService;

    public RoomHub(RoomChatService roomChatService)
    {
        _roomChatService = roomChatService;
    }

    // khi client kết nối đến hub thì sẽ được nhận về ds phòng hiện có
    // method tên SendRoomList
    public async Task SendRoomList()
    {
        //ReceiveRoomList là tên sự kiên 
        Console.WriteLine("[SignalR] Client SEND ROOM: ");
        await Clients.All.SendAsync("ReceiveRoomList", _roomChatService.lsRoom);
        // guiwr cho 1 nguowi dùng cụ thể
    }



    // khi cliern kết nối đến hub hàm này sẽ chạy
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("[SignalR] Client connected: " + Context.ConnectionId);
        await base.OnConnectedAsync();
        await SendRoomList();
    }



    // AddRoom : tên method
    
    public async Task AddRoom()
    {

        // chỉ cần gọi dã viết ở state service
        // không cần viết logic nữa 
        Console.WriteLine("[SignalR] Client ADD ROOM: ");
        _roomChatService.AddRoom();
        await SendRoomList();
    }















    // khi client ngắt kết nối đến hub hàm này sẽ chạy
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine("[SignalR] Client disconnected: " + Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }




}