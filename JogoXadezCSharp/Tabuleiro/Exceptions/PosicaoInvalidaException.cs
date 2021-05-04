using System;

namespace Tabuleiro.Exceptions
{
    class PosicaoInvalidaException: ApplicationException
    {

        public PosicaoInvalidaException(String msg) : base(msg)
        {

        }

    }
}
