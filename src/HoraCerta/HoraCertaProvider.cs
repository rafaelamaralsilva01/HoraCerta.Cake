using System;

namespace HoraCerta
{
    public class HoraCertaProvider : IHoraCertaProvider
    {
        private readonly Now now;
        private readonly NowUtc nowUtc;

        public HoraCertaProvider()
        {
        }
        
        public HoraCertaProvider(Now now, NowUtc nowUtc)
        {
            this.now = now;
            this.nowUtc = nowUtc;
        }

        public virtual DateTime Now()
        {
            return now?.Invoke() ?? DateTime.Now;
        }

        public virtual DateTime NowUtc()
        {
            return nowUtc?.Invoke() ?? DateTime.UtcNow;
        }
    }
}