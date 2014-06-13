using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraxGenerics
{
    public static class GenericSingleton<TInstanceType>
        where TInstanceType : new()
    {
        private static TInstanceType _instance = new TInstanceType();

        public static TInstanceType Instance
        {
            get { return _instance; }
        }
    }
}
