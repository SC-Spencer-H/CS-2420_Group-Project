namespace Library;

public class MemberRegistry
{
    private static MemberRegistry instance;
    public static MemberRegistry Instance
    {
        get
        {
            if (instance == null)
                instance = new();
            return instance;
        }
    }
    private const int mapSize = 8;
    private RegistryNode[] Map { get; set; } = new RegistryNode[mapSize];

    private MemberRegistry() { }

    public static void AddMember(Member member)
    {
        int i = Hash(member.Id);

        RegistryNode node = Instance.Map[i];
        if (node == null)
        {
            Instance.Map[i] = new RegistryNode(member.Id, member);
            return;
        }

        while (node.Next != null)
            node = node.Next;

        node.Next = new RegistryNode(member.Id, member);
    }

    public static Member? Get(string id)
    {
        int i = Hash(id);

        RegistryNode node = Instance.Map[i];
        while (node != null)
        {
            if (node.Key == id)
                return node.Value;
            node = node.Next;
        }

        return null;
    }

    private static int Hash(string id)
    {
        int index = 0;

        foreach (char c in id)
            index += (int)c;

        index %= mapSize;

        return index;
    }

    private class RegistryNode(string key, Member value)
    {
        public string Key { get; set; } = key;
        public Member Value { get; set; } = value;
        public RegistryNode Next { get; set; }
    }
}