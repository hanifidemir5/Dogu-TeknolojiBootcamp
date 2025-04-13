namespace BlogApp.Entity;

public class Category{
    public int CategoryId{get;set;}
    public string? Name {get;set;}
    public string? Url {get;set;}
    public bool IsActive { get; set; } = true;
    public List<Post>? Posts { get; set; } = new List<Post>();
}