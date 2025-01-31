using System;
using DanfeSharp.Graphics;

namespace DanfeSharp
{
    public class TextOverflow
    {
        internal static string TratarTexto(string conteudo, Fonte fonte, float maxWidth, bool addElipses = true)
        {
            if (String.IsNullOrWhiteSpace(conteudo))
                return conteudo;

            var textWidth = fonte.MedirLarguraTexto(conteudo);

            if (textWidth <= maxWidth)
                return conteudo;

            var elipse = addElipses ? "..." : "";
            var texto = elipse;
            String texto2;

            for (int i = 1; i <= conteudo.Length; i++)
            {
                texto2 = conteudo.Substring(0, i) + elipse;
                if (fonte.MedirLarguraTexto(texto2) < maxWidth)
                {
                    texto = texto2;
                }
                else
                    break;
            }

            return texto;
        }
    }
}