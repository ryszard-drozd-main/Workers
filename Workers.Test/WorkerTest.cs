using Xunit;
using System.Collections.Generic;
using WorkerLib.Model;
using WorkerLib.Extensions;

namespace Workers.Test
{
    public class WorkerTest
    {
        [Fact]
        public void NewWorkerHasNoBossAsBoss()
        {
            var sut = new Worker();
            Assert.True(sut.MyBoss == Worker.NoBoss);
        }
        [Fact]
        public void SetNoBossAsBoss()
        {
            var sut = new Worker();
            Assert.True(sut.SetMyBoss(Worker.NoBoss));
            Assert.True(sut.MyBoss == Worker.NoBoss);
        }
        [Fact]
        public void CantBeBossForMyself()
        {
            var sut = new Worker();
            Assert.False(sut.SetMyBoss(sut));
        }
        [Fact]
        public void CantHaveSmallCycle()
        {
            var sut01 = new Worker();
            var sut02 = new Worker();
            Assert.True(sut02.SetMyBoss(sut01));
            Assert.False(sut01.SetMyBoss(sut02));
        }
        [Fact]
        public void CantHaveMediumCycle()
        {
            var sut01 = new Worker();
            var sut02 = new Worker();
            var sut03 = new Worker();
            Assert.True(sut03.SetMyBoss(sut02));
            Assert.True(sut02.SetMyBoss(sut01));
            Assert.False(sut01.SetMyBoss(sut03));
        }
        [Fact]
        public void CantHaveBigCycle()
        {
            var sut01 = new Worker();
            var sut02 = new Worker();
            var sut03 = new Worker();
            var sut04 = new Worker();
            var sut05 = new Worker();
            var sut06 = new Worker();
            Assert.True(sut06.SetMyBoss(sut05));
            Assert.True(sut05.SetMyBoss(sut04));
            Assert.True(sut04.SetMyBoss(sut03));
            Assert.True(sut03.SetMyBoss(sut02));
            Assert.True(sut02.SetMyBoss(sut01));
            Assert.False(sut01.SetMyBoss(sut06));
        }
        [Fact]
        public void DeepTree()
        {
            var sut01 = new Worker();
            var sut02 = new Worker();
            var sut03 = new Worker();
            Assert.True(sut03.SetMyBoss(sut02));
            Assert.True(sut02.SetMyBoss(sut01));
            Assert.True(sut01.SetMyBoss(Worker.TheBoss));
        }
        [Fact]
        public void SingleWorker()
        {
            var workers = new List<Worker> { new Worker() };
            Assert.True(workers.HasOrphants());
        }
        [Fact]
        public void SingleWorkerWithTheBoss()
        {
            var sut01 = new Worker();
            sut01.SetMyBoss(Worker.TheBoss);
            var workers = new List<Worker> { sut01 };
            Assert.False(workers.HasOrphants());
        }
        [Fact]
        public void TreeOfWorkersWithTheBoss()
        {
            var sut01 = new Worker();
            var sut02 = new Worker();
            var sut03 = new Worker();
            Assert.True(sut03.SetMyBoss(sut01));
            Assert.True(sut02.SetMyBoss(sut01));
            Assert.True(sut01.SetMyBoss(Worker.TheBoss));
            var workers = new List<Worker> { sut01, sut02, sut03 };
            Assert.False(workers.HasOrphants());
        }
        [Fact]
        public void OrphanedWorkers()
        {
            var sut01 = new Worker();
            var sut02 = new Worker();
            var sut03 = new Worker();
            Assert.True(sut03.SetMyBoss(sut01));
            Assert.True(sut02.SetMyBoss(sut01));
            var workers = new List<Worker> { sut01, sut02, sut03 };
            Assert.True(workers.HasOrphants());
        }
    }
}
