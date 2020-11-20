
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;


namespace Riven.Extensions
{
    public class IsolationLevelExtensions_Test
    {
        [Fact]
        public void ToSystemDataIsolationLevel()
        {
            Assert.Equal(
                IsolationLevel.Serializable,
                System.Transactions.IsolationLevel.Serializable.ToSystemDataIsolationLevel()
                );
            Assert.Equal(
                IsolationLevel.ReadCommitted,
                System.Transactions.IsolationLevel.ReadCommitted.ToSystemDataIsolationLevel()
                );
            Assert.Equal(
                IsolationLevel.ReadUncommitted,
                System.Transactions.IsolationLevel.ReadUncommitted.ToSystemDataIsolationLevel()
                );
            Assert.Equal(
                IsolationLevel.RepeatableRead,
                System.Transactions.IsolationLevel.RepeatableRead.ToSystemDataIsolationLevel()
                );
            Assert.Equal(
                IsolationLevel.Serializable,
                System.Transactions.IsolationLevel.Serializable.ToSystemDataIsolationLevel()
                );
            Assert.Equal(
                IsolationLevel.Snapshot,
                System.Transactions.IsolationLevel.Snapshot.ToSystemDataIsolationLevel()
                );
            Assert.Equal(
              IsolationLevel.Unspecified,
              System.Transactions.IsolationLevel.Unspecified.ToSystemDataIsolationLevel()
              );

            Assert.Throws<Exception>(() =>
            {
                var i = -1;
                ((System.Transactions.IsolationLevel)i).ToSystemDataIsolationLevel();
            });
        }
    }
}
