using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunWithGenerics.Generics
{
    public class ExpressionLazy<TLazyType>
        where TLazyType : class
    {
        private Func<TLazyType> _creatorFunction;

        public ExpressionLazy(Func<TLazyType> creatorFunction)
        {
            _creatorFunction = creatorFunction;
        }

        TLazyType _value;
        public TLazyType Value
        {
            get
            {
                if (_value == null)
                {
                    _value = _creatorFunction();
                }

                return _value;
            }
        }
    }
}