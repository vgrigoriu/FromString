using System;
using System.Collections.Generic;

namespace FromString
{
    public delegate bool TryParse<T>(string rawValue, out T result);

    public class Parsed<T>
    {
        private readonly string rawValue;
        private readonly bool hasValue;
        private readonly T value;

        public Parsed(string rawValue)
        {
            this.rawValue = rawValue;

            var parser = ParsedHelper.GetParser<T>();
            hasValue = parser(rawValue, out value);
        }

        // should this be public?
        private Parsed(T value)
        {
            this.value = value;
            hasValue = true;
        } 

        public bool HasValue
        {
            get { return hasValue; }
        }

        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException("Value was not parsed");
                return value;
            }
        }

        public string RawValue
        {
            get { return rawValue; }
        }

        public override string ToString()
        {

            return HasValue ? Value.ToString() : RawValue;
        }

        public static implicit operator Parsed<T>(T value)
        {
            return new Parsed<T>(value);
        }

        public static void AddTryParse(TryParse<T> tryParse)
        {
            ParsedHelper.AddParser(tryParse);
        }
    }

    internal static class ParsedHelper
    {
        /// <summary>
        /// The dictionary is in a separate class because, in Parsed&lt;T>,
        /// there would have been one dictionary for each type parameter
        /// </summary>
        private static readonly Dictionary<Type, Delegate> Parsers = new Dictionary<Type, Delegate>();

        static ParsedHelper()
        {
            AddParser<int>(int.TryParse);
            AddParser((string s, out Uri uri) => Uri.TryCreate(s, UriKind.Absolute, out uri));
        }

        public static void AddParser<T>(TryParse<T> parser)
        {
            Parsers[typeof (T)] = parser;
        }

        public static TryParse<T> GetParser<T>()
        {
            if (Parsers.ContainsKey(typeof(T)))
                return (TryParse<T>)Parsers[typeof(T)];

            if (typeof(T).IsEnum)
            {
                TryParse<T> enumParser = (string s, out T enumValue) =>
                    {
                        try
                        {
                            enumValue = (T)Enum.Parse(typeof(T), s, ignoreCase: false);
                            return true;
                        }
                        catch (Exception)
                        {
                            enumValue = default(T);
                            return false;
                        }
                    };

                Parsers[typeof(T)] = enumParser;
                return enumParser;
            }

            throw new InvalidOperationException("Don't know how to parse " + typeof(T).Name);
        }
    }
}
