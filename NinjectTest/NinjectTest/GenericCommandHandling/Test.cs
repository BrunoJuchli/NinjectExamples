using System;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace NinjectTest.GenericCommandHandling
{
    public class Test
    {
        [Fact]
        public void Foo()
        {
            Type commandType = typeof(ICommand);
            object command = new object();

            ICommandDispatcher _commandDispatcher = null;
            MethodInfo dispatchMethod = GetMethod<ICommand>(c => _commandDispatcher.Dispatch(c))
                .GetGenericMethodDefinition()
                .MakeGenericMethod(commandType);

            RequestStatus result = (RequestStatus)dispatchMethod.Invoke(
                _commandDispatcher,
                new object[] { command });
        }

        public static MethodInfo GetMethod<T1>(Expression<Action<T1>> methodSelector)
        {
            return GetMethodInfo(methodSelector);
        }

        private static MethodInfo GetMethodInfo(LambdaExpression methodSelector)
        {
            if (methodSelector == null)
            {
                throw new ArgumentNullException("methodSelector");
            }
            if (methodSelector.Body.NodeType != ExpressionType.Call)
            {
                throw new ArgumentOutOfRangeException(
                    "methodSelector",
                    "Specified expression does is not a method call expression.");
            }

            var callExpression = (MethodCallExpression)methodSelector.Body;
            return callExpression.Method;
        }
    }
}