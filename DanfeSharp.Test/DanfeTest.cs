using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DanfeSharp.Test
{
    [TestClass]
    public class DanfeTest
    {

        [TestMethod]
        public void RetratoSemIcmsInterestadual()
        {
            var model = FabricaFake.DanfeViewModel_1();

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato,
                ExibirIcmsInterestadual = false
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void PaisagemSemIcmsInterestadual()
        {
            var model = FabricaFake.DanfeViewModel_1();

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Paisagem,
                ExibirIcmsInterestadual = false
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void Paisagem_2Canhotos()
        {
            var model = FabricaFake.DanfeViewModel_1();
            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Paisagem,
                QuantidadeCanhotos = 2
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void Retrato_2Canhotos()
        {
            var model = FabricaFake.DanfeViewModel_1();
            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato,
                QuantidadeCanhotos = 2
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void Paisagem_SemCanhoto()
        {
            var model = FabricaFake.DanfeViewModel_1();
            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Paisagem,
                QuantidadeCanhotos = 0
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void Retrato_SemCanhoto()
        {
            var model = FabricaFake.DanfeViewModel_1();
            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato,
                QuantidadeCanhotos = 0
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void Contingencia_SVC_AN()
        {
            var model = FabricaFake.DanfeViewModel_1();
            model.TipoEmissao = Esquemas.NFe.FormaEmissao.ContingenciaSVCAN;
            model.ContingenciaDataHora = DateTime.Now;
            model.ContingenciaJustificativa = "Aqui vai o motivo da contingência";

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void Contingencia_SVC_RS()
        {
            var model = FabricaFake.DanfeViewModel_1();
            model.TipoEmissao = Esquemas.NFe.FormaEmissao.ContingenciaSVCRS;
            model.ContingenciaDataHora = DateTime.Now;
            model.ContingenciaJustificativa = "Aqui vai o motivo da contingência";

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void Retrato()
        {
            var model = FabricaFake.DanfeViewModel_1();

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void OpcaoPreferirEmitenteNomeFantasia_False()
        {
            var model = FabricaFake.DanfeViewModel_1();

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato,
                PreferirEmitenteNomeFantasia = false
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void Paisagem()
        {
            var model = FabricaFake.DanfeViewModel_1();

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Paisagem
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void RetratoHomologacao()
        {
            var model = FabricaFake.DanfeViewModel_1();
            model.TipoAmbiente = 2;

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void PaisagemHomologacao()
        {
            var model = FabricaFake.DanfeViewModel_1();
            model.TipoAmbiente = 2;

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Paisagem
            };

            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, config);
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void ComBlocoLocalEntrega()
        {
            var model = FabricaFake.DanfeViewModel_1();
            model.LocalEntrega = FabricaFake.LocalEntregaRetiradaFake();
            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, new DanfeConfig());
            d.Gerar();
            d.SalvarTestPdf();
        }

        [TestMethod]
        public void ComBlocoLocalRetirada()
        {
            var model = FabricaFake.DanfeViewModel_1();
            model.LocalRetirada = FabricaFake.LocalEntregaRetiradaFake();
            DanfeSharp.Danfe d = new DanfeSharp.Danfe(model, new DanfeConfig());
            d.Gerar();
            d.SalvarTestPdf();
        }

    }
}
