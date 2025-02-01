using System.Linq;

namespace DanfeSharp.Blocos
{
    internal class BlocoDuplicataFatura : BlocoBase
    {
        public BlocoDuplicataFatura(BlocoContexto contexto) : base(contexto)
        {
            var de = ViewModel.Duplicatas.Select(x => new Duplicata(contexto, x)).ToList();
            var eh = de.First().Height;

            int numeroElementosLinha = Config.IsPaisagem ? 7 : 6;

            int i = 0;

            while (i < de.Count)
            {
                FlexibleLine fl = new FlexibleLine(contexto, Width, eh);

                int i2;
                for (i2 = 0; i2 < numeroElementosLinha && i < de.Count; i2++, i++)
                {
                    fl.ComElemento(de[i]);
                }

                for(; i2 < numeroElementosLinha; i2++)
                    fl.ComElemento(new ElementoVazio(contexto));


                fl.ComLargurasIguais();
                MainVerticalStack.Add(fl);
            }
        }

        public override string Cabecalho => "Fatura / Duplicata";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
    }
}
