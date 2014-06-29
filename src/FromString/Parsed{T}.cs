using System;
using System.Collections.Generic;

namespace FromString
{
    public delegate bool TryParse<T>(string rawValue, out T result);

    public class Parsed<T>
    {

        private readonly bool hasValue;
        private readonly T value;

        public Parsed(string rawValue)
        {
            var parser = (TryParse<T>)ParsedHelper.Parsers[typeof(T)];
            hasValue = parser(rawValue, out value);
        }

        public bool HasValue
        {
            get { return hasValue; }
        }

        public T Value
        {
            get { return value; }
        }
    }

    internal static class ParsedHelper
    {
        /// <summary>
        /// The dictionary is in a separate class because, in Parsed&lt;T>,
        /// there would have been one dictionary for each type parameter
        /// </summary>
        public static readonly Dictionary<Type, Delegate> Parsers = new Dictionary<Type, Delegate>();

        static ParsedHelper()
        {
            AddParser<int>(int.TryParse);
        }

        public static void AddParser<T>(TryParse<T> parser)
        {
            Parsers[typeof (T)] = parser;
        }
    }
}
