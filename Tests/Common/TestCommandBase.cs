using Infrastructure;
using System;

namespace Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;

        public TestCommandBase()
        {
            Context = ProjectContextFactory.Create();
        }

        public void Dispose()
        {
            ProjectContextFactory.Destroy(Context);
        }

    }
}
