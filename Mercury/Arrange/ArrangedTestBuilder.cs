﻿using System;
using Mercury.AssertBuilder;

namespace Mercury.Arrange
{
    internal sealed class ArrangedTestBuilder<TSut> : IArranged<TSut>, ISuite
    {
        private readonly string _testName;
        private readonly Func<TSut> _arrangeFunc;

        public ArrangedTestBuilder(string testName, Func<TSut> arrangeFunc)
        {
            _testName = testName;
            _arrangeFunc = arrangeFunc;
        }

        public IAssertCaseBuilder<TResult> Act<TResult>(Func<TSut, TResult> actFunc)
        {
            return new PreAssertBuilder<TResult>(this, () => actFunc(_arrangeFunc()));
        }

        public IArrangedWithData<TSut, TData> With<TData>(TData data)
        {
            return new ArrangedDataBuilder<TSut, TData>(this, _arrangeFunc).With(data);
        }

        public string SuiteName
        {
            get { return _testName; }
        }
    }
}