using System;

namespace HoraCerta
{
    public interface IHoraCertaProvider
    {
        DateTime Now();

        DateTime NowUtc();
    }
}
