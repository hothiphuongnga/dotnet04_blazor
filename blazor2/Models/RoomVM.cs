public class RoomVM
{
    public int id { get; set; }
    public string roomName { get; set; }
    public RoomVM(int id)
    {
        this.id = id;
        roomName = "Phòng " + id;
        
    }
}