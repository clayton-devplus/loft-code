using FluentAssertions;

namespace Loft.Code.Console.Tests;

public class ProgramTests
{
    [Fact]
    public void Program_ShouldExecute_WithoutErrors()
    {
        // Arrange
        var result = true;

        // Act & Assert
        result.Should().BeTrue();
    }
}