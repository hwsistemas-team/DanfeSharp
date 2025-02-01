using System;

namespace DanfeSharp.Elementos
{
    /// <summary>
    /// Linha de campos, posiciona e muda a largura desses elementos de forma proporcional.
    /// </summary>
    internal class LinhaCampos : FlexibleLine
    {
        public Estilo Estilo => Contexto.Estilo;
        internal new ElementoContexto Contexto => (ElementoContexto)base.Contexto;

        public LinhaCampos(ElementoContexto contexto, float width, float height = Constantes.CampoAltura) : base(contexto)
        {
            SetSize(width, height);
        }

        public LinhaCampos(ElementoContexto contexto) : base(contexto) {}

        public virtual LinhaCampos ComCampo(String cabecalho, String conteudo, AlinhamentoHorizontal alinhamentoHorizontalConteudo = AlinhamentoHorizontal.Esquerda)
        {
            var campo = new Campo(Contexto, cabecalho, conteudo, alinhamentoHorizontalConteudo);
            Elementos.Add(campo);
            return this;
        }

        public virtual LinhaCampos ComCampoNumerico(String cabecalho, double? conteudoNumerico, int casasDecimais = 2)
        {
            var campo = new CampoNumerico(Contexto, cabecalho, conteudoNumerico, casasDecimais);
            Elementos.Add(campo);
            return this;
        }
    }
}
