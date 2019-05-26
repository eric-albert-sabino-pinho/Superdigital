using Newtonsoft.Json;
using Superdigital.Domain.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace Superdigital.Test
{
    public class UnitTestPost
    {
        [Fact]
        public bool TestPost()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/Superdigital");
                var obj = new TransacaoEntity() { ContaOrigem = "1234567", ContaDestino = "76543210", ContaDestinoValorTransacao = 500 };
                string jsonLancamento = JsonConvert.SerializeObject(obj);
                StringContent ctLancamento = new StringContent(jsonLancamento);
                ctLancamento.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage retorno = client.PostAsync("", ctLancamento).Result;
                return retorno.IsSuccessStatusCode;

            }
        }
    }
}
