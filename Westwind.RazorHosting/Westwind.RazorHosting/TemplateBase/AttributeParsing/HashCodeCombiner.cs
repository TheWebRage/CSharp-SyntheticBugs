﻿// From ASP.Net Web Stack [http://aspnetwebstack.codeplex.com/]
// Used under the Apache License

using System.Collections;

namespace Westwind.RazorHosting
{
    internal class HashCodeCombiner
    {
        private long _combinedHash64 = 0x1505L;

        public int CombinedHash
        {
            get { return _combinedHash64.GetHashCode(); }
        }

        public HashCodeCombiner Add(IEnumerable e)
        {
            if (e == null)
            {
                Add(0);
            }
            else
            {
                int count = 0;
                foreach (object o in e)
                {
                    Add(o);
                    count++;
                }
                Add(count);
            }
            return this;
        }

        public HashCodeCombiner Add(int i)
        {
            _combinedHash64 = ((_combinedHash64 << 5) + _combinedHash64) ^ i;
            return this;
        }

        public HashCodeCombiner Add(object o)
        {
            int hashCode = (o != null) ? o.GetHashCode() : 0;
            Add(hashCode);
            return this;
        }

        public static HashCodeCombiner Start()
        {
            return new HashCodeCombiner();
        }
    }
}