using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestObfuscator.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace RequestObfuscator.Tests
{
    [Binding]
    public class IEnumerableExtensionsSteps
    {
        private IEnumerable<object> _collection;
        private int _counter;
        private Action<object> _action;

        [Given(@"I have IEnumerable of (.*) object")]
        public void GivenIHaveIEnumerableOfObject(int quantity)
        {
            _counter = 0;
            _collection = Enumerable.Repeat(new object(), quantity);
        }
        
        [When(@"I invoke ForEach extension on this IEnumerable with any Action")]
        public void WhenIInvokeForEachExtensionOnThisIEnumerableWithAnyAction()
        {
            // quick workaround for no possibility to mock extension method with Moq
            _action = @object => { _counter++; };
            _collection.ForEach(_action);
        }
        
        [Then(@"the Action should be invoked (.*) times")]
        public void ThenTheActionShouldBeInvokedTimes(int expectedResult)
        {
            Assert.AreEqual(_counter, expectedResult);
        }
    }
}
