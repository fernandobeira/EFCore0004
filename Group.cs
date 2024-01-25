namespace EFCore0004;

public class Group
{
public int Id {get; set;}
public required string Name { get; set; }
public ICollection<User> Users { get; set; } = new List<User>();
}
