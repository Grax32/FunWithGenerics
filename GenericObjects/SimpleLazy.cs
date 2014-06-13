using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunWithGenerics.Generics
{
    public class SimpleLazy<TLazyType>
        where TLazyType : class, new()
    {
        TLazyType _value;

        public TLazyType Value
        {
            get
            {
                return _value ?? (_value = new TLazyType());
            }
        }
    }
}
