namespace CounterInfo.Shared;



public static class EnumerableForEachExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public void ForEach(Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(action);

            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
