using Tadpole.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Tadpole.UnitTests;

public class SampleTests
{
    [Fact]
    public void Guardian_Defaults_AreValid()
    {
        var g = new Guardian { Email = "a@b.c", DisplayName = "A" };
        g.IsActive.Should().BeTrue();
        g.Children.Should().BeEmpty();
    }

    [Fact]
    public void Connection_CanonicalizeChildPair_OrdersByGuid()
    {
        var lo = Guid.Parse("00000000-0000-0000-0000-000000000001");
        var hi = Guid.Parse("00000000-0000-0000-0000-000000000002");

        Connection.CanonicalizeChildPair(lo, hi).Should().Be((lo, hi));
        Connection.CanonicalizeChildPair(hi, lo).Should().Be((lo, hi));
    }
}
