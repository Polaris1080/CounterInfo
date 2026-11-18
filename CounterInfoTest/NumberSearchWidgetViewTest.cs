using CounterInfo.View;

namespace CounterInfoTest
{
    public class NumberSearchWidgetViewModelTest
    {
        private readonly NumberSearchWidgetViewModel _target = new();

        [Theory]
        [InlineData(    0, false)][InlineData(    1,  true)][InlineData(    2,  true)]
        [InlineData( 9998,  true)][InlineData( 9999,  true)][InlineData(10000, false)]
        public void ParseInt(int input, bool expected)
        {
            _target.Query = input.ToString();
            Assert.Equal(expected, _target.SearchButtonEnable);
        }
        [Theory]
        [InlineData("")][InlineData("１")][InlineData("一")]
        public void ParseString(string input)
        {
            _target.Query = input;
            Assert.False(_target.SearchButtonEnable);
        }
    }
}
