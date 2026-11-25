namespace Library;

public class Member
{
    public string Id { get; }
    public string Name { get; private set; }
    public List<int> Borrowed { get; private set; } = new();
    public RecentlyViewed RecentlyViewed { get; private set; } = new();

    public Member(string id, string name)
    {
        Id = id;
        Name = name;
    }
}