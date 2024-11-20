using Ecommerce.Cliente.Application.Dtos;
using Ecommerce.Cliente.Domain.Entities;
using Ecommerce.Cliente.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Cliente.API.Controllers
{   

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteApplicationService _applicationService;

        public ClienteController(IClienteApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Obter todos os clientes
        /// </summary>
        /// <remarks>
        /// Obter uma lista com todos os clientes cadastrados, através do método GET.
        /// </remarks>
        /// <param name="getClientes">Obter Todos Clientes</param>
        /// <returns></returns>

        [HttpGet]
        [Produces<IEnumerable<ClienteEntity>>]
        public IActionResult Get()
        {
            var categorias = _applicationService.ObterTodosClientes();

            if (categorias is not null)
                return Ok(categorias);

            return BadRequest("Não foi possivel obter os dados");
        }

        /// <summary>
        /// Obter um cliente
        /// </summary>
        /// <remarks>
        /// Obter um cliente especifico cadastrado, através do método GET.
        /// </remarks>
        /// <param name="getByIdClientes">Obter um Cliente</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces<ClienteEntity>]
        public IActionResult GetPorId(int id)
        {
            var categorias = _applicationService.ObterClientePorId(id);

            if (categorias is not null)
                return Ok(categorias);

            return BadRequest("Não foi possivel obter os dados");
        }

        /// <summary>
        /// Cadastrar um cliente
        /// </summary>
        /// <remarks>
        /// Cadastrar um cliente, através do método POST
        /// </remarks>
        /// <param name="PostClient">Cadastrar um cliente</param>
        /// <returns></returns>
        [HttpPost]
        [Produces<ClienteEntity>]
        public IActionResult Post(ClienteDto entity)
        {
            try
            {
                var categorias = _applicationService.AdicionarCliente(entity);

                if (categorias is not null)
                    return Ok(categorias);

                return BadRequest("Não foi possivel salvar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest,
                });
            }
        }

        /// <summary>
        /// Atualizar um cliente
        /// </summary>
        /// <remarks>
        /// Atualizar alguma ou todas as informações de um cliente, através do método PUT
        /// </remarks>
        /// <param name="PostClient">Atualizar um cliente</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces<ClienteEntity>]
        public IActionResult Put(int id, ClienteDto entity)
        {
            try
            {
                var categorias = _applicationService.EditarCliente(id, entity);

                if (categorias is not null)
                    return Ok(categorias);

                return BadRequest("Não foi possivel editar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest,
                });
            }
        }

        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <remarks>
        /// Deletar um cliente cadastrado, através do método DELETE
        /// </remarks>
        /// <param name="PostClient">Deletar um cliente</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces<ClienteEntity>]
        public IActionResult Delete(int id)
        {
            var categorias = _applicationService.RemoverCliente(id);

            if (categorias is not null)
                return Ok(categorias);

            return BadRequest("Não foi possivel deletar os dados");
        }
    }
}
