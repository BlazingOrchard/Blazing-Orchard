using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Descriptors
{
    public class PlacementInfo
    {
        private static readonly char[] Delimiters = { ':', '#', '@', '%', '|' };

        public string Location { get; set; } = default!;
        public string Source { get; set; } = default!;
        public string ShapeType { get; set; } = default!;
        public string? DefaultPosition { get; set; }
        public AlternatesCollection Alternates { get; set; } = new AlternatesCollection();
        public AlternatesCollection Wrappers { get; set; } = new AlternatesCollection();

        /// <summary>
        /// Returns the list of zone names.
        /// e.g., <code>Content.Metadata:1</code> will return 'Content', 'Metadata'
        /// </summary>
        public string[] GetZones()
        {
            var location = Location;
            var firstDelimiter = location.IndexOfAny(Delimiters);
            var zones = firstDelimiter == -1 ? location : location.Substring(0, firstDelimiter);

            return zones.Split('.');
        }

        public string GetPosition()
        {
            var contentDelimiter = Location.IndexOf(':');
            
            if (contentDelimiter == -1)
                return DefaultPosition ?? "";

            var secondDelimiter = Location.IndexOfAny(Delimiters, contentDelimiter + 1);
            
            if (secondDelimiter == -1)
                return Location.Substring(contentDelimiter + 1);

            return Location.Substring(contentDelimiter + 1, secondDelimiter - contentDelimiter - 1);
        }
    }
}