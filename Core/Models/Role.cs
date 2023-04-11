namespace Core.Models;

public class Role
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<User>? Users { get; set; }

    public Role(){}
    public Role(string name)
    {
        Name = name;
    }
}