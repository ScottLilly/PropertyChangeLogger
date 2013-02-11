using System;

using PropertyChangeLogger;

namespace TestPropertyChangeLogger
{
    public class PostalCode : PropertyChangeLoggingClass
    {
        [DoNotLogChangesToThisProperty]
        public string Prefix { get; set; }
        [DoNotLogChangesToThisProperty]
        public string Suffix { get; set; }

        public string Value
        {
            get
            {
                if(String.IsNullOrWhiteSpace(Suffix))
                {
                    return Prefix;
                }
                else
                {
                    return string.Format("{0}-{1}", Prefix, Suffix);
                }
            }
        }

        public PostalCode(string prefix, string suffix)
        {
            Prefix = prefix;
            Suffix = suffix;

            SetInitialPropertyValues();
        }
    }
}
