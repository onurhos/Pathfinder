using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using EventFlow.EventStores;
using Moq;

namespace PathFinder.Tests.Helpers
{
    public abstract class Test
    {
        protected IFixture Fixture { get; set; }
        protected IDomainEventFactory DomainEventFactory;

        protected Test()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
            DomainEventFactory = new DomainEventFactory();
        }

        protected T A<T>()
        {
            return Fixture.Create<T>();
        }

        protected List<T> Many<T>(int count = 3)
        {
            return Fixture.CreateMany<T>(count).ToList();
        }

        protected T Mock<T>()
            where T : class
        {
            return new Mock<T>().Object;
        }

        protected T Inject<T>(T instance)
            where T : class
        {
            Fixture.Inject(instance);
            return instance;
        }

        protected Mock<T> InjectMock<T>(params object[] args)
            where T : class
        {
            var mock = new Mock<T>(args);
            Fixture.Inject(mock.Object);
            return mock;
        }
        
        protected Mock<Func<T>> CreateFailingFunction<T>(T result, params Exception[] exceptions)
        {
            var function = new Mock<Func<T>>();
            var exceptionStack = new Stack<Exception>(exceptions.Reverse());
            function
                .Setup(f => f())
                .Returns(() =>
                {
                    if (exceptionStack.Any())
                    {
                        throw exceptionStack.Pop();
                    }
                    return result;
                });
            return function;
        }
    }
}