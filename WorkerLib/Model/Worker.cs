namespace WorkerLib.Model
{
    public class Worker
    {
        public static Worker TheBoss = new Worker();
        public static Worker NoBoss = new Worker();

        public Worker MyBoss { get; private set; } = NoBoss;

        public bool SetMyBoss(Worker boss)
        {
            if (AmITheBoss()
                || AmINullBoss()
                || CantBeBossForMyself(boss))
                return false;

            if (IsInCycle(this, boss))
                return false;

            MyBoss = boss;
            return true;
        }

        private bool IsInCycle(Worker worker, Worker boss)
        {
            if (IsTheBoss(boss)
                || IsNullBoss(boss))
                return false;

            if (worker == boss) // cycle
                return true;

            return IsInCycle(worker, boss.MyBoss);
        }

        private bool CantBeBossForMyself(Worker boss)
        {
            return this == boss;
        }

        private bool AmINullBoss()
        {
            return IsNullBoss(this);
        }
        private bool IsNullBoss(Worker worker)
        {
            return worker == NoBoss;
        }

        private bool AmITheBoss()
        {
            return IsTheBoss(this);
        }
        private bool IsTheBoss(Worker worker)
        {
            return worker == TheBoss;
        }
    }
}
