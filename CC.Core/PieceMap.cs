using System;
using System.Collections.Concurrent;

namespace CC.Core
{
    [Serializable]
    public class PieceMap<TK, TV> : ConcurrentDictionary<TK, TV>
    {
        protected TV DefaultValue;

        public PieceMap(TV defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public TV Get(TK k)
        {
            if (ContainsKey(k) && TryGetValue(k, out TV result))
                return result;
            return DefaultValue;
        }
    }
}