using HelloWorldLibrary.BusinessLogic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using FluentAssertions;

namespace HelloWorldTests.BusinessLogic;

public class MessagesTests
{
    [Fact]
    public void Greeting_InEnglish()
    {
        // Arange
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);

        // Act
        String expected = "Hello World";
        String actual = messages.Greeting("en");

        // Assert
        expected.Should().BeEquivalentTo(actual);
    }

    [Fact]
    public void Greeting_InSpanish()
    {
        // Arrange
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);

        // Act
        String expected = "Hola Mundo";
        String actual = messages.Greeting("es");

        //Assert
        expected.Should().BeEquivalentTo(actual);
    }

    [Fact]
    public void Greeting_Invalid()
    {
        // Arrange
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);

        // Act
        Action action = () => messages.Greeting("fr");

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }
}

