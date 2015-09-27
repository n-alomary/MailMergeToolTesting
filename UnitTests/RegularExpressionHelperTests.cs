namespace UnitTests
{
    using FluentAssertions;
    using MailMergeTool;
    using Xunit;

    public class When_I_replace_pattern_in_string
    {
        [Fact]
        public void It_should_replace_pattern_and_return_expected_value()
        {
            var helper = new RegularExpressionHelper();
            var actual = helper.Replace("Hello <<Address>>", "World", "<<Address>>");

            actual.Should().Be("Hello World");
        }
    }
}
