using Library;
namespace Tests;

public class MemberRegistryTests
{
    [Fact]
    public void AddMember()
    {
        Member member = new("M1", "Spencer");
        MemberRegistry.AddMember(member);
        Assert.Equal(member, MemberRegistry.Get("M1"));
    }
}
