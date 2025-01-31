using System;

namespace DanfeSharp.Modelo
{
    public class PagamentoViewModel
    {
        /// <summary>
        /// <para>Indicador de pagamento</para>
        /// <para>Tag indPag</para>
        /// </summary>
        public String TipoPagamento { get; set; }

        /// <summary>
        /// <para>Forma de pagamento</para>
        /// <para>Tag tPag</para>
        /// </summary>
        public String FormaPagamento { get; set; }

        /// <summary>
        /// <para>Valor do pagamento</para>
        /// <para>Tag vPag</para>
        /// </summary>
        public Double? Valor { get; set; }
    }
}
