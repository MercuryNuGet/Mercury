using System;

namespace Mercury
{
    public static class PassthoughActExtensions
    {
        /// <summary>
        ///     Skip the act and go straight to an assert on the sut
        /// </summary>
        /// <typeparam name="TSut"></typeparam>
        /// <param name="arranged"></param>
        /// <param name="assertAction"></param>
        /// <returns></returns>
        public static IPostAssertCaseBuilder<TSut> Assert<TSut>(
            this IArranged<TSut> arranged, Action<TSut> assertAction)
        {
            return arranged.Act(sut => sut).Assert(assertAction);
        }

        /// <summary>
        ///     Skip the act and go straight to an assert on the sut
        /// </summary>
        /// <typeparam name="TSut"></typeparam>
        /// <param name="arranged"></param>
        /// <param name="assertName"></param>
        /// <param name="assertAction"></param>
        /// <returns></returns>
        public static IPostAssertCaseBuilder<TSut> Assert<TSut>(
            this IArranged<TSut> arranged, string assertName, Action<TSut> assertAction)
        {
            return arranged.Act(sut => sut).Assert(assertName, assertAction);
        }

        /// <summary>
        ///     Skip the act and go straight to an assert on the sut
        /// </summary>
        /// <typeparam name="TSut"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="arranged"></param>
        /// <param name="assertAction"></param>
        /// <returns></returns>
        public static IPostAssertCaseBuilder<TSut, TData> Assert<TSut, TData>(
            this IArrangedWithData<TSut, TData> arranged, Action<TSut, TData> assertAction)
        {
            return arranged.Act((sut, data) => sut).Assert(assertAction);
        }

        /// <summary>
        ///     Skip the act and go straight to an assert on the sut
        /// </summary>
        /// <typeparam name="TSut"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="arranged"></param>
        /// <param name="assertName"></param>
        /// <param name="assertAction"></param>
        /// <returns></returns>
        public static IPostAssertCaseBuilder<TSut, TData> Assert<TSut, TData>(
            this IArrangedWithData<TSut, TData> arranged, string assertName, Action<TSut, TData> assertAction)
        {
            return arranged.Act((sut, data) => sut).Assert(assertName, assertAction);
        }
    }
}