using System;
using DanfeSharp.Graphics;
using DanfeSharp.Elementos;
using DanfeSharp.Modelo;

namespace DanfeSharp.Blocos
{
    /// <summary>
    /// Define um bloco básico do DANFE.
    /// </summary>
    internal abstract class BlocoBase : ElementoBase
    {
        /// <summary>
        /// Constante de proporção dos campos para o formato retrato A4, porcentagem dividida pela largura desenhável.
        /// </summary>
        public const float Proporcao = 100F / 200F;

        public DanfeViewModel ViewModel => Contexto.ViewModel;

        public abstract PosicaoBloco Posicao { get; }

        /// <summary>
        /// Pilha principal.
        /// </summary>
        public VerticalStack MainVerticalStack { get; private set; }

        /// <summary>
        /// Quando verdadeiro, o bloco é mostrado apenas na primeira página, caso contário é mostrado em todas elas.
        /// </summary>
        public virtual Boolean VisivelSomentePrimeiraPagina => true;

        public virtual String Cabecalho => null;

        internal new BlocoContexto Contexto => (BlocoContexto)base.Contexto;

        public BlocoBase(BlocoContexto contexto) : base(contexto)
        {
            MainVerticalStack = new VerticalStack(contexto);

            if (!String.IsNullOrWhiteSpace(Cabecalho))
            {
                MainVerticalStack.Add(new CabecalhoBloco(contexto, Cabecalho));
            }
        }

        public LinhaCampos AdicionarLinhaCampos()
        {
            var l = new LinhaCampos(Contexto, Width);
            l.Width = Width;
            l.Height = Constantes.CampoAltura;
            MainVerticalStack.Add(l);
            return l;
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
            MainVerticalStack.SetPosition(X, Y);
            MainVerticalStack.Width = Width;
            MainVerticalStack.Draw(gfx);
        }

        public override float Height { get => MainVerticalStack.Height; set => throw new NotSupportedException(); }
        public override bool PossuiContono => false;
    }
}
