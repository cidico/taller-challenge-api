#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace TallerAPI.Core.Entities;

public class User
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Name { get; set; }
}