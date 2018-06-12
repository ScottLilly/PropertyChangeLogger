using PropertyChangeLogger;

namespace TestPropertyChangeLogger
{
    public class PostalCode : PropertyChangeLoggingClass
    {
        [DoNotLogChangesToThisProperty]
        public string Prefix { get; set; }
        [DoNotLogChangesToThisProperty]
        public string Suffix { get; set; }

        public string Value => string.IsNullOrWhiteSpace(Suffix) ? Prefix : $"{Prefix}-{Suffix}";

        public PostalCode(string prefix, string suffix)
        {
            Prefix = prefix;
            Suffix = suffix;

            SetInitialPropertyValues();
        }
    }
}