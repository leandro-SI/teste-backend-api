using AutoMapper;
using Gestao.Application.Dtos;
using Gestao.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestao.Application.Profiles
{
    public class GestaoProfile : Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<TarefaDTO, Tarefa>().ReverseMap();
                config.CreateMap<PessoaDTO, Pessoa>().ReverseMap();
                config.CreateMap<ArquivoDTO, Arquivo>().ReverseMap();

            });

            return mappingConfig;
        }
    }
}
