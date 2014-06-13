using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects
{
    public class PropertyUpdater<T>
    {
        static Action<object> SetPropertyByName = GetSetPropertyByNameFunction();
        static Func<object, string> GetPropertyByName = GetGetPropertyByNameFunction();

        static SimpleClass MySimpleClass = GetSimpleClass();
        SimpleClass OtherClass = GetSimpleClass();

        private static SimpleClass GetSimpleClass()
        {
            return new SimpleClass();
        }





        private static Action<object> GetSetPropertyByNameFunction()
        {
            var type = typeof(T);

            var returnValueExpression = Expression.Variable(typeof(object), "returnValue");
            var instanceValueExpression = Expression.Parameter(type, "instance");

            var propertySwitchCases = type.GetProperties().Select(v => Expression.SwitchCase(
                    Expression.Assign(returnValueExpression, Expression.Property(instanceValueExpression, v.Name)),
                    Expression.Constant(v.Name)))
                    .ToArray();

            var propertyNameParameterExpression = Expression.Parameter(typeof(string), "propertyName");
            var switchExpression = Expression.Switch(
                propertyNameParameterExpression,
                Expression.Throw(Expression.New(typeof(Exception))),
                propertySwitchCases);


            return t => { };
        }


        private static Func<object, string> GetGetPropertyByNameFunction()
        {
            throw new NotImplementedException();
        }


    }
}
