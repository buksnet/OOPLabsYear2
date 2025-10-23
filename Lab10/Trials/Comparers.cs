namespace Trials
{
    public class NameComparer : IComparer<Trial>
    {
        public int Compare(Trial? x, Trial? y)
        {
            return string.Compare(x?.Name, y?.Name, StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Сравнение по длительности (в минутах) для семейства классов Trial
    /// </summary>
    public class DurationComparer : IComparer<Trial>
    {
        public int Compare(Trial? x, Trial? y)
        {
            if (x is null) return 0;
            if (y is null) return 0;

            return x.Duration - y.Duration;
        }
    }
}
