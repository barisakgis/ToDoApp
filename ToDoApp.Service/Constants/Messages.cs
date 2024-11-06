namespace ToDoApp.Service.Constants;

public static class Messages
{ 

    public const string ToDoAddedMessage = "ToDo Eklendi";
    public const string ToDoUpdatedMessage = "ToDo Güncellendi";
    public const string ToDoDeletedMessage = "ToDo Silindi.";
    public static string ToDoIsNotPresentMessage(Guid id)
    {
        return $"İlgili id ye göre ToDo bulunamadı : {id}";
    }

}
