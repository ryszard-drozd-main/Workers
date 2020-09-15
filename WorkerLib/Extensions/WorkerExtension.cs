using System.Collections.Generic;
using System.Linq;
using WorkerLib.Model;

namespace WorkerLib.Extensions
{
    public static class WorkerExtension
    {
        public static bool AmITheBossOrHasTheBoss(this Worker worker)
        {
            if (worker == Worker.NoBoss)
                return false;
            if (worker == Worker.TheBoss)
                return true;
            return AmITheBossOrHasTheBoss(worker.MyBoss);
        }
        public static bool HasOrphants(this IEnumerable<Worker> workers)
        {
            return workers.Any(w => !w.AmITheBossOrHasTheBoss());
        }
    }
}
