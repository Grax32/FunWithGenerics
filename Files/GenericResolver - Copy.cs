using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace TestProject1
{
    public static class GenericResolver<T>
        where T : class
    {
        static GenericResolver()
        {
            // eliminate magic string.  Store name of resolve function+
            Expression<Func<T>> resolverExpression = () => Resolve();
            var resolverMethodCallExpression = resolverExpression.Body as MethodCallExpression;
            resolverMethodName = resolverMethodCallExpression.Method.Name;

            // determine if type is interface or abstract.  If not, we can not instantiate it
            var type = typeof(T);
            IsInterfaceOrAbstract = type.IsInterface || type.IsAbstract;

            if (IsInterfaceOrAbstract)
            {
                // if we can not instantiate, set the resolver to throw an exception.
                // this resolver will be replaced when the type is configured
                SetResolver(() => ThrowInterfaceException());
            }
            else
            {
                // try to find the default constructor and create a default resolver from it
                SetDefaultConstructor();
            }
        }

        static readonly string resolverMethodName;
        static readonly List<SetterExpression> setterExpressions = new List<SetterExpression>();
        static Func<T> ResolverFactory { get; set; }
        static bool IsInterfaceOrAbstract { get; set; }
        static T ThrowInterfaceException()
        {
            throw new Exception(string.Format("Error on {0}. Unable to resolve Interface and Abstract classes without a configuration.", typeof(T).FullName));
        }

        class SetterExpression
        {
            public MemberExpression PropertyMemberExpression { get; set; }
            public LambdaExpression Setter { get; set; }
        }

        /// <summary>
        /// Expression to construct new instance of class
        /// </summary>
        public static Expression<Func<T>> ResolverFactoryExpression { get; private set; }

        /// <summary>
        /// Expression to construct new instance of class and set members or other operations
        /// </summary>
        public static Expression<Func<T>> ResolverExpression { get; private set; }

        public static void SetResolver(Expression<Func<T>> factoryExpression)
        {
            SetResolverInner(factoryExpression);
        }

        public static void SetResolver(ConstructorInfo constructor)
        {
            SetConstructor(constructor);
        }

        private static void SetResolverInner(Expression<Func<T>> factoryExpression)
        {
            ResolverFactoryExpression = factoryExpression;
            CompileResolver();
        }

        /// <summary>
        /// Add property setter for property, use the Resolver to determine the value of the property
        /// </summary>
        /// <typeparam name="TPropertyType"></typeparam>
        /// <param name="propertyExpression"></param>
        public static void AddPropertySetter<TPropertyType>(Expression<Func<T, TPropertyType>> propertyExpression)
            where TPropertyType : class
        {
            Expression<Func<TPropertyType>> setter = () => GenericResolver<TPropertyType>.Resolve();
            AddPropertySetterInner<TPropertyType>(propertyExpression, setter);
        }

        /// <summary>
        /// Add property setter for the property, compile and use the expression for the value of the property
        /// </summary>
        /// <typeparam name="TPropertyType"></typeparam>
        /// <param name="propertyExpression"></param>
        /// <param name="setter"></param>
        public static void AddPropertySetter<TPropertyType>(Expression<Func<T, TPropertyType>> propertyExpression, Expression<Func<TPropertyType>> setter)
        {
            AddPropertySetterInner<TPropertyType>(propertyExpression, setter);
        }

        private static void AddPropertySetterInner<TPropertyType>(Expression<Func<T, TPropertyType>> propertyExpression, Expression<Func<TPropertyType>> setter)
        {
            var propertyMemberExpression = propertyExpression.Body as MemberExpression;
            if (propertyMemberExpression == null)
            {
                throw new ArgumentException("Must contain a MemberExpression", "propertyExpression");
            }

            setterExpressions.Add(new SetterExpression { PropertyMemberExpression = propertyMemberExpression, Setter = setter });

            CompileResolver();
        }

        /// <summary>
        /// Compile the resolver expression
        /// If any setter expressions are used, build an expression that creates the object and then sets the properties before returning it,
        /// otherwise, use the simpler expression that just returns the object
        /// </summary>
        private static void CompileResolver()
        {
            // if no property expressions, then just use the unmodified ResolverFactoryExpression
            if (setterExpressions.Any())
            {
                var resolver = ResolverFactoryExpression;

                var variableExpression = Expression.Variable(typeof(T));
                var assignExpression = Expression.Assign(variableExpression, resolver.Body);

                // begin list of expressions for block expression
                var blockExpression = new List<Expression>();
                blockExpression.Add(assignExpression);

                // setters
                setterExpressions.ForEach(v =>
                    {
                        var propertyExpression = Expression.Property(variableExpression, (PropertyInfo)v.PropertyMemberExpression.Member);
                        var propertyAssignExpression = Expression.Assign(propertyExpression, v.Setter.Body);
                        blockExpression.Add(propertyAssignExpression);
                    });

                // return value
                blockExpression.Add(variableExpression);

                var expression = Expression.Block(new ParameterExpression[] { variableExpression }, blockExpression);

                ResolverExpression = (Expression<Func<T>>)Expression.Lambda(expression, resolver.Parameters);
            }
            else
            {
                ResolverExpression = ResolverFactoryExpression;
            }

            ResolverFactory = ResolverExpression.Compile();
        }

        // Resolve the concrete instance for this type
        public static T Resolve()
        {
            return ResolverFactory();
        }

        /// <summary>
        /// Get the constructor with the fewest number of parameters and create a factory for it
        /// </summary>
        private static void SetDefaultConstructor()
        {
            // get first available constructor ordered by parameter count ascending
            var constructor = typeof(T).GetConstructors().OrderBy(v => v.GetParameters().Count()).FirstOrDefault();

            if (constructor != null)
            {
                SetConstructor(constructor);
            }
        }

        /// <summary>
        /// Create an expression to create this type from the passed-in constructor
        /// </summary>
        /// <param name="constructor"></param>
        private static void SetConstructor(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();

            var arguments = new List<Expression>();
            foreach (var parameter in parameters)
            {
                var type = typeof(GenericResolver<>).MakeGenericType(parameter.ParameterType);
                var argument = Expression.Call(null, type.GetMethod(resolverMethodName));
                arguments.Add(argument);
            }

            Expression createInstanceExpression = Expression.New(constructor, arguments);

            SetResolverInner((Expression<Func<T>>)Expression.Lambda(createInstanceExpression));
        }
    }
}