// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury.AssertBuilder
{
	internal sealed class PreAssertBuilder<TSut> : IAssertCaseBuilder<TSut>
    {
        private readonly Func<TSut> _actFunc;
        private readonly ISuite _suite;

        public PreAssertBuilder(ISuite suite, Func<TSut> actFunc)
        {
            _actFunc = actFunc;
            _suite = suite;
        }

        public IPostAssertCaseBuilder<TSut> Assert(Action<TSut> assertAction)
        {
            return new AssertBuilder<TSut>(_actFunc, _suite).Assert(assertAction);
        }

        public IPostAssertCaseBuilder<TSut> Assert(string assertionTestCaseName,
            Action<TSut> assertAction)
        {
            return new AssertBuilder<TSut>(_actFunc, _suite).Assert(assertionTestCaseName,
                assertAction);
        }
    }
}
