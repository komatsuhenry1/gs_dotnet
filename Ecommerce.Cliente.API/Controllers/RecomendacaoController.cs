﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Ecommerce.Cliente.API.Controllers
{
    //Classe com a estrutura de treinamento e dados para  
    public class DadosRecomendacao
    {
        [LoadColumn(0)] public string CPF { get; set; }
        [LoadColumn(1)] public string Produto { get; set; }
        [LoadColumn(2)] public float AvaliacaoProduto { get; set; }
    }

    //Classe que retorna as previsões de recomendação
    public class RecomendacaoProduto
    {
        [ColumnName("Score")]
        public float PontuacaoRecomendacao { get; set; }
        [ColumnName("Produto")]
        public string Produto { get; set; } = string.Empty;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class RecomendacaoController : ControllerBase
    {
        private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "ModeloRecomendacao.zip");
        private readonly string caminhoTreinamento = Path.Combine(Environment.CurrentDirectory, "Data", "recomendacao-train.csv");
        private readonly MLContext mlContext;

        public RecomendacaoController()
        {
            mlContext = new MLContext();

            if (!System.IO.File.Exists(caminhoModelo))
            {
                Console.WriteLine("Modelo não encontrado. Iniciando treinamento...");
                TreinarModelo();
            }
        }

        //Metodo para verificar se o produto é ou nao recomendado baseado na entrada de CPF e Produto
        [HttpGet("recomendar/{cpf}/{produto}")]
        public IActionResult Recomendar(string cpf, string produto)
        {
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return BadRequest("O modelo ainda não foi treinado.");
            }

            ITransformer modelo;
            using (var stream = new FileStream(caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                modelo = mlContext.Model.Load(stream, out var modeloSchema);
            }

            var engineRecomendacao = mlContext.Model.CreatePredictionEngine<DadosRecomendacao, RecomendacaoProduto>(modelo);

            var recomendacao = engineRecomendacao.Predict(new DadosRecomendacao { 
                CPF = cpf,
                Produto = produto
            });

            return Ok(new { 
                produto = recomendacao.Produto,
                recomendacao = GetStatusRecomendacao(recomendacao.PontuacaoRecomendacao)
            });
        }


        //Metodo para treinar o modelo que será utilizado para prever se o produto é ou não recomendado
        private void TreinarModelo()
        {
            var pastaModelo = Path.GetDirectoryName(caminhoModelo);
            if (!Directory.Exists(pastaModelo))
            {
                Directory.CreateDirectory(pastaModelo);
                Console.WriteLine($"Diretório criado: {pastaModelo}");
            }

            IDataView dadosTreinamento = mlContext.Data.LoadFromTextFile<DadosRecomendacao>(
                path: caminhoTreinamento, hasHeader: true, separatorChar: ',');

            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(DadosRecomendacao.AvaliacaoProduto))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "CPFCodificado", inputColumnName: nameof(DadosRecomendacao.CPF)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ProdutoCodificado", inputColumnName: nameof(DadosRecomendacao.Produto)))
                .Append(mlContext.Transforms.Concatenate("Features", "CPFCodificado", "ProdutoCodificado"))
                .Append(mlContext.Regression.Trainers.FastTree());

            var modelo = pipeline.Fit(dadosTreinamento);

            mlContext.Model.Save(modelo, dadosTreinamento.Schema, caminhoModelo);
            Console.WriteLine($"Modelo treinado e salvo em: {caminhoModelo}");
        }

        private string GetStatusRecomendacao(float pontuacao)
        {
            switch (Math.Round(pontuacao, 1))
            {
                case >= 4:
                    return "Altamente Recomendado";
                case >= 3:
                    return "Recomendado";
                default:
                    return "Recomendado";
            }
        }
    }
}
