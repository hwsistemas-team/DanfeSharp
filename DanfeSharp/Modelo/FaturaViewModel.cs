using System;

namespace DanfeSharp.Modelo
{
    public class FaturaViewModel
    {
        /// <summary>
        /// <para>Número da fatura</para>
        /// <para>Tag nFat</para>
        /// </summary>
        public String Numero { get; set; }

        /// <summary>
        /// <para>Valor original da fatura</para>
        /// <para>Tag vOrig</para>
        /// </summary>
        public Double ValorOriginal { get; set; }

        /// <summary>
        /// <para>Valor desconto da fatura</para>
        /// <para>Tag vOrig</para>
        /// </summary>
        public Double ValorDesconto { get; set; }

        /// <summary>
        /// <para>Valor liquido da fatura</para>
        /// <para>Tag vOrig</para>
        /// </summary>
        public Double ValorLiquido { get; set; }

    }
}
