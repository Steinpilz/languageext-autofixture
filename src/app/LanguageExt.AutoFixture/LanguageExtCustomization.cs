using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace LanguageExt.AutoFixture
{
    public class LanguageExtCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register<OptionNone>(() => None);
            //var customization = new SupportMutableValueTypesCustomization();
            //customization.Customize(fixture);
            fixture.Customizations.Add(new Builder());
        }
    }

    class Builder : ISpecimenBuilder
    {
        static LanguageExt.HashSet<T> ToHashSet<T>(IEnumerable<T> source)
        {
            return Prelude.toHashSet(source.Distinct());
        }

        static LanguageExt.Either<L, R> ToEither<L, R>(R instance)
        {
            return (Either<L, R>)instance;
        }

        static LanguageExt.HashMap<L, R> ToHashMap<L, R>(IDictionary<L, R> dictionary)
        {
            return toHashMap(dictionary);
        }

        public object Create(object request, ISpecimenContext context)
        {
            var typedRequest = request as Type;
            if (typedRequest == null)
                return new NoSpecimen();

            return (
                TryCreateEither(typedRequest, context)
                || TryCreateHashSet(typedRequest, context)
                || TryCreateHashMap(typedRequest, context)
                )
                .IfNone(new NoSpecimen());
        }

        private Option<object> TryCreateEither(Type typedRequest, ISpecimenContext context)
        {
            // create either with Right state
            if (typedRequest.IsGenericType
                && typedRequest.GetGenericTypeDefinition() == typeof(Either<,>))
            {
                var leftArgType = typedRequest.GetGenericArguments()[0];
                var rightArgType = typedRequest.GetGenericArguments()[1];

                var instance = context.Resolve(rightArgType);

                // cast to either
                Expression<Func<LanguageExt.Either<int, int>>> expr = () => ToEither<int, int>(1);

                var methodCall = (MethodCallExpression)expr.Body;

                var method = methodCall.Method.GetGenericMethodDefinition();

                var genericMethod = method.MakeGenericMethod(leftArgType, rightArgType);
                return Some(genericMethod.Invoke(null, new object[] { instance }));
            }

            return None;
        }

        private Option<object> TryCreateHashSet(Type typedRequest, ISpecimenContext context)
        {
            // HashSet has no public constructor (IEnumerable<T>), so using Prelude.toHashSet function
            if (typedRequest.IsGenericType
                && typedRequest.GetGenericTypeDefinition() == typeof(LanguageExt.HashSet<>))
            {
                // create list

                var genericArg = typedRequest.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(genericArg);
                var listInstance = context.Resolve(listType);

                Expression<Func<LanguageExt.HashSet<int>>> expr = () => ToHashSet(new List<int>());

                var methodCall = (MethodCallExpression)expr.Body;

                var method = methodCall.Method.GetGenericMethodDefinition();

                var genericMethod = method.MakeGenericMethod(genericArg);
                var hashSet = genericMethod.Invoke(null, new object[] { listInstance });
                return Some(hashSet);
            }

            return None;
        }

        private Option<object> TryCreateHashMap(Type typedRequest, ISpecimenContext context)
        {
            // HashSet has no public constructor (IEnumerable<T>), so using Prelude.toHashSet function
            if (typedRequest.IsGenericType
                && typedRequest.GetGenericTypeDefinition() == typeof(LanguageExt.HashMap<,>))
            {
                // create list
                var genericArgs = typedRequest.GetGenericArguments();
                var dictionaryType = typeof(IDictionary<,>).MakeGenericType(genericArgs);
                var dictionaryInstance = context.Resolve(dictionaryType);

                Expression<Func<LanguageExt.HashMap<int, int>>> expr = () => ToHashMap(new Dictionary<int, int>());

                var methodCall = (MethodCallExpression)expr.Body;

                var method = methodCall.Method.GetGenericMethodDefinition();

                var genericMethod = method.MakeGenericMethod(genericArgs);
                var hashSet = genericMethod.Invoke(null, new object[] { dictionaryInstance });
                return Some(hashSet);
            }

            return None;
        }
    }
}
